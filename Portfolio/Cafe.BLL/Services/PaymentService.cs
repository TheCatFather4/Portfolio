using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(ILogger<PaymentService> logger, IOrderRepository orderRepository, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<List<PaymentType>>> GetPaymentTypesAsync()
        {
            try
            {
                var paymentTypes = await _paymentRepository.GetPaymentTypesAsync();

                if (paymentTypes == null || paymentTypes.Count == 0)
                {
                    _logger.LogError("Payment types not found.");
                    return ResultFactory.Fail<List<PaymentType>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(paymentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve payment types: {ex.Message}");
                return ResultFactory.Fail<List<PaymentType>>("An error occurred. Please contact the site administrator.");
            }
        }

        public async Task<Result<PaymentResponse>> ProcessPaymentAsync(PaymentRequest dto)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(dto.OrderID);

                if (order == null)
                {
                    _logger.LogError($"Order with ID {dto.OrderID} not found.");
                    return ResultFactory.Fail<PaymentResponse>("An error occurred. Please try again in a few minutes.");
                }
                else if (dto.Amount != order.FinalTotal)
                {
                    return ResultFactory.Fail<PaymentResponse>($"You must pay the full amount due of {order.FinalTotal:c}");
                }
                else if (order.PaymentStatusID == 1)
                {
                    return ResultFactory.Fail<PaymentResponse>("This order has already been paid.");
                }

                var rng = new Random();
                var isSuccessful = rng.Next(1, 100) <= 90;

                var payment = new Payment
                {
                    OrderID = dto.OrderID,
                    Amount = dto.Amount,
                    PaymentTypeID = dto.PaymentTypeID,
                    TransactionDate = DateTime.Now,
                    PaymentStatusID = (byte)(isSuccessful ? 1 : 3)
                };

                order.PaymentStatusID = payment.PaymentStatusID;

                await _paymentRepository.AddPaymentAsync(payment);
                await _orderRepository.UpdateOrderStatusAsync(order);

                var responseDto = new PaymentResponse
                {
                    OrderID = order.OrderID,
                    PaymentStatusID = payment.PaymentStatusID,
                    TransactionDate = (DateTime)payment.TransactionDate
                };

                return ResultFactory.Success(responseDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to process a payment: {ex.Message}");
                return ResultFactory.Fail<PaymentResponse>("An error occurred. Please contact the site administrator.");
            }
        }
    }
}