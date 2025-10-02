using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Data.Repositories.TrainingRepository
{
    public class TrainingMenuRepository : IMenuRepository
    {
        public List<Category> GetCategories()
        {
            var categories = new List<Category>();

            categories = FakeCafeDb.Categories
                .ToList();

            return categories;
        }

        public List<TimeOfDay> GetTimeOfDays()
        {
            var times = new List<TimeOfDay>();

            times = FakeCafeDb.TimeOfDays
                .ToList();

            return times;
        }

        public List<Item> GetMenu()
        {
            var items = FakeCafeDb.Items
                .ToList();

            foreach (var item in items)
            {
                item.Prices = FakeCafeDb.Prices
                    .Where(p => p.ItemID == item.ItemID)
                    .ToList();
            }

            return items;
        }

        public Item GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<ItemPrice> GetItemPriceByIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemWithPriceAsync(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
