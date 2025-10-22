using Cafe.BLL.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.APIServiceTests
{
    [TestFixture]
    public class APIMenuTests
    {
        [Test]
        public void GetMenuAPI_Success()
        {
            var service = new MenuService(
                new MockMenuLogger(),
                new MockMenuRepository());

            var result = service.GetMenuAPI();

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void GetItemAPI_Async_Success()
        {
            var service = new MenuService(
                new MockMenuLogger(),
                new MockMenuRepository());

            var result = service.GetItemAPIAsync(1);

            Assert.That(result.IsCompleted);
        }
    }
}