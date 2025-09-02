using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger _logger;

        public PaymentService(IPaymentRepository paymentRepository, ILogger logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public Result<PaymentResponse> ProcessPayment(PaymentRequest dto)
        {
            _logger.LogInformation("Attempting to retrieve order details...");
            var order = _paymentRepository.GetOrderById(dto.OrderID);

            if (order == null)
            {
                _logger.LogWarning($"Order not found for ID: {dto.OrderID}.");
                return ResultFactory.Fail<PaymentResponse>("Order not found.");
            }
            else if (dto.Amount != order.FinalTotal)
            {
                _logger.LogWarning($"Amount of {dto.Amount:c} does not match order total of {order.FinalTotal:c}.");
                return ResultFactory.Fail<PaymentResponse>("Order amount does not match.");
            }
            else if (order.PaymentStatusID == 1)
            {
                _logger.LogWarning($"Order ID: {order.OrderID} has already been paid with a status of {order.PaymentStatusID}.");
                return ResultFactory.Fail<PaymentResponse>("Order has already been paid.");
            }

            var random = new Random();
            var isSuccessful = random.Next(1, 100) <= 90;

            var payment = new Payment
            {
                OrderID = dto.OrderID,
                PaymentTypeID = dto.PaymentTypeID,
                Amount = dto.Amount,
                TransactionDate = DateTime.Now,
                PaymentStatusID = (byte)(isSuccessful ? 1 : 3)
            };
            
            try
            {
                _logger.LogInformation("Atttempting to process payment...");
                _paymentRepository.AddPayment(payment);

                order.PaymentStatusID = payment.PaymentStatusID;
                _paymentRepository.UpdateOrderStatus(order);

                var paymentResponse = new PaymentResponse
                {
                    OrderID = payment.OrderID,
                    PaymentStatusID = payment.PaymentStatusID,
                };

                return ResultFactory.Success(paymentResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                return ResultFactory.Fail<PaymentResponse>("An unexpected error occurred.");
            }
        }
    }
}
