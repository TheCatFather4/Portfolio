using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperAccountantRepository : IAccountantRepository
    {
        private readonly string _connectionString;

        public DapperAccountantRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ItemPrice GetItemPriceByItemId(int itemId)
        {
            ItemPrice price = new ItemPrice();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM ItemPrice AS ip WHERE ip.ItemID = @ItemID;";

                var parameter = new
                {
                    ItemID = itemId
                };

                price = cn.QueryFirstOrDefault<ItemPrice>(sql, parameter);
            }

            return price;
        }

        public List<Item> GetItemsByCategoryID(int categoryID)
        {
            List<Item> items = new List<Item>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Item AS ip WHERE ip.CategoryID = @CategoryID;";

                var parameter = new
                {
                    CategoryID = categoryID
                };

                items = cn.Query<Item>(sql, parameter).ToList();
            }

            return items;
        }

        public List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT oi.*, co.* FROM OrderItem AS oi 
                            INNER JOIN CafeOrder AS co ON co.OrderID = oi.OrderID 
                            WHERE co.PaymentStatusID = 1 AND oi.ItemPriceID = @ItemPriceID;";

                var parameter = new
                {
                    ItemPriceID = itemPriceId
                };

                orderItems = cn.Query<OrderItem, CafeOrder, OrderItem>(
                    sql,
                    (orderItem, cafeOrder) =>
                    {
                        orderItem.CafeOrder = cafeOrder;
                        return orderItem;
                    },
                    parameter,
                    splitOn: "OrderID")
                    .ToList();
            }

            return orderItems;
        }

        public List<CafeOrder> GetOrders()
        {
            List<CafeOrder> orders = new List<CafeOrder>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM CafeOrder;";

                orders = cn.Query<CafeOrder>(sql).ToList();
            }

            return orders;
        }
    }
}