using Cafe.BLL.Services;
using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        public IOrderService GetOrderService()
        {
            var service = new OrderService(
                new MockOrderLogger(),
                new MockMenuRetrievalRepository(),
                new MockOrderRepository(),
                new MockShoppingBagRepository());

            return service;
        }

        [Test]
        public void CreateNewOrderAsync_Success()
        {
            var service = GetOrderService();

            var dto = new OrderRequest
            {
                CustomerId = 1,
                PaymentTypeId = 1,
                Tip = 1.00M
            };

            var result = service.CreateNewOrderAsync(dto);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void GetItemPriceByItemId_Success()
        {
            var service = GetOrderService();

            var result = service.GetItemPriceByItemIdAsync(1);

            Assert.That(result.Result.ItemPriceID, Is.EqualTo(1));
        }

        [Test]
        public void GetShoppingBagByCustomerId_Success()
        {
            var service = GetOrderService();

            var result = service.GetShoppingBagByCustomerIdAsync(1);

            Assert.That(result.Result.ShoppingBagID, Is.EqualTo(1));
        }
    }
}