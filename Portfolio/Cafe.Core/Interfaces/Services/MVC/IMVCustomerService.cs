using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services.MVC
{
    public interface IMVCustomerService
    {
        Task<Result> RegisterCustomerAsync(string email, string identityId);
        Task<Result<Customer>> GetCustomerByEmailAsync(string identityId);
        Task<Result> UpdateCustomerAsync(Customer entity);
    }
}
