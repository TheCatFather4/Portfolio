﻿using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private readonly CafeContext _dbContext;

        public EFCustomerRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            _dbContext.Customer.Add(customer);
            await _dbContext.SaveChangesAsync();

            return customer.CustomerID;
        }

        public async Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag)
        {
            _dbContext.ShoppingBag.Add(shoppingBag);
            await _dbContext.SaveChangesAsync();

            return shoppingBag.ShoppingBagID;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
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