using Cafe.BLL.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.MVCServiceTests
{
    [TestFixture]
    public class MVCMenuTests
    {
        [Test]
        public void GetCategories_Success()
        {
            var service = new MenuService(
                new MockMenuLogger(),
                new MockMenuRepository());

            var result = service.GetCategories();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void GetMenu_Success()
        {
            var service = new MenuService(
                new MockMenuLogger(),
                new MockMenuRepository());

            var result = service.GetMenu();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void GetTimeOfDays_Success()
        {
            var service = new MenuService(
                new MockMenuLogger(),
                new MockMenuRepository());

            var result = service.GetTimeOfDays();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void GetItemPriceById_Success()
        {
            var service = new MenuService(
                new MockMenuLogger(),
                new MockMenuRepository());

            var result = service.GetItemPriceByIdAsync(1);

            Assert.That(result.IsCompleted);
        }
    }
}