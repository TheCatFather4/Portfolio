using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services.MVC
{
    public interface IMVCAccountantService
    {
        Result<List<CafeOrder>> GetOrders();
        Result<List<Item>> GetItemsByCategoryID(int categoryID);
        Result<List<OrderItem>> GetOrderItemsByItemPriceId(int itemPriceId);
        Result<ItemPrice> GetItemPriceByItemId(int itemId);
    }
}
