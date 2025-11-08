using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IShoppingBagRepository
    {
        Task AddItemToShoppingBagAsync(ShoppingBagItem item);
        Task<ShoppingBag> GetShoppingBagAsync(int customerId);
        Task RemoveItemFromShoppingBagAsync(ShoppingBagItem item);
        Task UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity);
        Task ClearShoppingBag(int shoppingBagId);
        Task<ShoppingBagItem> GetShoppingBagItemByIdAsync(int shoppingBagItemId);
    }
}