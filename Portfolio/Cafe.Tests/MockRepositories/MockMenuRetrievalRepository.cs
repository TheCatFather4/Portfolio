using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockMenuRetrievalRepository : IMenuRetrievalRepository
    {
        public async Task<List<Item>> GetAllItemsAsync()
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

            var item2 = new Item
            {
                ItemID = 2,
                CategoryID = 2,
                ItemStatusID = 2,
                ItemName = "Drink",
                ItemDescription = "Refreshing",
                ItemImgPath = "drink.jpg",
                Prices = new List<ItemPrice>()
            };

            var price2 = new ItemPrice
            {
                ItemPriceID = 2,
                ItemID = 2,
                TimeOfDayID = 2,
                Price = (decimal)3.50,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now.AddDays(3)
            };

            item2.Prices.Add(price2);
            items.Add(item2);

            var item3 = new Item
            {
                ItemID = 3,
                CategoryID = 3,
                ItemStatusID = 3,
                ItemName = "Cake",
                ItemDescription = "Sweet",
                ItemImgPath = "cake.jpg",
                Prices = new List<ItemPrice>()
            };

            var price3 = new ItemPrice
            {
                ItemPriceID = 3,
                ItemID = 3,
                TimeOfDayID = 3,
                Price = (decimal)4.50,
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(4)
            };

            item3.Prices.Add(price3);
            items.Add(item3);

            await Task.Delay(1000);
            return items;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = new List<Category>();

            var category = new Category
            {
                CategoryID = 1,
                CategoryName = "Delicious"
            };

            categories.Add(category);

            await Task.Delay(1000);
            return categories;
        }

        public async Task<Item?> GetItemByIdAsync(int itemId)
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

            if (item.ItemID != 1)
            {
                return null;
            }

            await Task.Delay(1000);
            return item;
        }

        public async Task<ItemPrice?> GetItemPriceByItemIdAsync(int itemId)
        {
            var prices = new List<ItemPrice>();

            var price1 = new ItemPrice
            {
                ItemPriceID = 1,
                ItemID = 1,
                TimeOfDayID = 1,
                Price = (decimal)5.00,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                OrderItems = new List<OrderItem>()

            };

            var price2 = new ItemPrice
            {
                ItemPriceID = 2,
                ItemID = 2,
                TimeOfDayID = 1,
                Price = (decimal)3.00,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                OrderItems = new List<OrderItem>()
            };

            prices.Add(price1);
            prices.Add(price2);

            foreach (var p in prices)
            {
                if (itemId == p.ItemID)
                {
                    await Task.Delay(1000);
                    return p;
                }
            }

            return null;
        }

        public async Task<List<Item>> GetItemsByCategoryIdAsync(int categoryId)
        {
            var items = new List<Item>();

            var item = new Item
            {
                ItemID = 1,
                CategoryID = 1,
                ItemStatusID = 1,
                ItemName = "Soda",
                ItemDescription = "Refreshing",
                ItemImgPath = "soda.jpg"
            };

            var item2 = new Item
            {
                ItemID = 2,
                CategoryID = 1,
                ItemStatusID = 1,
                ItemName = "Tea",
                ItemDescription = "Crisp",
                ItemImgPath = "tea.jpg"
            };

            if (categoryId == 1)
            {
                items.Add(item);
                items.Add(item2);
            }

            await Task.Delay(1000);
            return items;
        }

        public async Task<List<TimeOfDay>> GetTimeOfDaysAsync()
        {
            var timeOfDays = new List<TimeOfDay>();

            var timeOfDay = new TimeOfDay
            {
                TimeOfDayID = 1,
                TimeOfDayName = "Morning"
            };

            timeOfDays.Add(timeOfDay);

            await Task.Delay(1000);
            return timeOfDays;
        }
    }
}