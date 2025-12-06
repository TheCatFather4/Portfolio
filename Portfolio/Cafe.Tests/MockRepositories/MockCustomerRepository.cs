using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            int id = 1;

            await Task.Delay(1000);
            return id;
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            var customers = new List<Customer>();

            var c1 = new Customer()
            {
                CustomerID = 1,
                FirstName = "Yugi",
                LastName = "Moto",
                Email = "Customer1@fwcafe.com",
                Id = "123abc"
            };

            var c2 = new Customer()
            {
                CustomerID = 2,
                FirstName = "Seto",
                LastName = "Kaiba",
                Email = "Huhuhuhuahaha@duelist.com",
                Id = "blueeyeswhitedragon"
            };

            customers.Add(c1);
            customers.Add(c2);

            await Task.Delay(1000);

            foreach (var c in customers)
            {
                if (c.Email == email)
                {
                    return c;
                }
            }

            return null;
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
            await Task.Delay(100);
            var updated = new Customer
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Id = customer.Id
            };
        }
    }
}