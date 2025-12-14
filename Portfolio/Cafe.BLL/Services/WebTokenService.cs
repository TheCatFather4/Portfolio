using Cafe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning the generation of JSON Web Tokens.
    /// </summary>
    public class WebTokenService : IWebTokenService
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructs a service with the dependencies required for JSON Web Token generation.
        /// </summary>
        /// <param name="config">A dependency used for JWT generation.</param>
        public WebTokenService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generates a JSON Web Token that is associated with specific user claims.
        /// Note: Async is simulated in this method. It was incorporated for consistency.
        /// </summary>
        /// <param name="user">A user record associated with ASP.NET Core Identity.</param>
        /// <returns>A string in the form of a JSON Web Token.</returns>
        public async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(_config.GetValue<int>("Jwt:Expiration")),
                signingCredentials: credentials
                );

            await Task.Delay(1000);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}