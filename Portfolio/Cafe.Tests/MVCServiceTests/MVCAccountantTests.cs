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