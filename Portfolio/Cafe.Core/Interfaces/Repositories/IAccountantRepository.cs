using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IAccountantRepository
    {
        List<CafeOrder> GetOrders();
        List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId);
    }
}