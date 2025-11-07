using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IShoppingBagRepository
    {
        Task AddItemToShoppingBagAsync(ShoppingBagItem item);
        Task<ShoppingBag> GetShoppingBagAsync(int customerId);
        Task UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity);
        Task RemoveItemAsync(int shoppingBagId, int shoppingBagItemId);
        Task ClearShoppingBag(int shoppingBagId);
        Task<ShoppingBagItem> GetShoppingBagItemByIdAsync(int shoppingBagItemId);
    }
}