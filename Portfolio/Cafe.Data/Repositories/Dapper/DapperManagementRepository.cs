using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperManagementRepository : IManagementRepository
    {
        private readonly string _connectionString;

        public DapperManagementRepository(string connectionString)
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

        public void AddServer(Server server)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Server] (FirstName, LastName, HireDate, TermDate, DoB) 
                            VALUES (@FirstName, @LastName, @HireDate, @TermDate, @DoB);";

                var parameters = new
                {
                    server.FirstName,
                    server.LastName,
                    server.HireDate,
                    server.TermDate,
                    server.DoB
                };

                cn.Execute(sql, parameters);
            }
        }

        public Item GetMenuItemById(int itemID)
        {
            Item item = new Item();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Item AS i 
                            WHERE i.ItemID = @ItemID;

                            SELECT * FROM ItemPrice AS ip 
                            WHERE ip.ItemID = @ItemID;";

                var parameter = new
                {
                    ItemID = itemID
                };

                using (var multi = cn.QueryMultiple(sql, parameter))
                {
                    item = multi.ReadFirst<Item>();
                    item.Prices = multi.Read<ItemPrice>().ToList();
                }
            }

            return item;
        }

        public Server GetServerById(int serverID)
        {
            Server server = new Server();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM [Server] AS s WHERE s.ServerID = @ServerID;";

                var parameter = new
                {
                    ServerID = serverID,
                };

                server = cn.QueryFirstOrDefault<Server>(sql, parameter);
            }

            return server;
        }

        public List<Server> GetServers()
        {
            List<Server> servers = new List<Server>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Server];";

                servers = cn.Query<Server>(sql).ToList();
            }

            return servers;
        }

        public bool IsDuplicateItem(string itemName)
        {
            bool result = false;

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Item 
                            WHERE ItemName = @ItemName;";

                var parameter = new
                {
                    itemName,
                };

                var item = cn.QueryFirstOrDefault<Item>(sql, parameter);

                if (item != null)
                {
                    result = true;
                }
            }

            return result;
        }

        public void UpdateMenu(Item item)
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

        public void UpdateServer(Server server)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Server] SET 
                            FirstName = @FirstName, 
                            LastName = @LastName, 
                            DoB = @DoB, 
                            HireDate = @HireDate, 
                            TermDate = @TermDate 
                            WHERE ServerID = @ServerID;";

                var parameters = new
                {
                    server.ServerID,
                    server.FirstName,
                    server.LastName,
                    server.DoB,
                    server.HireDate,
                    server.TermDate
                };

                cn.Execute(sql, parameters);
            }
        }
    }
}