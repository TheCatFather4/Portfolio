using Cafe.Core.Entities;

namespace Cafe.Data.Repositories.TrainingRepository
{
    public class FakeCafeDb
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category {CategoryID = 1, CategoryName = "Beverages"},
            new Category {CategoryID = 2, CategoryName = "Sandwiches"},
            new Category {CategoryID = 3, CategoryName = "Sides"},
        };

        public static List<Item> Items = new List<Item>
        {
            new Item {ItemID = 1, CategoryID = 1, ItemName = "Coffee", ItemDescription = "Freshly brewed with a distinctive aroma."},
            new Item {ItemID = 2, CategoryID = 1, ItemName = "Orange Juice", ItemDescription = "Refreshingly made by hand for a crisp taste."},
            new Item {ItemID = 3, CategoryID = 2, ItemName = "Egg Cheese Bagel", ItemDescription = "Scrambled eggs and cheese between a bagel."},
            new Item {ItemID = 4, CategoryID = 3, ItemName = "Hash Browns", ItemDescription = "Made from Yukon gold potatoes with a satisfying texture."},
            new Item {ItemID = 5, CategoryID = 3, ItemName = "Onion Rings", ItemDescription = "Crunchy onion rings with a delightfully sweet taste."},
            new Item {ItemID = 6, CategoryID = 1, ItemName = "Spooky Slushie", ItemDescription = "A frightfully delicious wildberry slush drink."},
            new Item {ItemID = 7, CategoryID = 2, ItemName = "BLT", ItemDescription = "Four strips of crispy applewood bacon, lettuce, and tomato between toast."}
        };

        public static List<ItemPrice> Prices = new List<ItemPrice>
        {
            new ItemPrice {ItemPriceID = 1, ItemID = 1, TimeOfDayID = 1, Price = 3.50M, StartDate = new DateTime(2024, 1, 1)},
            new ItemPrice {ItemPriceID = 2, ItemID = 2, TimeOfDayID = 1, Price = 2.50M, StartDate = new DateTime(2024, 1, 1)},
            new ItemPrice {ItemPriceID = 3, ItemID = 3, TimeOfDayID = 1, Price = 4.50M, StartDate = new DateTime(2024, 1, 1)},
            new ItemPrice {ItemPriceID = 4, ItemID = 1, TimeOfDayID = 2, Price = 2.00M, StartDate = new DateTime(2024, 1, 1)},
            new ItemPrice {ItemPriceID = 5, ItemID = 4, TimeOfDayID = 1, Price = 1.00M, StartDate = new DateTime(2024, 1, 1)},
            new ItemPrice {ItemPriceID = 6, ItemID = 5, TimeOfDayID = 2, Price = 2.00M, StartDate = new DateTime(2024, 1, 1)},
            new ItemPrice {ItemPriceID = 7, ItemID = 6, TimeOfDayID = 3, Price = 2.50M, StartDate = new DateTime(2024, 10, 1), EndDate = new DateTime(2024, 10, 31)},
            new ItemPrice {ItemPriceID = 8, ItemID = 7, TimeOfDayID = 2, Price = 4.00M, StartDate = new DateTime(2024, 8, 10)}
        };

        public static List<TimeOfDay> TimeOfDays = new List<TimeOfDay>
        {
            new TimeOfDay {TimeOfDayID = 1, TimeOfDayName = "Breakfast"},
            new TimeOfDay {TimeOfDayID = 2, TimeOfDayName = "Lunch"},
            new TimeOfDay {TimeOfDayID = 3, TimeOfDayName = "Dinner"}
        };

        public static List<Server> Servers = new List<Server>
        {
            new Server {ServerID = 1, FirstName = "Luke", LastName = "McGluke", HireDate = new DateTime(2024, 5, 10), DoB = new DateTime(2000, 7, 25)},
            new Server {ServerID = 2, FirstName = "Yotototee", LastName = "Yotototaa", HireDate = new DateTime(2024, 5, 10), DoB = new DateTime(2002, 10, 3)},
            new Server {ServerID = 3, FirstName = "Schwepper", LastName = "Dr. Pepper", HireDate = new DateTime(2024, 5, 10), DoB = new DateTime(1998, 8, 30)}
        };
    }
}
