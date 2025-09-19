using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Ordering;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    public class ProcessPaymentController : Controller
    {
        private readonly IMVPaymentService _paymentService;
        private readonly ISelectListBuilder _selectListBuilder;

        public ProcessPaymentController(IMVPaymentService paymentService, ISelectListBuilder selectListBuilder)
        {
            _paymentService = paymentService;
            _selectListBuilder = selectListBuilder;
        }

        [HttpGet]
        public IActionResult Process(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new PaymentForm()
                {
                    OrderId = orderId
                };

                model.PaymentTypes = _selectListBuilder.BuildPaymentTypes(TempData);

                if (model.PaymentTypes == null)
                {
                    TempData["Alert"] = Alert.CreateError("Error getting list information.");
                    return RedirectToAction("Index", "Profile");
                }

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Process(PaymentForm model)
        {
            if (User.Identity.IsAuthenticated)
            {
                model.PaymentTypes = _selectListBuilder.BuildPaymentTypes(TempData);

                var payment = new Payment()
                {
                    OrderID = model.OrderId,
                    PaymentTypeID = model.PaymentTypeId,
                    Amount = model.Amount,
                };

                var result = _paymentService.ProcessPayment(payment);

                if (result.Ok)
                {
                    model.TransactionDate = (DateTime)payment.TransactionDate;
                    model.PaymentStatusId = payment.PaymentStatusID;

                    return View(model);
                }

                TempData["Alert"] = Alert.CreateError($"An error ocurred: {result.Message}");
                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
