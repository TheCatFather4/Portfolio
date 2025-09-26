using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IAccountantService
    {
        Result<List<CafeOrder>> GetOrders();
        Result<List<ItemPrice>> GetItemPrices();
        Result<List<Item>> GetItemsByCategoryID(int categoryID);
        Result<List<OrderItem>> GetOrderItemsByItemPriceId(int itemPriceId);
        Result<ItemPrice> GetItemPriceByItemId(int itemId);
    }
}
