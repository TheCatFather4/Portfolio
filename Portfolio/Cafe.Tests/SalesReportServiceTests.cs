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
        public void FilterItemsByCategoryIdAsync_NotFound()
        {
            var service = GetSalesReportService();

            var result = service.FilterItemsByCategoryIdAsync(3);

            Assert.That(result.Result.Ok, Is.False);
            Assert.That(result.Result.Data, Is.Null);
        }

        [Test]
        public void FilterItemsByCategoryIdAsync_Success()
        {
            var service = GetSalesReportService();

            var result = service.FilterItemsByCategoryIdAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.CategoryItems, Has.Count.EqualTo(2));
        }

        [Test]
        public void FilterItemsByItemIdAsync_NotFound()
        {
            var service = GetSalesReportService();

            var result = service.FilterItemsByItemIdAsync(3);

            Assert.That(result.Result.Ok, Is.False);
            Assert.That(result.Result.Data, Is.Null);
        }

        [Test]
        public void FilterItemsByItemIdAsync_Success()
        {
            var service = GetSalesReportService();

            var result = service.FilterItemsByItemIdAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.Reports, Has.Count.EqualTo(3));
        }

        [Test]
        public void FilterOrdersByDateAsync_NotFound()
        {
            var service = GetSalesReportService();

            var date = new DateTime(2025, 1, 25);

            var result = service.FilterOrdersByDateAsync(date);

            Assert.That(result.Result.Data?.Orders, Has.Count.EqualTo(0));
        }

        [Test]
        public void FilterOrdersByDateAsync_Success()
        {
            var service = GetSalesReportService();

            var date = new DateTime();

            date = DateTime.Today;

            var result = service.FilterOrdersByDateAsync(date);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.Orders, Has.Count.EqualTo(1));
        }
    }
}