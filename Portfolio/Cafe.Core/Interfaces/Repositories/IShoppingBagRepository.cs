using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IShoppingBagRepository
    {
        Task AddItemToShoppingBagAsync(ShoppingBagItem item);
        Task ClearShoppingBagAsync(int shoppingBagId);
        Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag);
        Task<ShoppingBag?> GetShoppingBagAsync(int customerId);
        Task<ShoppingBagItem?> GetShoppingBagItemByIdAsync(int shoppingBagItemId);
        Task<decimal> GetShoppingBagTotalAsync(int customerId);
        Task RemoveItemFromShoppingBagAsync(ShoppingBagItem item);
        Task UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity);
    }
}