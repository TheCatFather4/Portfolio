using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperShoppingBagRepository : IShoppingBagRepository
    {
        private readonly string _connectionString;

        public DapperShoppingBagRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddItemToShoppingBagAsync(ShoppingBagItem item)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var existingItemSql = @"SELECT * FROM ShoppingBagItem 
                                        WHERE ShoppingBagID = @ShoppingBagID AND ItemID = @ItemID;";

                var existingItemParameters = new
                {
                    item.ShoppingBagID,
                    item.ItemID
                };

                var existingItem = cn.QueryFirstOrDefaultAsync<ShoppingBagItem>(existingItemSql, existingItemParameters);

                if (existingItem != null)
                {
                    var updateSql = @"UPDATE ShoppingBagItem SET 
                                        Quantity = Quantity + @Quantity 
                                        WHERE ShoppingBagID = @ShoppingBagID AND ItemID = @ItemID;";

                    var updateParameters = new
                    {
                        item.Quantity,
                        item.ShoppingBagID,
                        item.ItemID
                    };

                    await cn.ExecuteAsync(updateSql, updateParameters);
                }
                else
                {
                    var sql = @"INSERT INTO ShoppingBagItem (ShoppingBagID, ItemID, Quantity, ItemName, Price, ItemStatusID, ItemImgPath) 
                                VALUES (@ShoppingBagID, @ItemID, @Quantity, @ItemName, @Price, @ItemStatusID, @ItemImgPath);";

                    var parameters = new
                    {
                        item.ShoppingBagID,
                        item.ItemID,
                        item.Quantity,
                        item.ItemName,
                        item.Price,
                        item.ItemStatusID,
                        item.ItemImgPath
                    };

                    await cn.ExecuteAsync(sql, parameters);
                }
            }
        }

        public async Task ClearShoppingBagAsync(int shoppingBagId)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE ShoppingBagItem 
                            WHERE ShoppingBagID = @ShoppingBagID;";

                var parameter = new
                {
                    ShoppingBagID = shoppingBagId
                };

                await cn.ExecuteAsync(sql, parameter);
            }
        }

        public async Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag)
        {
            int id = 0;

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO ShoppingBag (CustomerID) 
                            VALUES (@CustomerID);
                            SELECT SCOPE_IDENTITY();";

                var parameter = new
                {
                    shoppingBag.CustomerID
                };

                id = await cn.ExecuteScalarAsync<int>(sql, parameter);
            }

            return id;
        }

        public async Task<ShoppingBag?> GetShoppingBagAsync(int customerId)
        {
            ShoppingBag? sb = new ShoppingBag();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM ShoppingBag
                            WHERE CustomerID = @CustomerID;";

                var itemsql = @"SELECT * FROM ShoppingBagItem AS sbi 
                                INNER JOIN ShoppingBag AS sb ON sb.ShoppingBagID = sbi.ShoppingBagID
                                WHERE sb.CustomerID = @CustomerID;";

                var parameter = new
                {
                    CustomerID = customerId
                };

                sb = await cn.QueryFirstOrDefaultAsync<ShoppingBag>(sql, parameter);

                if (sb != null)
                {
                    sb.Items = (List<ShoppingBagItem>?)await cn.QueryAsync<ShoppingBagItem>(itemsql, parameter);
                }
            }

            return sb;
        }

        public async Task<ShoppingBagItem> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            ShoppingBagItem sbi = new ShoppingBagItem();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM ShoppingBagItem 
                            WHERE ShoppingBagItemID = @ShoppingBagItemID;";

                var parameter = new
                {
                    shoppingBagItemId
                };

                sbi = await cn.QueryFirstOrDefaultAsync<ShoppingBagItem>(sql, parameter);
            }

            return sbi;
        }

        public async Task<decimal> GetShoppingBagTotalAsync(int customerId)
        {
            decimal total = 0;

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT SUM(sbi.Price * sbi.Quantity) FROM ShoppingBag AS sb 
                            INNER JOIN ShoppingBagItem AS sbi ON sb.ShoppingBagID = sbi.ShoppingBagID 
                            WHERE sb.CustomerID = @CustomerID;";

                var parameter = new
                {
                    CustomerID = customerId
                };

                total = await cn.ExecuteScalarAsync<decimal>(sql, parameter);
            }

            return total;
        }

        public async Task RemoveItemFromShoppingBagAsync(ShoppingBagItem item)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE ShoppingBagItem 
                            WHERE ShoppingBagItemID = @ShoppingBagItemID;";

                var parameter = new
                {
                    item.ShoppingBagItemID
                };

                await cn.ExecuteAsync(sql, parameter);
            }
        }

        public async Task UpdateItemQuantityAsync(int shoppingBagItemId, byte quantity)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"Update ShoppingBagItem SET 
                            Quantity = @Quantity 
                            WHERE ShoppingBagItemID = @ShoppingBagItemID;";

                var parameters = new
                {
                    ShoppingBagItemID = shoppingBagItemId,
                    Quantity = quantity
                };

                await cn.ExecuteAsync(sql, parameters);
            }
        }
    }
}