using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IShoppingBagService
    {
        Task<Result> AddItemToShoppingBagAsync(AddItemRequest dto);
        Task<Result> ClearShoppingBagAsync(int customerId);
        Task<Result<ShoppingBagResponse>> GetShoppingBagByCustomerIdAsync(int customerId);
        Task<Result<ShoppingBagItem>> GetShoppingBagItemByIdAsync(int shoppingBagItemId);
        Task<Result> RemoveItemFromShoppingBagAsync(int customerId, int shoppingBagItemId);
        Task<Result> UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity);
    }
}