using Cafe.BLL.Services;
using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class ShoppingBagServiceTests
    {
        public IShoppingBagService GetShoppingBagService()
        {
            var service = new ShoppingBagService(
                new MockShoppingBagLogger(),
                new MockShoppingBagRepository());

            return service;
        }

        [Test]
        public void AddItemToShoppingBagAsync_Success()
        {
            var service = GetShoppingBagService();

            var dto = new AddItemRequest
            {
                ShoppingBagId = 1,
                ItemId = 1,
                ItemStatusId = 1,
                Quantity = 1,
                ItemName = "Food",
                Price = 1.00M,
                ItemImgPath = "food.jpg"
            };

            var result = service.AddItemToShoppingBagAsync(dto);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void ClearShoppingBagAsync_Fail()
        {
            var service = GetShoppingBagService();

            var result = service.ClearShoppingBagAsync(3);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void ClearShoppingBagAsync_Success()
        {
            var service = GetShoppingBagService();

            var result = service.ClearShoppingBagAsync(1);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void GetShoppingBagByCustomerIdAsync_NotFound()
        {
            var service = GetShoppingBagService();

            var result = service.GetShoppingBagByCustomerIdAsync(3);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void GetShoppingBagByCustomerIdAsync_Success()
        {
            var service = GetShoppingBagService();

            var result = service.GetShoppingBagByCustomerIdAsync(1);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void GetShoppingBagItemByIdAsync_NotFound()
        {
            var service = GetShoppingBagService();

            var result = service.GetShoppingBagItemByIdAsync(3);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void GetShoppingBagItemByIdAsync_Success()
        {
            var service = GetShoppingBagService();

            var result = service.GetShoppingBagItemByIdAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.ItemName, Is.EqualTo("Coffee"));
        }

        [Test]
        public void RemoveItemFromShoppingBagAsync_ItemToRemoveNotFound()
        {
            var service = GetShoppingBagService();

            var result = service.RemoveItemFromShoppingBagAsync(1, 3);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void RemoveItemFromShoppingBagAsync_ShoppingBagNotFound()
        {
            var service = GetShoppingBagService();

            var result = service.RemoveItemFromShoppingBagAsync(3, 1);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void RemoveItemFromShoppingBagAsync_Success()
        {
            var service = GetShoppingBagService();

            var result = service.RemoveItemFromShoppingBagAsync(1, 1);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void UpdateItemQuantityAsync_Success()
        {
            var service = GetShoppingBagService();

            var result = service.UpdateItemQuantityAsync(1, 3);

            Assert.That(result.Result.Ok, Is.True);
        }
    }
}