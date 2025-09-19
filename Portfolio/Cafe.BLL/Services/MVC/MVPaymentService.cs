using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services.MVC;

namespace Cafe.BLL.Services.MVC
{
    public class MVPaymentService : IMVPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public MVPaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Result<List<PaymentType>> GetPaymentTypes()
        {
            var paymentTypes = _paymentRepository.GetPaymentTypes();

            if (paymentTypes == null || paymentTypes.Count == 0)
            {
                return ResultFactory.Fail<List<PaymentType>>("Payment types not found.");
            }

            return ResultFactory.Success(paymentTypes);
        }

        public Result<Payment> ProcessPayment(Payment payment)
        {

            var order = _paymentRepository.GetOrderById(payment.OrderID);

            if (order == null)
            {
                return ResultFactory.Fail<Payment>("Order not found.");
            }
            else if (payment.Amount != order.FinalTotal)
            {
                return ResultFactory.Fail<Payment>("Order amount does not match.");
            }
            else if (order.PaymentStatusID == 1)
            {
                return ResultFactory.Fail<Payment>("Order has already been paid.");
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
                return ResultFactory.Fail<Payment>("An error occurred.");
            }
        }
    }
}
