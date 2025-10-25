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
                CategoryID = 1,
                ItemStatusID = 1,
                ItemName = "Food",
                ItemDescription = "Tasty",
                ItemImgPath = "food.jpg",
                Prices = new List<ItemPrice>()
            };

            var price = new ItemPrice
            {
                ItemPriceID = 1,
                ItemID = 1,
                TimeOfDayID = 1,
                Price = (decimal)2.50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            item.Prices.Add(price);

            items.Add(item);

            return items;
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();

            var category = new Category
            {
                CategoryID = 1,
                CategoryName = "Delicious"
            };

            categories.Add(category);

            return categories;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            var item = new Item
            {
                ItemID = itemId,
                CategoryID = 1,
                ItemStatusID = 1,
                ItemName = "Food",
                ItemDescription = "Tasty",
                ItemImgPath = "food.jpg",
                Prices = new List<ItemPrice>()
            };

            var price = new ItemPrice
            {
                ItemPriceID = 1,
                ItemID = itemId,
                TimeOfDayID = 1,
                Price = (decimal)2.50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            item.Prices.Add(price);

            return item;
        }

        public async Task<ItemPrice> GetItemPriceByItemIdAsync(int itemId)
        {
            var price = new ItemPrice
            {
                ItemPriceID = 1,
                ItemID = itemId,
                TimeOfDayID = 1,
                Price = (decimal)2.50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            return price;
        }

        public List<TimeOfDay> GetTimeOfDays()
        {
            var timeOfDays = new List<TimeOfDay>();

            var timeOfDay = new TimeOfDay
            {
                TimeOfDayID = 1,
                TimeOfDayName = "Morning"
            };

            timeOfDays.Add(timeOfDay);

            return timeOfDays;
        }
    }
}