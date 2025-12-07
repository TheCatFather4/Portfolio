using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperMenuRetrievalRepository : IMenuRetrievalRepository
    {
        private readonly string _connectionString;

        public DapperMenuRetrievalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            var itemDictionary = new Dictionary<int, Item>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Item AS i INNER JOIN ItemPrice AS ip ON ip.ItemID = i.ItemID ORDER BY i.ItemID";

                await cn.QueryAsync<Item, ItemPrice, Item>(
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

        public async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> categories = new List<Category>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Category";
                categories = (await cn.QueryAsync<Category>(sql)).ToList();
            }

            return categories;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
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

        public async Task<ItemPrice> GetItemPriceByItemIdAsync(int itemId)
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

        public List<Item> GetItemsByCategoryId(int categoryId)
        {
            List<Item> items = new List<Item>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Item AS ip WHERE ip.CategoryID = @CategoryID;";

                var parameter = new
                {
                    CategoryID = categoryId
                };

                items = cn.Query<Item>(sql, parameter).ToList();
            }

            return items;
        }

        public async Task<List<TimeOfDay>> GetTimeOfDaysAsync()
        {
            List<TimeOfDay> timeOfDays = new List<TimeOfDay>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM TimeOfDay";
                timeOfDays = (await cn.QueryAsync<TimeOfDay>(sql)).ToList();
            }

            return timeOfDays;
        }
    }
}