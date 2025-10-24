using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockMenuRepository : IMenuRepository
    {
        public List<Item> GetAllItems()
        {
            var items = new List<Item>();

            var item = new Item
            {
                ItemID = 1,
                ItemName = "Food",
                ItemDescription = "Tasty",
                CategoryID = 1,
                Prices = new List<ItemPrice>()
            };

            var price = new ItemPrice
            {
                ItemID = 1,
                TimeOfDayID = 1,
                Price = (decimal)2.50,
                StartDate = DateTime.Now,
                ItemPriceID = 1
            };

            item.Prices.Add(price);
            items.Add(item);

            return items;
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();

            categories.Add(new Category
            {
                CategoryID = 1,
                CategoryName = "Beverages"
            });

            return categories;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            var item = new Item
            {
                ItemID = itemId
            };

            var prices = new List<ItemPrice>();

            var price = new ItemPrice();
            prices.Add(price);

            item.Prices = prices;

            return item;
        }

        public async Task<ItemPrice> GetItemPriceByItemIdAsync(int itemId)
        {
            var price = new ItemPrice();
            return price;
        }

        public List<TimeOfDay> GetTimeOfDays()
        {
            var times = new List<TimeOfDay>();

            times.Add(new TimeOfDay
            {
                TimeOfDayID = 1,
                TimeOfDayName = "Lunch"
            });

            return times;
        }
    }
}