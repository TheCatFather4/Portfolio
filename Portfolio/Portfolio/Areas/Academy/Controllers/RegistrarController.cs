using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.Academy.Models.Registrar;

namespace Portfolio.Areas.Academy.Controllers
{
    /// <summary>
    /// Handles requests concerning user accounts.
    /// </summary>
    [Area("Academy")]
    public class RegistrarController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Constructs a controller with the required dependencies to authenticate users.
        /// </summary>
        /// <param name="signInManager"></param>
        public RegistrarController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Takes the user to a web page where they can log in to the academy.
        /// </summary>
        /// <returns>A ViewResult object.</returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Attempts to authenticate the user.
        /// </summary>
        /// <param name="model">Used to identify the user's credentials.</param>
        /// <returns>A RedirectionToActionResult if successful, otherwise returns the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForm model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        /// <summary>
        /// Logs a user out of their account.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}