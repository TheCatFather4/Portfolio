using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items);
        List<CafeOrder> GetAllOrders();
        Task<CafeOrder> GetOrderByIdAsync(int orderId);
        List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId);
        Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId);
    }
}