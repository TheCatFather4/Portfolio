using Cafe.Core.DTOs;

namespace Cafe.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Result> Register(RegisterUser dto, string identityId);
    }
}
