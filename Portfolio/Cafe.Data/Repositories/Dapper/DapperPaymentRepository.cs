using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    /// <summary>
    /// Handles data persistence concerning Payment entities.
    /// Implements IPaymentRepository by utilizing Dapper Micro-ORM.
    /// </summary>
    public class DapperPaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public DapperPaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddPaymentAsync(Payment payment)
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

                await cn.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<decimal> GetFinalTotalAsync(int orderId)
        {
            decimal total = 0;

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT FinalTotal FROM CafeOrder WHERE OrderID = @OrderID;";

                var parameter = new
                {
                    orderId,
                };

                total = await cn.ExecuteAsync(sql, parameter);
            }

            return total;
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            var paymentTypes = new List<PaymentType>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM PaymentType;";

                paymentTypes = (await cn.QueryAsync<PaymentType>(sql)).ToList();
            }

            return paymentTypes;
        }
    }
}