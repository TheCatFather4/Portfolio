using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Ordering;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    public class ProcessPaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ISelectListBuilder _selectListBuilder;

        public ProcessPaymentController(IPaymentService paymentService, ISelectListBuilder selectListBuilder)
        {
            _paymentService = paymentService;
            _selectListBuilder = selectListBuilder;
        }

        [HttpGet]
        public async Task<IActionResult> Process(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new PaymentForm()
                {
                    OrderId = orderId,
                };

                model.PaymentTypes = await _selectListBuilder.BuildPaymentTypesAsync(TempData);

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
        public async Task<IActionResult> Process(PaymentForm model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var dto = new PaymentRequest()
                    {
                        OrderID = model.OrderId,
                        PaymentTypeID = (int)model.PaymentTypeId,
                        Amount = (decimal)model.Amount,
                    };

                    var result = await _paymentService.ProcessPaymentAsync(dto);

                    if (result.Ok)
                    {
                        model.TransactionDate = result.Data.TransactionDate;
                        model.PaymentStatusId = (byte)result.Data.PaymentStatusID;

                        return View(model);
                    }

                    TempData["Alert"] = Alert.CreateError(result.Message);
                    model.PaymentTypes = await _selectListBuilder.BuildPaymentTypesAsync(TempData);
                    return View(model);
                }

                model.PaymentTypes = await _selectListBuilder.BuildPaymentTypesAsync(TempData);
                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}