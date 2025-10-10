using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services.MVC
{
    public class MVCPaymentService : IMVPaymentService
    {
        private readonly ILogger _logger;
        private readonly IPaymentRepository _paymentRepository;

        public MVCPaymentService(ILogger<MVCPaymentService> logger, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _paymentRepository = paymentRepository;
        }

        public Result<List<PaymentType>> GetPaymentTypes()
        {
            var paymentTypes = _paymentRepository.GetPaymentTypes();

            if (paymentTypes == null || paymentTypes.Count == 0)
            {
                _logger.LogError("An error occurred when attempting to retrieve payment types.");
                return ResultFactory.Fail<List<PaymentType>>("An error occurred. Please try again in a few minutes.");
            }

            return ResultFactory.Success(paymentTypes);
        }

        public Result<Payment> ProcessPayment(Payment payment)
        {

            var order = _paymentRepository.GetOrderById(payment.OrderID);

            if (order == null)
            {
                _logger.LogError($"Order with ID: {payment.OrderID} not found.");
                return ResultFactory.Fail<Payment>("An error occurred. Please try again in a few minutes.");
            }
            else if (payment.Amount != order.FinalTotal)
            {
                return ResultFactory.Fail<Payment>($"You must pay the full amount due of: {order.FinalTotal:c}");
            }
            else if (order.PaymentStatusID == 1)
            {
                return ResultFactory.Fail<Payment>("This order has already been paid.");
            }

            var random = new Random();
            var isSuccessful = random.Next(1, 100) <= 90;

            payment.TransactionDate = DateTime.Now;
            payment.PaymentStatusID = (byte)(isSuccessful ? 1 : 3);

            try
            {
                _paymentRepository.AddPayment(payment);

                order.PaymentStatusID = payment.PaymentStatusID;
                _paymentRepository.UpdateOrderStatus(order);

                return ResultFactory.Success(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to process payment for Order ID: {payment.OrderID}");
                return ResultFactory.Fail<Payment>("An error occurred. Please contact our management team for assistance.");
            }
        }
    }
}