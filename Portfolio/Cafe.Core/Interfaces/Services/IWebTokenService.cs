using Microsoft.AspNetCore.Identity;

namespace Cafe.Core.Interfaces.Services
{
    public interface IWebTokenService
    {
        string GenerateToken(IdentityUser user);
    }
}