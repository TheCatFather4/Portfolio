using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IShoppingBagService
    {
        Task<Result> AddItemToShoppingBagAsync(AddItemRequest dto);
        Task<Result<ShoppingBagResponse>> GetShoppingBagByCustomerIdAsync(int customerId);

        // Used by Order Service - refactor later
        Task<Result<ShoppingBag>> GetShoppingBagAsync(int customerId);
        Task<Result> RemoveItemFromBagAsync(int customerId, int shoppingBagItemId);
        Task<Result> UpdateItemQuantityAsync(int customerId, int shoppingBagItemId, byte quantity);
        Task<Result> ClearShoppingBagAsync(int customerId);
        Task<Result<Item>> GetItemWithPriceAsync(int itemId);
        Task<Result<ShoppingBagItem>> GetShoppingBagItemByIdAsync(int shoppingBagItemId);
    }
}