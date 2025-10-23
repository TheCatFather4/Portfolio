using Cafe.BLL.Services;
using Cafe.Core.DTOs;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.APIServiceTests
{
    [TestFixture]
    public class APIShoppingBagTests
    {
        [Test]
        public void AddItemAPI_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRepository());

            var dto = new AddItemRequest();

            var result = service.APIAddItemToBagAsync(1, dto);

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void GetShoppingBagAPI_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRepository());

            var result = service.APIGetShoppingBagAsync(1);

            Assert.That(result.IsCompleted);
        }
    }
}