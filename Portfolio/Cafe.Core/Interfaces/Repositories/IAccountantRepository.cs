using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IAccountantRepository
    {
        List<CafeOrder> GetOrders();
        List<ItemPrice> GetItemPrices();
        List<Item> GetItemsByCategoryID(int categoryID);
        List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId);
        ItemPrice GetItemPriceByItemId(int itemId);
    }
}
