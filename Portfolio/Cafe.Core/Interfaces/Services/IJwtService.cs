using Microsoft.AspNetCore.Identity;

namespace Cafe.Core.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(IdentityUser user);
    }
}
