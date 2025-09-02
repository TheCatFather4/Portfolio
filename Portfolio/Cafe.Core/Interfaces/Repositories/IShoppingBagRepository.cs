using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IShoppingBagRepository
    {
        Task<ShoppingBag> GetShoppingBagAsync(int customerId);
        Task AddItemAsync(int customerId, ShoppingBagItem item);
        Task RemoveItemAsync(int shoppingBagId, int shoppingBagItemId);
        Task UpdateItemQuantityAsync(int shoppingBagId, int shoppingBagItemId, byte quantity);
        Task ClearShoppingBag(int shoppingBagId);
    }
}
