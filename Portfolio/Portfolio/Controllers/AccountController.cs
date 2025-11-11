using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Identity;

namespace Portfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerService _customerService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var emailResult = await _customerService.GetDuplicateEmailAsync(model.Email);

                if (!emailResult.Ok)
                {
                    TempData["Alert"] = Alert.CreateError(emailResult.Message);
                    return View(model);
                }

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var dto = new AddCustomerRequest
                    {
                        FirstName = "New",
                        LastName = "Customer",
                        Email = model.Email,
                        Password = model.Password,
                        IdentityId = user.Id
                    };

                    var customerResult = await _customerService.AddCustomerAsync(dto);

                    if (customerResult.Ok)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        TempData["Alert"] = Alert.CreateSuccess(customerResult.Message);
                        return RedirectToAction("Cafe", "Home");
                    }

                    TempData["Alert"] = Alert.CreateError(customerResult.Message);
                    return RedirectToAction("Cafe", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Alert"] = Alert.CreateSuccess("You have successfully logged out.");
            return RedirectToAction("Cafe", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, 
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Cafe", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}