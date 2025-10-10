using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperMenuRepository : IMenuRepository
    {
        private readonly string _connectionString;

        public DapperMenuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Category";
                categories = cn.Query<Category>(sql).ToList();
            }

            return categories;
        }

        public async Task<ItemPrice> GetItemPriceByIdAsync(int itemId)
        {
            ItemPrice price = new ItemPrice();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM ItemPrice AS ip WHERE ip.ItemID = @ItemID;";

                var parameter = new
                {
                    ItemID = itemId
                };

                price = await cn.QueryFirstOrDefaultAsync<ItemPrice>(sql, parameter);
            }

            return price;
        }

        public List<Item> GetItems()
        {
            List<Item> items = new List<Item>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Item";
                items = cn.Query<Item>(sql).ToList();
            }

            return items;
        }

        public async Task<Item> GetItemWithPriceAsync(int itemId)
        {
            Item item = new Item();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Item AS i 
                            WHERE i.ItemID = @ItemID;

                            SELECT * FROM ItemPrice AS ip 
                            WHERE ip.ItemID = @ItemID;";

                var parameters = new
                {
                    ItemID = itemId
                };

                using (var multi = cn.QueryMultiple(sql, parameters))
                {
                    item = multi.ReadFirst<Item>();
                    item.Prices = multi.Read<ItemPrice>().ToList();
                }
            }

            return item;
        }

        public List<Item> GetMenu()
        {
            var itemDictionary = new Dictionary<int, Item>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Item AS i INNER JOIN ItemPrice AS ip ON ip.ItemID = i.ItemID ORDER BY i.ItemID";

                cn.Query<Item, ItemPrice, Item>(
                    sql,
                    (item, price) =>
                    {
                        if (!itemDictionary.TryGetValue((int)item.ItemID, out var currentItem))
                        {
                            currentItem = item;
                            itemDictionary.Add((int)currentItem.ItemID, currentItem);
                        }

                        if (price != null)
                        {
                            if (currentItem.Prices == null)
                            {
                                currentItem.Prices = new List<ItemPrice>();
                            }

                            currentItem.Prices.Add(price);
                        }

                        return currentItem;
                    },

                    splitOn: "ItemPriceID"
                    );
            }

            return itemDictionary.Values.ToList();
        }

        public List<TimeOfDay> GetTimeOfDays()
        {
            List<TimeOfDay> timeOfDays = new List<TimeOfDay>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM TimeOfDay";
                timeOfDays = cn.Query<TimeOfDay>(sql).ToList();
            }

            return timeOfDays;
        }
    }
}
