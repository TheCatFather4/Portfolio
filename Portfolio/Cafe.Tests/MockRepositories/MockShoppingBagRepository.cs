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

        public async Task ClearShoppingBagAsync(int shoppingBagId)
        {
            var sb = new ShoppingBag();
            await Task.Delay(1000);
        }

        public async Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag)
        {
            int id = 1;

            await Task.Delay(100);
            return id;
        }

        public async Task<ShoppingBag?> GetShoppingBagAsync(int customerId)
        {
            if (customerId == 1)
            {
                var sb = new ShoppingBag
                {
                    ShoppingBagID = 1,
                    CustomerID = customerId,
                    Items = new List<ShoppingBagItem>()
                };

                var item1 = new ShoppingBagItem
                {
                    ShoppingBagItemID = 1,
                    ShoppingBagID = 1,
                    ItemID = 1,
                    Quantity = 3,
                    ItemName = "Food",
                    Price = 5.00M
                };

                var item2 = new ShoppingBagItem
                {
                    ShoppingBagItemID = 2,
                    ShoppingBagID = 1,
                    ItemID = 2,
                    Quantity = 3,
                    ItemName = "Desert",
                    Price = 3.00M
                };

                sb.Items.Add(item1);
                sb.Items.Add(item2);

                await Task.Delay(1000);
                return sb;
            }

            return null;
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