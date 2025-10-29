using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperMenuManagerRepository : IMenuManagerRepository
    {
        private readonly string _connectionString;

        public DapperMenuManagerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddItem(Item item)
        {
            int id = 0;

            using (var cn = new SqlConnection(_connectionString))
            {
                var itemSql = @"INSERT INTO Item (CategoryID, ItemName, ItemDescription, ItemStatusID, ItemImgPath) 
                                VALUES (@CategoryID, @ItemName, @ItemDescription, @ItemStatusID, @ItemImgPath);
                                SELECT SCOPE_IDENTITY();";

                var itemParameters = new
                {
                    item.CategoryID,
                    item.ItemName,
                    item.ItemDescription,
                    item.ItemStatusID,
                    item.ItemImgPath
                };

                id = cn.ExecuteScalar<int>(itemSql, itemParameters);

                var itemPriceSql = @"INSERT INTO ItemPrice (ItemID, TimeOfDayID, Price, StartDate, EndDate) 
                                     VALUES (@ItemID, @TimeOfDayID, @Price, @StartDate, @EndDate);";

                var itemPriceParameters = new
                {
                    ItemID = id,
                    item.Prices[0].TimeOfDayID,
                    item.Prices[0].Price,
                    item.Prices[0].StartDate,
                    item.Prices[0].EndDate
                };

                cn.Execute(itemPriceSql, itemPriceParameters);
            }
        }

        public void UpdateItem(Item item)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var itemSql = @"UPDATE Item SET 
                                ItemName = @ItemName,
                                ItemDescription = @ItemDescription
                                WHERE ItemID = @ItemID;";

                var itemParameters = new
                {
                    item.ItemName,
                    item.ItemDescription,
                    item.ItemID
                };

                cn.Execute(itemSql, itemParameters);

                var itemPriceSql = @"Update ItemPrice SET 
                                    Price = @Price 
                                    WHERE ItemID = @ItemID;";

                var itemPriceParameters = new
                {
                    item.Prices[0].Price,
                    item.ItemID
                };

                cn.Execute(itemPriceSql, itemPriceParameters);
            }
        }
    }
}