using Cafe.Core.DTOs.Requests;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Cafe.ShoppingCart;
using Portfolio.Utilities;

namespace Portfolio.Controllers.Cafe
{
    /// <summary>
    /// Handles requests concerning shopping carts.
    /// </summary>
    public class ShoppingCartController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMenuRetrievalService _menuRetrievalService;
        private readonly IShoppingBagService _shoppingBagService;

        /// <summary>
        /// Constructs a controller with the required dependencies for creating, reading, updating, and deleting entities concerning shopping carts.
        /// </summary>
        /// <param name="customerService"></param>
        /// <param name="menuRetrievalService"></param>
        /// <param name="shoppingBagService"></param>
        public ShoppingCartController(ICustomerService customerService, IMenuRetrievalService menuRetrievalService, IShoppingBagService shoppingBagService)
        {
            _customerService = customerService;
            _menuRetrievalService = menuRetrievalService;
            _shoppingBagService = shoppingBagService;
        }

        /// <summary>
        /// Takes the user to their shopping cart. Authentication is required to access.
        /// </summary>
        /// <returns>A created ViewResult object with the model state.</returns>
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

        /// <summary>
        /// Adds an item to the user's shopping cart.
        /// </summary>
        /// <param name="itemId">The ID of the item to add.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpGet]
        public async Task<IActionResult> AddItem(int itemId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _menuRetrievalService.GetItemByIdMVCAsync(itemId);

                if (result.Ok)
                {
                    var model = new AddCartItemForm()
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

        /// <summary>
        /// If authenticated and model state is valid, the model state is mapped to a request DTO 
        /// and passed to the associated service member to add it to the database.
        /// </summary>
        /// <param name="model">The model used to transport an item's data.</param>
        /// <returns>A RedirectedToActionResult with a confirmation message.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(AddCartItemForm model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var customerResult = await _customerService.GetCustomerByEmailAsync(User.Identity.Name);

                    if (customerResult.Ok)
                    {
                        var dto = new AddItemToBagRequest
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

        /// <summary>
        /// Deletes an item from a customer's shopping cart.
        /// </summary>
        /// <param name="model">The model used to delete an item.</param>
        /// <returns>A RedirectedToActionResult with a confirmation message.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem(DeleteOrEditCartItem model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var updateResult = await _shoppingBagService.RemoveItemFromShoppingBagAsync(model.CustomerID, model.ShoppingBagItemID);

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

        /// <summary>
        /// Takes the user to a web page that asks them for confirmation about emptying their shopping cart.
        /// </summary>
        /// <param name="customerId">The ID of the customer's shopping cart.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpGet]
        public async Task<IActionResult> EmptyShoppingCart(int customerId)
        {
            var model = new EmptyCart
            {
                CustomerId = customerId
            };

            return View(model);
        }

        /// <summary>
        /// Empties a customer's shopping cart or takes them back to their shopping cart with all of their items remaining.
        /// </summary>
        /// <param name="model">The model used to empty the customer's shopping cart.</param>
        /// <returns>A RedirectedToActionResult with a confirmation message.</returns>
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

        /// <summary>
        /// Take the user to a form to update the quantity of an item in their shopping cart.
        /// </summary>
        /// <param name="shoppingBagItemId">The ID of the shopping bag item to update.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
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
                        var model = new DeleteOrEditCartItem()
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

        /// <summary>
        /// Updates the quantity of an item in the customer's shopping cart.
        /// </summary>
        /// <param name="model">The model used to update the quantity of the item.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItem(DeleteOrEditCartItem model)
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
    }
}