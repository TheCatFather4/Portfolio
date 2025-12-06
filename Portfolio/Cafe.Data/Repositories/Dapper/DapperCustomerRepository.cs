using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperCustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public DapperCustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            int id = 0;

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Customer (FirstName, LastName, Email, Id, ShoppingBagID) 
                            VALUES (@FirstName, @LastName, @Email, @Id, @ShoppingBagID);
                            SELECT SCOPE_IDENTITY();";

                var parameters = new
                {
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Id,
                    customer.ShoppingBagID
                };

                id = await cn.ExecuteScalarAsync<int>(sql, parameters);
            }

            return id;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            Customer customer = new Customer();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Customer AS c WHERE c.Email = @Email;";

                var parameter = new
                {
                    Email = email
                };

                customer = await cn.QueryFirstOrDefaultAsync<Customer>(sql, parameter);
            }

            return customer;
        }

        public async Task<string?> GetEmailAddressAsync(string email)
        {
            string? duplicate;

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT Email FROM Customer AS c WHERE c.Email = @Email;";

                var parameter = new
                {
                    Email = email
                };

                duplicate = await cn.QueryFirstOrDefaultAsync<string>(sql, parameter);
            }

            return duplicate;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Customer SET 
                            FirstName = @FirstName,
                            LastName = @LastName,
                            ShoppingBagID = @ShoppingBagID
                            WHERE CustomerID = @CustomerID;";

                var parameters = new
                {
                    customer.FirstName,
                    customer.LastName,
                    customer.ShoppingBagID,
                    customer.CustomerID
                };

                await cn.ExecuteAsync(sql, parameters);
            }
        }
    }
}