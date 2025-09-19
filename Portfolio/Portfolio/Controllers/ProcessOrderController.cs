using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Ordering;

namespace Portfolio.Controllers
{
    public class ProcessOrderController : Controller
    {
        private readonly IMVOrderService _orderService;

        public ProcessOrderController(IMVOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Process(int customerId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new OrderForm
                {
                    CustomerId = customerId,
                    PaymentTypeId = 1
                };

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(OrderForm model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _orderService.CreateNewOrderAsync(model.CustomerId, model.PaymentTypeId, model.Tip);

                if (result.Ok)
                {
                    model.SubTotal = result.Data.SubTotal;
                    model.Tax = result.Data.Tax;
                    model.FinalTotal = result.Data.FinalTotal;
                    model.PaymentStatusId = result.Data.PaymentStatusID;
                    model.OrderId = result.Data.OrderID;

                    TempData["Alert"] = Alert.CreateSuccess("Order ready for payment!");
                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError("Unable to process order.");
                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Login", "Account");
        }

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
                        PaymentStatusId = o.PaymentStatusID,
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

                TempData["Alert"] = Alert.CreateError("Order details not found");
                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _orderService.GetOrderByIdAsync(orderId);

                if (result.Ok)
                {
                    var model = new OrderDetails
                    {
                        OrderId = result.Data.OrderID,
                        PaymentStatusId = result.Data.PaymentStatusID,
                        OrderDate = result.Data.OrderDate,
                        SubTotal = result.Data.SubTotal,
                        Tax = result.Data.Tax,
                        Tip = result.Data.Tip,
                        FinalTotal = result.Data.FinalTotal,
                        Items = result.Data.OrderItems?.Select(oi => new OrderItemDetails
                        {
                            OrderItemId = oi.OrderItemID,
                            Quantity = oi.Quantity,
                            ExtendedPrice = oi.ExtendedPrice
                        }).ToList() ?? new List<OrderItemDetails>()
                    };

                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError("Orders not found.");
                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
