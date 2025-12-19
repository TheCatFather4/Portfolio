using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockPaymentRepository : IPaymentRepository
    {
        public async Task AddPaymentAsync(Payment payment)
        {
            var payments = new List<Payment>();

            await Task.Delay(1000);
            payments.Add(payment);
        }

        public async Task<decimal> GetFinalTotalAsync(int orderId)
        {
            if (orderId == 1)
            {
                await Task.Delay(1000);
                return 20.00M;
            }

            return 0;
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            var paymentTypes = new List<PaymentType>();

            var pt1 = new PaymentType
            {
                PaymentTypeID = 1,
                PaymentTypeName = "Cash"
            };

            var pt2 = new PaymentType
            {
                PaymentTypeID = 2,
                PaymentTypeName = "Visa"
            };

            var pt3 = new PaymentType
            {
                PaymentTypeID = 3,
                PaymentTypeName = "Mastercard"
            };

            paymentTypes.Add(pt1);
            paymentTypes.Add(pt2);
            paymentTypes.Add(pt3);

            await Task.Delay(1000);
            return paymentTypes;
        }
    }
}