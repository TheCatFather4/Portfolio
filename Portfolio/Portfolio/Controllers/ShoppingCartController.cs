using Cafe.Core.DTOs;
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
        private readonly IMVCCustomerService _customerService;

        public ShoppingCartController(UserManager<IdentityUser> userManager, IShoppingBagService shoppingBagService, IMVCCustomerService customerService)
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
                    var cartResult = await _shoppingBagService.GetShoppingBagByCustomerIdAsync(customerResult.Data.CustomerID);

                    if (cartResult.Ok)
                    {
                        model.CustomerID = cartResult.Data.CustomerID;
                        model.ShoppingBagID = cartResult.Data.ShoppingBagID;
                        model.Items = cartResult.Data.Items;
                        model.Total = CalculateTotal.AddItems(cartResult.Data.Items);

                        return View(model);
                    }

                    TempData["Alert"] = Alert.CreateError(cartResult.Message);
                    return RedirectToAction("Cafe", "Home");
                }

                TempData["Alert"] = Alert.CreateError(customerResult.Message);
                return RedirectToAction("Cafe", "Home");
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
                        var dto = new AddItemRequest
                        {
                            ShoppingBagId = (int)customerResult.Data.ShoppingBagID,
                            ItemId = model.ItemID,
                            Quantity = (byte)model.Quantity,
                            ItemName = model.ItemName,
                            Price = (decimal)model.Price,
                            ItemImgPath = model.ItemImgPath
                        };

                        var result = await _shoppingBagService.AddItemToShoppingBagAsync(dto);

                        if (result.Ok)
                        {
                            TempData["Alert"] = Alert.CreateSuccess(result.Message);
                            return RedirectToAction("Index", "ShoppingCart");
                        }

                        TempData["Alert"] = Alert.CreateError(result.Message);
                        return RedirectToAction("Cafe", "Home");
                    }

                    TempData["Alert"] = Alert.CreateError(customerResult.Message);
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

                    TempData["Alert"] = Alert.CreateError(itemResult.Message);
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
                    var result = await _shoppingBagService.UpdateItemQuantityAsync(model.ShoppingBagItemID, (byte)model.Quantity);

                    if (result.Ok)
                    {
                        TempData["Alert"] = Alert.CreateSuccess(result.Message);
                    }
                    else
                    {
                        TempData["Alert"] = Alert.CreateError(result.Message);
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
                    TempData["Alert"] = Alert.CreateSuccess(updateResult.Message);
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(updateResult.Message);
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
                    TempData["Alert"] = Alert.CreateSuccess(result.Message);
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                }

                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}