using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ApiControllers
{
    /// <summary>
    /// Handles requests involving customer registration and logging in
    /// </summary>
    [Route("api/cafe/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerService _customerService;
        private readonly IWebTokenService _jwtService;

        /// <summary>
        /// Injects two ASP.NET Identity dependencies, and two service dependencies
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="customerService"></param>
        /// <param name="jwtService"></param>
        public CustomerController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICustomerService customerService, IWebTokenService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registers a new customer with the cafe and a new identity user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] AddCustomerRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var duplicateEmail = await _customerService.GetDuplicateEmailAsync(dto.Email);

            if (!duplicateEmail.Ok)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                dto.IdentityId = user.Id;

                var newCustomerResult = await _customerService.AddCustomerAsync(dto);

                if (newCustomerResult.Ok)
                {
                    return Ok("User registered successfully");
                }

                return StatusCode(500, newCustomerResult.Message);
            }

            return StatusCode(500, "An error occurred. Please try again later.");
        }

        /// <summary>
        /// Checks if a user's credentials are valid and if so, returns a JSON web token for authorization
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return NotFound("Invalid user name.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token =  _jwtService.GenerateToken(user);

                if (token == null)
                {
                    return StatusCode(500, "An error occurred. Please try again in a few minutes.");
                }

                return Ok(new { token });
            }
            else if (result.IsLockedOut)
            {
                return Unauthorized("There have been too many login attempts for this user. Please try again later.");
            }
            else
            {
                return NotFound("Invalid password.");
            }
        }
    }
}