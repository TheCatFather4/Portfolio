using Cafe.BLL.Services;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class MenuRetrievalServiceTests
    {
        public IMenuRetrievalService GetMenuService()
        {
            var service = new MenuRetrievalService(
                new MockMenuRetrievalLogger(),
                new MockMenuRetrievalRepository());

            return service;
        }

        [Test]
        public void GetAllItemsAPI_Success()
        {
            var service = GetMenuService();

            var result = service.GetAllItemsAPI();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetAllItemsMVC_Success()
        {
            var service = GetMenuService();

            var result = service.GetAllItemsMVC();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetCategories_Success()
        {
            var service = GetMenuService();

            var result = service.GetCategories();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetItemByIdAsyncAPI_Success()
        {
            var service = GetMenuService();

            var task = service.GetItemByIdAsyncAPI(1);

            Assert.That(task.Result.Ok, Is.True);
            Assert.That(task.Result.Data, Is.Not.Null);
        }

        [Test]
        public void GetItemByIdAsyncMVC_Success()
        {
            var service = GetMenuService();

            var task = service.GetItemByIdAsyncMVC(1);

            Assert.That(task.Result.Ok, Is.True);
            Assert.That(task.Result.Data, Is.Not.Null);
        }

        [Test]
        public void GetItemPriceByItemIdAsync_Success()
        {
            var service = GetMenuService();

            var task = service.GetItemPriceByItemIdAsync(1);

            Assert.That(task.Result.Ok, Is.True);
            Assert.That(task.Result.Data, Is.Not.Null);
        }

        [Test]
        public void GetItemsByCategoryId_Success()
        {
            var service = GetMenuService();

            var result = service.GetItemsByCategoryId(1);

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetTimeOfDays_Success()
        {
            var service = GetMenuService();

            var result = service.GetTimeOfDays();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
        }
    }
}