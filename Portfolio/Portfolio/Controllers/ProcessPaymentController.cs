using Cafe.Core.DTOs.Requests;
using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.Payments;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    /// <summary>
    /// Handles requests concerning payments.
    /// </summary>
    public class ProcessPaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ISelectListBuilder _selectListBuilder;

        /// <summary>
        /// Constructs a controller with the required dependencies to process payments.
        /// Note: payments are simulated in the application.
        /// </summary>
        /// <param name="paymentService"></param>
        /// <param name="selectListBuilder"></param>
        public ProcessPaymentController(IPaymentService paymentService, ISelectListBuilder selectListBuilder)
        {
            _paymentService = paymentService;
            _selectListBuilder = selectListBuilder;
        }

        /// <summary>
        /// Displays the final total to be paid for a café order.
        /// </summary>
        /// <param name="orderId">The ID of the order to be paid.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
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

                var result = await _paymentService.GetFinalTotalAsync(orderId);

                if (!result.Ok)
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return RedirectToAction("Index", "Profile");
                }

                model.FinalTotal = result.Data;

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// The model data is mapped to a request DTO and then sent to the service layer to be processed.
        /// If successful, the model is returned with confirmation of payment data.
        /// </summary>
        /// <param name="model">A model used to create new payments.</param>
        /// <returns>A created ViewResult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(PaymentForm model)
        {
            if (User.Identity.IsAuthenticated)
            {
                // The SelectList must be reloaded before returning the model.
                model.PaymentTypes = await _selectListBuilder.BuildPaymentTypesAsync(TempData);

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
                        model.PaymentTypeName = EnumConverter.GetPaymentTypeName((int)model.PaymentTypeId);

                        return View(model);
                    }

                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}