using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            int id = 1;
            return id;
        }

        public async Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag)
        {
            int id = 1;
            return id;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            if (email == "Customer1@fwcafe.com")
            {
                var customer = new Customer
                {
                    CustomerID = 1,
                    Email = email,
                    FirstName = "Test",
                    LastName = "Tester"
                };

                return customer;
            }

            return new Customer
            {
                CustomerID = 0,
                Email = "Null",
                FirstName = "Null",
                LastName = "Null"
            };
        }

        public async Task<string?> GetEmailAddressAsync(string email)
        {
            if (email == "Customer1@fwcafe.com")
            {
                await Task.Delay(1000);
                return "Customer1@fwcafe.com";
            }

            return null;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var c1 = new Customer();
        }
    }
}