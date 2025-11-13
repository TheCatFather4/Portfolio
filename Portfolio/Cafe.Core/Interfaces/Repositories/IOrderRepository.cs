using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items);
        Task<List<CafeOrder>> GetAllOrdersAsync();
        Task<CafeOrder> GetOrderByIdAsync(int orderId);
        Task<List<OrderItem>> GetOrderItemsByItemPriceIdAsync(int itemPriceId);
        Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId);
    }
}