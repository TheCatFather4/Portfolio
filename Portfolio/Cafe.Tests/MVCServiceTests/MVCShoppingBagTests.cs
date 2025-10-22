using Cafe.BLL.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.MVCServiceTests
{
    [TestFixture]
    public class MVCShoppingBagTests
    {
        [Test]
        public void AddItem_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRepository());

            var result = service.MVCAddItemToBagAsync(1, 1, "food", (decimal)2.50, 1, "food.jpg");

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void ClearShoppingBag_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRepository());

            var result = service.ClearShoppingBagAsync(1);

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void GetShoppinmgBag_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRepository());

            var result = service.GetShoppingBagAsync(1);

            Assert.That(result.IsCompleted);
        }
    }
}