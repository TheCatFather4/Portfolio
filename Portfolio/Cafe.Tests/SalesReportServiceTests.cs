using Cafe.BLL.Services;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class SalesReportServiceTests
    {
        public ISalesReportService GetSalesReportService()
        {
            var service = new SalesReportService(
                new MockSalesReportLogger(),
                new MockMenuRetrievalRepository(),
                new MockOrderRepository());

            return service;
        }

        [Test]
        public void FilterItemsByCategoryIdAsync_Success()
        {
            var service = GetSalesReportService();

            var result = service.FilterItemsByCategoryIdAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data.Categories, Has.Count.EqualTo(1));
        }

        [Test]
        public void FilterItemsByItemIdAsync_Success()
        {
            var service = GetSalesReportService();

            var result = service.FilterItemsByItemIdAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data.Items, Has.Count.EqualTo(3));
        }

        [Test]
        public void FilterOrdersByDate_Success()
        {
            var service = GetSalesReportService();

            var date = new DateTime();

            date = DateTime.Today;

            var result = service.FilterOrdersByDateAsync(date);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data.Orders, Has.Count.EqualTo(2));
        }
    }
}