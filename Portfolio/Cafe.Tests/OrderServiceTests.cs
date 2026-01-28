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

            var dto = new CafeOrderRequest
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

            Assert.That(result.Result?.ItemPriceID, Is.EqualTo(1));
        }

        [Test]
        public void GetOrderDetailsAsync_NotFound()
        {
            var service = GetOrderService();

            var result = service.GetOrderDetailsAsync(2);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void GetOrderDetailsAsync_Success()
        {
            var service = GetOrderService();

            var result = service.GetOrderDetailsAsync(1);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void GetOrderHistoryAsync_NotFound()
        {
            var service = GetOrderService();

            var result = service.GetOrderHistoryAsync(3);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void GetOrderHistoryAsync_Success()
        {
            var service = GetOrderService();

            var result = service.GetOrderHistoryAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetOrderTotalAsync_Success()
        {
            var service = GetOrderService();

            var result = service.GetOrderTotalAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Is.EqualTo(26.00M));
        }

        [Test]
        public void GetOrderTotalAsync_Zero()
        {
            var service = GetOrderService();
            var result = service.GetOrderTotalAsync(2);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void GetShoppingBagByCustomerId_Success()
        {
            var service = GetOrderService();

            var result = service.GetShoppingBagByCustomerIdAsync(1);

            Assert.That(result.Result?.ShoppingBagID, Is.EqualTo(1));
        }
    }
}