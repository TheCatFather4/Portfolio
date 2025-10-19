using Cafe.BLL.Services.MVC;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.MVCServiceTests
{
    [TestFixture]
    public class MVCAccountantTests
    {
        [Test]
        public void GetItemPrice_Success()
        {
            var service = new MVCAccountantService(
                new MockAccountantLogger(),
                new MockAccountantRepository());

            var result = service.GetItemPriceByItemId(1);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void GetItemPrice_Fail()
        {
            var service = new MVCAccountantService(
                new MockAccountantLogger(),
                new MockAccountantRepository());

            var result = service.GetItemPriceByItemId(0);

            Assert.That(result.Data.ItemID, Is.Zero);
        }

        [Test]
        public void GetItemsByCategoryId_Success()
        {
            var service = new MVCAccountantService(
                new MockAccountantLogger(),
                new MockAccountantRepository());

            var result = service.GetItemsByCategoryID(1);

            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void GetOrderItemsByItemPriceId_Success()
        {
            var service = new MVCAccountantService(
                new MockAccountantLogger(),
                new MockAccountantRepository());

            var result = service.GetOrderItemsByItemPriceId(1);

            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void GetOrders_Success()
        {
            var service = new MVCAccountantService(
                new MockAccountantLogger(),
                new MockAccountantRepository());

            var result = service.GetOrders();

            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }
    }
}