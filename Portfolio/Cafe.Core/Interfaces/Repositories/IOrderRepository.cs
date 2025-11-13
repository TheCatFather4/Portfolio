using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<List<CafeOrder>> GetAllOrdersAsync();
        Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items);
        Task<CafeOrder> GetOrderByIdAsync(int orderId);
        List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId);
        Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId);
    }
}