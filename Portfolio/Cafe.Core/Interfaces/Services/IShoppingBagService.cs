using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IShoppingBagService
    {
        Task<Result> APIAddItemToBagAsync(int customerId, AddItemRequest dto);
        Task<Result<ShoppingBagResponse>> APIGetShoppingBagAsync(int customerId);
        Task<Result<ShoppingBag>> GetShoppingBagAsync(int customerId);
        Task<Result> RemoveItemFromBagAsync(int customerId, int shoppingBagItemId);
        Task<Result> UpdateItemQuantityAsync(int customerId, int shoppingBagItemId, byte quantity);
        Task<Result> ClearShoppingBagAsync(int customerId);
        Task<Result> MVCAddItemToBagAsync(int shoppingBagId, int itemId, string itemName, decimal price, byte quantity, string imgPath);
        Task<Result<Item>> GetItemWithPriceAsync(int itemId);
        Task<Result<ShoppingBagItem>> GetShoppingBagItemByIdAsync(int shoppingBagItemId);
    }
}