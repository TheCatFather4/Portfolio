using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMVCustomerService _customerService;

        public ProfileController(UserManager<IdentityUser> userManager, IMVCustomerService customerService)
        {
            _userManager = userManager;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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

        [HttpPost]
        public async Task<IActionResult> Index(CustomerProfile model)
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
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Alert"] = Alert.CreateError("Unable to update your customer data. Please contact the administrator at 1-800-123-4567 for assistance.");
                        return RedirectToAction("Index");
                    }
                }

                return View(model);
            }

            return RedirectToAction("login", "Account");
        }
    }
}