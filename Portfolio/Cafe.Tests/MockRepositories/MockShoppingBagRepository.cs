using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockShoppingBagRepository : IShoppingBagRepository
    {
        public Task AddItemToShoppingBagAsync(ShoppingBagItem item)
        {
            throw new NotImplementedException();
        }

        public Task ClearShoppingBagAsync(int shoppingBagId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag)
        {
            int id = 1;

            await Task.Delay(100);
            return id;
        }

        public Task<ShoppingBag> GetShoppingBagAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingBagItem> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetShoppingBagTotalAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemFromShoppingBagAsync(ShoppingBagItem item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity)
        {
            throw new NotImplementedException();
        }
    }
}