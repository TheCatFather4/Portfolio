using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CafeContext _dbContext;

        public PaymentRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public void AddPayment(Payment payment)
        {
             _dbContext.Payment.Add(payment);
             _dbContext.SaveChanges();
        }

        public CafeOrder GetOrderById(int orderId)
        {
            return _dbContext.CafeOrder
                .FirstOrDefault(co => co.OrderID == orderId);
            
        }

        public void UpdateOrderStatus(CafeOrder order)
        {
            _dbContext.CafeOrder.Update(order);
            _dbContext.SaveChanges();
        }
    }
}
