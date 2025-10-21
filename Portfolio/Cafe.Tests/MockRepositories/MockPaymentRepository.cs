using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockPaymentRepository : IPaymentRepository
    {
        public void AddPayment(Payment payment)
        {
            var payments = new List<Payment>();

            payments.Add(payment);
        }

        public CafeOrder GetOrderById(int orderId)
        {
            var order = new CafeOrder
            {
                OrderID = orderId
            };

            return order;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            var payments = new List<PaymentType>();

            var payType = new PaymentType
            {
                PaymentTypeID = 1,
                PaymentTypeName = "Visa"
            };

            payments.Add(payType);

            return payments;
        }

        public void UpdateOrderStatus(CafeOrder order)
        {
            var existingOrder = new CafeOrder();

            existingOrder.OrderID = order.OrderID;
        }
    }
}