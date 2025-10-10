using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperPaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public DapperPaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPayment(Payment payment)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Payment (OrderID, PaymentTypeID, Amount, TransactionDate, PaymentStatusID) 
                            VALUES (@OrderID, @PaymentTypeID, @Amount, @TransactionDate, @PaymentStatusID);";

                var parameters = new
                {
                    payment.OrderID,
                    payment.PaymentTypeID,
                    payment.Amount,
                    payment.TransactionDate,
                    payment.PaymentStatusID
                };

                cn.Execute(sql, parameters);
            }
        }

        public CafeOrder GetOrderById(int orderId)
        {
            CafeOrder order = new CafeOrder();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM CafeOrder 
                            WHERE OrderID = @OrderID;";

                var parameter = new
                {
                    OrderID = orderId
                };

                order = cn.QueryFirstOrDefault<CafeOrder>(sql, parameter);
            }

            return order;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            List<PaymentType> pts = new List<PaymentType>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM PaymentType;";

                pts = cn.Query<PaymentType>(sql).ToList();
            }

            return pts;
        }

        public void UpdateOrderStatus(CafeOrder order)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE CafeOrder SET 
                                PaymentStatusID = @PaymentStatusID 
                            WHERE OrderID = @OrderID;";

                var parameters = new
                {
                    order.PaymentStatusID,
                    order.OrderID
                };

                cn.Execute(sql, parameters);
            }
        }
    }
}