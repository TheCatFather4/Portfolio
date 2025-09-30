using Cafe.Core.Interfaces.Services;
using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Ordering;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IShoppingBagService _shoppingBagService;
        private readonly IMVCustomerService _customerService;

        public ShoppingCartController(UserManager<IdentityUser> userManager, IShoppingBagService shoppingBagService, IMVCustomerService customerService)
        {
            _userManager = userManager;
            _shoppingBagService = shoppingBagService;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ShoppingCart();

            if (User.Identity.IsAuthenticated)
            {
                var customerResult = await _customerService.GetCustomerByEmailAsync(User.Identity.Name);

                if (customerResult.Ok)
                {
                    var cartResult = await _shoppingBagService.GetShoppingBagAsync(customerResult.Data.CustomerID);

                    if (cartResult.Ok)
                    {
                        model.CustomerID = customerResult.Data.CustomerID;
                        model.ShoppingBagID = cartResult.Data.ShoppingBagID;
                        model.Items = cartResult.Data.Items;
                        model.Total = CalculateTotal.AddItems(cartResult.Data.Items);

                        return View(model);
                    }

                    TempData["Alert"] = Alert.CreateError("An error occurred. Please contact the management team.");
                    return RedirectToAction("Index", "Cafe");
                }

                TempData["Alert"] = Alert.CreateError("An error occurred. Please contact the management team.");
                return RedirectToAction("Index", "Cafe");
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> AddItem(int itemId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _shoppingBagService.GetItemWithPriceAsync(itemId);

                if (result.Ok)
                {
                    var model = new CartItem()
                    {
                        ItemID = itemId,
                        ItemName = result.Data.ItemName,
                        Quantity = 0,
                        Price = result.Data.Prices.FirstOrDefault(p => p.ItemID == itemId).Price,
                        ItemStatusID = result.Data.ItemStatusID,
                        ItemImgPath = result.Data.ItemImgPath
                    };

                    return View(model);
                }
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(CartItem model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var customerResult = await _customerService.GetCustomerByEmailAsync(User.Identity.Name);

                    if (customerResult.Ok)
                    {
                        var result = await _shoppingBagService
                            .MVCAddItemToBagAsync((int)customerResult.Data.ShoppingBagID, model.ItemID, model.ItemName, (decimal)model.Price, (byte)model.Quantity, model.ItemImgPath);

                        if (result.Ok)
                        {
                            TempData["Alert"] = Alert.CreateSuccess(result.Message);
                            return RedirectToAction("Index", "ShoppingCart");
                        }

                        TempData["Alert"] = Alert.CreateError("An error occurred. Please contact the management team.");
                        return RedirectToAction("Index", "Cafe");
                    }

                    TempData["Alert"] = Alert.CreateError("An error occurred. Please contact the management team.");
                    return RedirectToAction("Index", "ShoppingCart");
                }

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateItem(int shoppingBagItemId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _customerService.GetCustomerByEmailAsync(User.Identity.Name);

                if (result.Ok)
                {
                    var itemResult = await _shoppingBagService.GetShoppingBagItemByIdAsync(shoppingBagItemId);

                    if (itemResult.Ok)
                    {
                        var model = new ItemUpdate()
                        {
                            CustomerID = result.Data.CustomerID,
                            ShoppingBagItemID = shoppingBagItemId,
                            Quantity = itemResult.Data.Quantity,
                            ItemName = itemResult.Data.ItemName,
                            Price = (decimal)itemResult.Data.Price,
                            ItemImgPath = itemResult.Data.ItemImgPath
                        };

                        return View(model);
                    }

                    TempData["Alert"] = Alert.CreateError("An error ocurred. Please contact our management team.");
                    return RedirectToAction("Index", "ShoppingCart");
                }
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItem(ItemUpdate model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var result = await _shoppingBagService.UpdateItemQuantityAsync(model.CustomerID, model.ShoppingBagItemID, (byte)model.Quantity);

                    if (result.Ok)
                    {
                        TempData["Alert"] = Alert.CreateSuccess("Item quantity successfully updated!");
                    }
                    else
                    {
                        TempData["Alert"] = Alert.CreateError("An error occurred. Please contact our management team for assistance.");
                    }

                    return RedirectToAction("Index", "ShoppingCart");
                }

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem(ItemUpdate model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var updateResult = await _shoppingBagService.RemoveItemFromBagAsync(model.CustomerID, model.ShoppingBagItemID);

                if (updateResult.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess("Item successfully removed from shopping cart.");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError("An error occurred. Please contact our management team.");
                }

                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> EmptyShoppingCart(int customerId)
        {
            var model = new EmptyCart
            {
                CustomerId = customerId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmptyShoppingCart(EmptyCart model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _shoppingBagService.ClearShoppingBagAsync(model.CustomerId);

                if (result.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess("Shopping Cart cleared!");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError("An error occurred. Please contact our management team for assistance.");
                }

                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
