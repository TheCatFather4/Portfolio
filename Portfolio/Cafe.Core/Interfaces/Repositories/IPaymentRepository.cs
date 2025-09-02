using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        CafeOrder GetOrderById(int orderId);
        void UpdateOrderStatus(CafeOrder order);
    }
}
