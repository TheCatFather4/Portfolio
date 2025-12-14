using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Cafe.Tests.MockRepositories
{
    public class MockConfigurationSection : IConfigurationSection
    {
        private readonly string _key;
        private readonly string _value;

        public MockConfigurationSection(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public string? this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Key => _key;

        public string Path => _key;

        public string? Value { get => _value; set => throw new NotImplementedException(); }

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
            throw new NotImplementedException();
        }
    }
}