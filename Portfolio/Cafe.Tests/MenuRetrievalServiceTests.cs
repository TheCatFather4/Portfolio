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
        public IMenuRetrievalService GetMenuRetrievalService()
        {
            var service = new MenuRetrievalService(
                new MockMenuRetrievalLogger(),
                new MockMenuRetrievalRepository());

            return service;
        }

        [Test]
        public void GetAllItemsAPIAsync_Success()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetAllItemsAPIAsync();

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(3));
        }

        [Test]
        public void GetAllItemsMVC_Success()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetAllItemsMVCAsync();

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(3));
        }

        [Test]
        public void GetCategoriesAsync_Success()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetCategoriesAsync();

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetItemByIdAPIAsync_NotFound()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetItemByIdAPIAsync(2);

            Assert.That(result.Result.Ok, Is.False);
            Assert.That(result.Result.Data, Is.Null);
        }

        [Test]
        public void GetItemByIdAPIAsync_Success()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetItemByIdAPIAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Is.Not.Null);
            Assert.That(result.Result.Data?.ItemName, Is.EqualTo("Food"));
        }

        [Test]
        public void GetItemByIdMVCAsync_NotFound()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetItemByIdAPIAsync(2);

            Assert.That(result.Result.Ok, Is.False);
            Assert.That(result.Result.Data, Is.Null);
        }

        [Test]
        public void GetItemByIdMVCAsync_Success()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetItemByIdMVCAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Is.Not.Null);
            Assert.That(result.Result.Data?.ItemName, Is.EqualTo("Food"));
        }

        [Test]
        public void GetTimeOfDays_Success()
        {
            var service = GetMenuRetrievalService();

            var result = service.GetTimeOfDaysAsync();

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(1));
        }
    }
}