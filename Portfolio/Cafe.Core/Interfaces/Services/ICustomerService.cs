using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Result> AddCustomerAsync(AddCustomerRequest dto);
        Task<Result<Customer>> GetCustomerByEmailAsync(string email);
        Task<Result> GetDuplicateEmailAsync(string email);
        Task<Result> UpdateCustomerAsync(Customer customer);
    }
}