using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<string?> GetEmailAddressAsync(string email);
        Task UpdateCustomerAsync(Customer customer);
    }
}