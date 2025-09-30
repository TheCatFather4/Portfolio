using Cafe.Core.Interfaces.Services.MVC;
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
        private readonly IMVCustomerService _customerService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMVCustomerService customerService)
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
                var emailResult = await _customerService.GetCustomerByNewEmailAsync(model.Email);

                if (!emailResult.Ok)
                {
                    TempData["Alert"] = Alert.CreateError(emailResult.Message);
                    return View(model);
                }

                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var customerResult = await _customerService.RegisterCustomerAsync(model.Email, user.Id);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (customerResult.Ok)
                    {
                        TempData["Alert"] = Alert.CreateSuccess(customerResult.Message);
                    }

                    return RedirectToAction("Index", "Cafe");
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
            return RedirectToAction("Index", "Cafe");
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
                    return RedirectToAction("Index", "Cafe");
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
