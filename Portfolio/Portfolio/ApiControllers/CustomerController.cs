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
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Injects two ASP.NET Identity dependencies, and two service dependencies
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="customerService"></param>
        /// <param name="jwtService"></param>
        public CustomerController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICustomerService customerService, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registers a new customer
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUser dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                var newCustomerResult =  await _customerService.Register(dto, user.Id);

                if (newCustomerResult.Ok)
                {
                    return Ok("User registered successfully");
                }
            }

            return BadRequest("Error registering user.");
        }

        /// <summary>
        /// Logs in a registered customer and returns a token for authentication
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginUser dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = await _jwtService.GenerateTokenAsync(user);
                return Ok(new { token });
            }

            if (result.IsLockedOut)
            {
                return Unauthorized("Account locked out.");
            }

            return Unauthorized("Invalid credentials.");
        }
    }
}
