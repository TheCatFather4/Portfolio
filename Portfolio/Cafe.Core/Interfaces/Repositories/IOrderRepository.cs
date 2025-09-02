using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items);
        Task<CafeOrder> GetOrderByIdAsync(int orderId);
        Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId);

        // UpdateOrderStatus(int orderId, string status)
    }
}
