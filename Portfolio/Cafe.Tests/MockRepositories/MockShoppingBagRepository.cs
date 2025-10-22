using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockShoppingBagRepository : IShoppingBagRepository
    {
        public Task APIAddItemAsync(int customerId, ShoppingBagItem item)
        {
            throw new NotImplementedException();
        }

        public Task ClearShoppingBag(int shoppingBagId)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingBag> GetShoppingBagAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingBagItem> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            throw new NotImplementedException();
        }

        public Task MVCAddItemAsync(ShoppingBagItem item)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemAsync(int shoppingBagId, int shoppingBagItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemQuantityAsync(int shoppingBagId, int shoppingBagItemId, byte quantity)
        {
            throw new NotImplementedException();
        }
    }
}