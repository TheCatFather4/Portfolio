using Cafe.Core.DTOs;

namespace Cafe.Core.Interfaces.Services.MVC
{
    public interface IMVCustomerService
    {
        Task<Result> RegisterCustomerAsync(string email, string identityId);
    }
}
