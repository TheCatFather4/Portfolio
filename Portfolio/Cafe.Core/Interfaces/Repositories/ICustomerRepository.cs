using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomerAsync(Customer customer);
        Task<int> CreateShoppingBagAsync(ShoppingBag shoppingBag);
        Task UpdateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByEmailAsync(string email);
    }
}