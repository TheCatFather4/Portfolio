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

        public Task AddItemAsync(int customerId, ShoppingBagItem item)
        {
            throw new NotImplementedException();
        }

        public Task ClearShoppingBag(int shoppingBagId)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingBag> GetShoppingBagAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingBagItem> GetShoppingBagItemByIdAsync(int shoppingBagItemId)
        {
            throw new NotImplementedException();
        }

        public async Task MVCAddItemAsync(ShoppingBagItem item)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var quantitySql = @"SELECT Quantity FROM ShoppingBagItem 
                                WHERE ShoppingBagID = @ShoppingBagID AND ItemID = @ItemID;";

                var parameter = new
                {
                    item.ShoppingBagID,
                    item.ItemID
                };

                var quantity = await cn.ExecuteScalarAsync<byte>(quantitySql, parameter);

                if (quantity > 0)
                {
                    var updateSql = @"UPDATE ShoppingBagItem SET 
                                        Quantity = Quantity + @Quantity 
                                        WHERE ShoppingBagID = @ShoppingBagID AND ItemID = @ItemID;";

                    var quantityParameters = new
                    {
                        item.Quantity,
                        item.ShoppingBagID,
                        item.ItemID
                    };

                    await cn.ExecuteAsync(updateSql, quantityParameters);
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

        public Task RemoveItemAsync(int shoppingBagId, int shoppingBagItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemQuantityAsync(int shoppingBagId, int shoppingBagItemId, byte quantity)
        {
            throw new NotImplementedException();
        }
    }
}
