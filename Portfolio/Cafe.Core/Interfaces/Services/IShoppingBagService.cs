using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IShoppingBagService
    {
        Task<Result> AddItemToBagAsync(int customerId, int itemId, byte quantity);
        Task<Result<ShoppingBag>> GetShoppingBagAsync(int customerId);
        Task<Result> RemoveItemFromBagAsync(int customerId, int shoppingBagItemId);
        Task<Result> UpdateItemQuantityAsync(int customerId, int shoppingBagItemId, byte quantity);
        Task<Result> ClearShoppingBagAsync(int customerId);
    }
}
