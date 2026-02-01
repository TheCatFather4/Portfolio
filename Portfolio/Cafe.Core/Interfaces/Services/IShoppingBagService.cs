using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Requests;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IShoppingBagService
    {
        Task<Result> AddItemToShoppingBagAsync(AddItemToBagRequest dto);
        Task<Result> ClearShoppingBagAsync(int customerId);
        Task<Result<ShoppingBagResponse>> GetShoppingBagByCustomerIdAsync(int customerId);
        Task<Result<ShoppingBagItem>> GetShoppingBagItemByIdAsync(int shoppingBagItemId);
        Task<Result> RemoveItemFromShoppingBagAsync(int customerId, int shoppingBagItemId);
        Task<Result> UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity);
    }
}