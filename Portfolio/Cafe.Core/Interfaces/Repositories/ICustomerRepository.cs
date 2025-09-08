using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(Customer customer);
        Task CreateShoppingBagAsync(ShoppingBag shoppingBag);
        Task UpdateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByEmailAsync(string identityName);
    }
}
