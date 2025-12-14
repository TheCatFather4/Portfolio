using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockShoppingBagRepository : IShoppingBagRepository
    {
        public async Task AddItemToShoppingBagAsync(ShoppingBagItem item)
        {
            var items = new List<ShoppingBagItem>();
            items.Add(item);
            await Task.Delay(1000);
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

        public async Task<ShoppingBagItem?> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            if (shoppingBagItemId == 1)
            {
                var sbi = new ShoppingBagItem
                {
                    ShoppingBagItemID = 1,
                    ShoppingBagID = 1,
                    ItemID = 1,
                    ItemStatusID = 1,
                    Quantity = 1,
                    ItemName = "Coffee",
                    Price = 2.00M,
                    ItemImgPath = "coffee.jpg"
                };

                await Task.Delay(1000);
                return sbi;
            }

            return null;
        }

        public async Task<decimal> GetShoppingBagTotalAsync(int customerId)
        {
            if (customerId == 1)
            {
                await Task.Delay(1000);
                return 25.00M;
            }

            else return 0;
        }

        public async Task RemoveItemFromShoppingBagAsync(ShoppingBagItem item)
        {
            await Task.Delay(1000);
        }

        public async Task UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity)
        {
            await Task.Delay(1000);
        }
    }
}