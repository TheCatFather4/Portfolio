using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning payments
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly ILogger _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;

        /// <summary>
        /// Constructs a service with the dependencies required for retrieving and making payments.
        /// </summary>
        /// <param name="logger">A dependency used for logging errors.</param>
        /// <param name="orderRepository">A dependency used for retrieving and updating orders.</param>
        /// <param name="paymentRepository">A dependency used for retrieving PaymentType records and creating new Payment records.</param>
        public PaymentService(ILogger<PaymentService> logger, IOrderRepository orderRepository, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
        }

        /// <summary>
        /// An OrderID is used to retrieve a total for the corresponding CafeOrder. 
        /// If found, the total is returned.
        /// </summary>
        /// <param name="orderId">An OrderID used to retrieve the final total of a CafeOrder record.</param>
        /// <returns>A Result DTO with a price as its data.</returns>
        public async Task<Result<decimal>> GetFinalTotalAsync(int orderId)
        {
            try
            {
                var total = await _paymentRepository.GetFinalTotalAsync(orderId);

                if (total == 0)
                {
                    _logger.LogError($"Final total for Order ID: {orderId} is 0.");
                    return ResultFactory.Fail<decimal>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(total);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve the final total for CafeOrder {orderId}: {ex.Message}");
                return ResultFactory.Fail<decimal>("An error occurred. Please contact the site administrator.");
            }
        }

        /// <summary>
        /// A repository method is invoked to retrieve all payment types from the database. 
        /// If successful, a list of PaymentType records is returned.
        /// </summary>
        /// <returns>A Result DTO with a list of PaymentType records as its data.</returns>
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

        /// <summary>
        /// Attempts to retrieve an Order record by OrderID. 
        /// If successful, a new Payment record is created and added to the database.
        /// Note: The Random object is used to simulate a declined payment.
        /// </summary>
        /// <param name="dto">A request DTO used to retrieve an Order record and in creating a new Payment record.</param>
        /// <returns>A Result DTO with a PaymentResponse DTO as its data.</returns>
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

                // Simulation of declined payment
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