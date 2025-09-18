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

                    TempData["Alert"] = Alert.CreateSuccess("Order ready for payment!");
                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError("Unable to process order.");
                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
