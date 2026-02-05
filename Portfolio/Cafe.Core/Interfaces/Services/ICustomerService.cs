using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Requests;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Result<NewCustomerResponse>> AddCustomerAsync(AddCustomerRequest dto);
        Task<Result<Customer>> GetCustomerByEmailAsync(string email);
        Task<Result> GetDuplicateEmailAsync(string email);
        Task<Result> UpdateCustomerAsync(Customer customer);
    }
}