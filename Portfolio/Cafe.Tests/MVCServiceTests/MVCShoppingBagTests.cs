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
                new MockMenuRetrievalRepository());

            var result = service.MVCAddItemToBagAsync(1, 1, "food", (decimal)2.50, 1, "food.jpg");

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void ClearShoppingBag_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRetrievalRepository());

            var result = service.ClearShoppingBagAsync(1);

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void GetItemWithPrice_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRetrievalRepository());

            var result = service.GetItemWithPriceAsync(1);

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void GetShoppinmgBag_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRetrievalRepository());

            var result = service.GetShoppingBagAsync(1);

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void GetShoppingBagItem_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRetrievalRepository());

            var result = service.GetShoppingBagItemByIdAsync(1);

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void RemoveItem_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRetrievalRepository());

            var result = service.RemoveItemFromBagAsync(1, 1);

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void UpdateQuantity_Async_Success()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository(),
                new MockMenuRetrievalRepository());

            var result = service.UpdateItemQuantityAsync(1, 1, 3);

            Assert.That(result.IsCompleted);
        }
    }
}