using Cafe.Core.DTOs.Requests;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Cafe.ProcessOrder;
using Portfolio.Utilities;

namespace Portfolio.Controllers.Cafe
{
    /// <summary>
    /// Handles requests concerning café orders.
    /// </summary>
    public class ProcessOrderController : Controller
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Constructs a controller with the required dependencies for creating and retrieving café orders.
        /// </summary>
        /// <param name="orderService"></param>
        public ProcessOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Retrieves the total price of the items in the customer's shopping bag.
        /// If successful, the data is mapped to an OrderForm model.
        /// The model is returned for the user to add a tip if desired.
        /// </summary>
        /// <param name="customerId">The ID associated with the customer's shopping bag.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpGet]
        public async Task<IActionResult> Process(int customerId)
        {
            if (User.Identity.IsAuthenticated)
            {
                // The invoked method accesses the customer's current shopping bag items to calculate the total.
                var total = await _orderService.GetOrderTotalAsync(customerId);

                if (total.Ok)
                {
                    var model = new OrderForm
                    {
                        CustomerId = customerId,
                        PaymentTypeId = 1,
                        TipFormTotal = total.Data,
                        TipTen = total.Data * 0.10M,
                        TipFifteen = total.Data * 0.15M,
                        TipTwenty = total.Data * 0.20M
                    };

                    return View(model);
                }
            }

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// The model is mapped to a request DTO, and is sent to the service layer to add to the database.
        /// If successful, the returned response DTO is mapped to the model and displayed to the user.
        /// </summary>
        /// <param name="model">Used to create a new order entity.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(OrderForm model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var dto = new CafeOrderRequest
                    {
                        CustomerId = model.CustomerId,
                        PaymentTypeId = model.PaymentTypeId,
                        Tip = (decimal)model.Tip
                    };

                    var result = await _orderService.CreateNewOrderAsync(dto);

                    if (result.Ok)
                    {
                        model.SubTotal = result.Data.SubTotal;
                        model.Tax = result.Data.Tax;
                        model.FinalTotal = result.Data.FinalTotal;
                        model.PaymentStatusId = (byte?)result.Data.PaymentStatusID;
                        model.OrderId = result.Data.OrderID;

                        TempData["Alert"] = Alert.CreateSuccess("Order ready for payment!");
                        return View(model);
                    }

                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return RedirectToAction("Index", "ShoppingCart");
                }

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Retrieves a list of café orders associated with a specific customer ID.
        /// If successful, the list is mapped to a model and then returned with a rendered view for display.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpGet]
        public async Task<IActionResult> OrderHistory(int customerId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _orderService.GetOrderHistoryAsync(customerId);

                if (result.Ok)
                {
                    var model = result.Data.Select(o => new OrderDetails
                    {
                        OrderId = o.OrderID,
                        PaymentStatusId = (byte?)o.PaymentStatusID,
                        OrderDate = o.OrderDate,
                        SubTotal = o.SubTotal,
                        Tax = o.Tax,
                        Tip = o.Tip,
                        FinalTotal = o.FinalTotal,
                        Items = o.OrderItems?.Select(oi => new OrderItemDetails
                        {
                            OrderItemId = oi.OrderItemID,
                            Quantity = oi.Quantity,
                            ExtendedPrice = oi.ExtendedPrice
                        }).ToList() ?? new List<OrderItemDetails>()
                    }).ToList();

                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Profile", "Account");
            }

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Retrieves the data associated with a specific café order ID.
        /// If successful, the data is mapped to an OrderDetails model.
        /// The model is then returned with the rendered view.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpGet]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _orderService.GetOrderDetailsAsync(orderId);

                if (result.Ok)
                {
                    var model = new OrderDetails
                    {
                        OrderId = result.Data.OrderID,
                        PaymentStatusId = (byte?)result.Data.PaymentStatusID,
                        OrderDate = result.Data.OrderDate,
                        SubTotal = result.Data.SubTotal,
                        Tax = result.Data.Tax,
                        Tip = result.Data.Tip,
                        FinalTotal = result.Data.FinalTotal,
                        Items = result.Data.OrderItems?.Select(oi => new OrderItemDetails
                        {
                            OrderItemId = oi.OrderItemID,
                            ItemName = EnumConverter.GetItemPriceName(oi.ItemPriceID),
                            Quantity = oi.Quantity,
                            ExtendedPrice = oi.ExtendedPrice
                        }).ToList() ?? new List<OrderItemDetails>()
                    };

                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}