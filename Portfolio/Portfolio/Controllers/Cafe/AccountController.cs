using Cafe.Core.DTOs.Requests;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Cafe.Account;

namespace Portfolio.Controllers.Cafe
{
    /// <summary>
    /// Handles requests concerning customer accounts.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Constructs an MVC controller with the required dependencies for authentication, authorization, and customer profiles.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="customerService"></param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
        }

        /// <summary>
        /// Takes the user to a web page where they can register as a new customer.
        /// </summary>
        /// <returns>The register view.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterUserForm();

            return View(model);
        }

        /// <summary>
        /// Registers a new customer with the café. If successful, the customer is signed in and given a new shopping bag.
        /// </summary>
        /// <param name="model">A model used to register new customers.</param>
        /// <returns>A RedirectToActionResult that sends the user to the café's homepage.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserForm model)
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

                        TempData["Alert"] = Alert.CreateSuccess("New account successfully registered!");
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

        /// <summary>
        /// Takes the user to a web page where they can log in to their account.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Attempts to sign a user into their café account.
        /// If successful, the user will be redirected to the café home page.
        /// </summary>
        /// <param name="model">A model used to log customers in to their account.</param>
        /// <returns>A webpage with a confirmation message depending on the result.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserForm model)
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

        /// <summary>
        /// Logs a customer out of their account.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Alert"] = Alert.CreateSuccess("You have successfully logged out.");
            return RedirectToAction("Cafe", "Home");
        }

        /// <summary>
        /// Takes an authenticated user to their customer profile web page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var model = new CustomerProfile();

            if (User.Identity.IsAuthenticated)
            {
                var result = await _customerService.GetCustomerByEmailAsync(User.Identity.Name);

                if (result.Ok)
                {
                    model.CustomerID = result.Data.CustomerID;
                    model.FirstName = result.Data.FirstName;
                    model.LastName = result.Data.LastName;
                    model.Email = result.Data.Email;
                    model.Id = result.Data.Id;
                    model.ShoppingBagId = result.Data.ShoppingBagID;
                }
            }

            return View(model);
        }

        /// <summary>
        /// Updates a customer's first and last name associated with their account.
        /// </summary>
        /// <param name="model">A model used for updating a customer's profile.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(CustomerProfile model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var entity = model.ToEntity();

                    var result = await _customerService.UpdateCustomerAsync(entity);

                    if (result.Ok)
                    {
                        TempData["Alert"] = Alert.CreateSuccess(result.Message);
                        return View(model);
                    }
                    else
                    {
                        TempData["Alert"] = Alert.CreateError("Unable to update your customer data. Please contact the administrator at 1-800-123-4567 for assistance.");
                        return View(model);
                    }
                }

                return View(model);
            }

            return RedirectToAction("login", "Account");
        }

        /// <summary>
        /// Returns a webpage to users that are not authorized to access the
        /// management area or sales report area.
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}