using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperOrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public DapperOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items)
        {
            int id = 0;

            using (var cn = new SqlConnection(_connectionString))
            {
                var orderSql = @"INSERT INTO CafeOrder (ServerID, PaymentTypeID, OrderDate, SubTotal, Tax, Tip, FinalTotal, CustomerID, PaymentStatusID) 
                                 VALUES (@ServerID, @PaymentTypeID, @OrderDate, @SubTotal, @Tax, @Tip, @FinalTotal, @CustomerID, @PaymentStatusID);
                                 SELECT SCOPE_IDENTITY();";

                var orderParameters = new
                {
                    order.ServerID,
                    order.PaymentTypeID,
                    order.OrderDate,
                    order.SubTotal,
                    order.Tax,
                    order.Tip,
                    order.FinalTotal,
                    order.CustomerID,
                    order.PaymentStatusID
                };

                id = await cn.ExecuteScalarAsync<int>(orderSql, orderParameters);

                // add list of order items
                foreach (var item in items)
                {
                    var itemSql = @"INSERT INTO OrderItem (OrderID, ItemPriceID, Quantity, ExtendedPrice) 
                                    VALUES (@OrderID, @ItemPriceID, @Quantity, @ExtendedPrice);";

                    var itemParameters = new
                    {
                        OrderID = id,
                        item.ItemPriceID,
                        item.Quantity,
                        item.ExtendedPrice
                    };

                    await cn.ExecuteAsync(itemSql, itemParameters);
                }
            }

            order.OrderID = id;

            return order;
        }

        public async Task<CafeOrder> GetOrderByIdAsync(int orderId)
        {
            CafeOrder order = new CafeOrder();

            using (var cn = new SqlConnection(_connectionString))
            {
                var orderSql = @"SELECT * FROM CafeOrder 
                                WHERE OrderID = @OrderID";

                var orderParameter = new
                {
                    OrderID = orderId
                };

                order = await cn.QueryFirstOrDefaultAsync<CafeOrder>(orderSql, orderParameter);

                var itemsSql = @"SELECT * FROM OrderItem 
                                WHERE OrderID = @OrderID";

                order.OrderItems = cn.Query<OrderItem>(itemsSql, orderParameter).ToList();
            }

            return order;
        }

        public async Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId)
        {
            List<CafeOrder> orders = new List<CafeOrder>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM CafeOrder 
                            WHERE CustomerID = @CustomerID;";

                var parameter = new
                {
                    CustomerID = customerId
                };

                orders = cn.Query<CafeOrder>(sql, parameter).ToList();

                var itemSql = @"SELECT * FROM OrderItem
                                WHERE OrderID = @OrderID;";

                foreach (var order in orders)
                {
                    var itemParameter = new
                    {
                        order.OrderID,
                    };

                    order.OrderItems = cn.Query<OrderItem>(itemSql, itemParameter).ToList();
                }
            }

            return orders;
        }
    }
}