using Cafe.BLL.Services;
using Cafe.Tests.MockRepositories;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class WebTokenServiceTests
    {
        [Test]
        public void GenerationTokenAsync_Success()
        {
            var service = new WebTokenService(new MockConfiguration());

            var user = new IdentityUser
            {
                Id = new Guid().ToString(),
                Email = "webtokentester@jwt.com"
            };

            var result = service.GenerateTokenAsync(user);

            Assert.That(result.Result, Is.Not.Null);
        }
    }
}