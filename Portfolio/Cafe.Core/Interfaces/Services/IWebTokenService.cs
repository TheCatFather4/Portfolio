using Microsoft.AspNetCore.Identity;

namespace Cafe.Core.Interfaces.Services
{
    public interface IWebTokenService
    {
        Task<string> GenerateTokenAsync(IdentityUser user);
    }
}