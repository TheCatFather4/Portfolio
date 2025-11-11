using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Result> RegisterCustomerAsync(string email, string identityId);
        Task<Result<Customer>> GetCustomerByEmailAsync(string identityId);
        Task<Result> UpdateCustomerAsync(Customer entity);
        Task<Result> GetDuplicateEmailAsync(string email);
        Task<Result> Register(RegisterRequest dto, string identityId);
    }
}