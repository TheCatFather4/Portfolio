using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CafeContext _dbContext;

        public CustomerRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _dbContext.Customer.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateShoppingBagAsync(ShoppingBag shoppingBag)
        {
            _dbContext.ShoppingBag.Add(shoppingBag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByEmailAsync(string identityName)
        {
            return await _dbContext.Customer
                .FirstOrDefaultAsync(c => c.Email == identityName);
        }

        public async Task<Customer> GetCustomerByNewEmailAsync(string email)
        {
            return await _dbContext.Customer
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customer.Update(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
