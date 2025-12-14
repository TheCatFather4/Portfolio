using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Cafe.Tests.MockRepositories
{
    public class MockConfiguration : IConfiguration
    {
        private readonly Dictionary<string, string?> _configData = new()
        {
            { "Jwt:Key", "this-is-a-secret-key-long-enough-to-test-this-out" },
            { "Jwt:Issuer", "TestIssuer" },
            { "Jwt:Audience", "TestAudience" },
            { "Jwt:Expiration", "60" }
        };

        public string? this[string key]
        {
            get
            {
                if (_configData.TryGetValue(key, out var value))
                {
                    return value;
                }

                return null;
            }

            set => throw new NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            if (key == "Jwt:Expiration")
            {
                _configData.TryGetValue(key, out var value);

                return new MockConfigurationSection(key, value!);
            }

            throw new NotImplementedException();
        }
    }
}