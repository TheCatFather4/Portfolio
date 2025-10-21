using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockMenuRepository : IMenuRepository
    {
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

        public async Task<ItemPrice> GetItemPriceByIdAsync(int itemId)
        {
            var price = new ItemPrice();
            return price;
        }

        public List<Item> GetItems()
        {
            var items = new List<Item>();

            items.Add(new Item
            {
                ItemID = 1,
                ItemName = "Food"
            });

            return items;
        }

        public Task<Item> GetItemWithPriceAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetMenu()
        {
            var items = new List<Item>();

            items.Add(new Item
            {
                ItemID = 1,
                ItemName = "Food"
            });

            return items;
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