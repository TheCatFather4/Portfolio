using Cafe.BLL.Services;
using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class MenuManagerServiceTests
    {
        public IMenuManagerService GetMenuManagerService()
        {
            var service = new MenuManagerService(
                new MockMenuManagerLogger(),
                new MockMenuManagerRepository(),
                new MockMenuRetrievalRepository());

            return service;
        }

        [Test]
        public void AddNewItemAsync_Success()
        {
            var service = GetMenuManagerService();

            var item = new Item
            {
                ItemID = 1,
                CategoryID = 1,
                ItemStatusID = 1,
                ItemName = "Food",
                ItemDescription = "Delicious",
                ItemImgPath = "food.jpg",
                Prices = new List<ItemPrice>()
            };

            var result = service.AddNewItemAsync(item);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void FilterMenuAsync_ByCategory()
        {
            var service = GetMenuManagerService();

            var filter = new MenuFilter
            {
                CategoryID = 1,
            };

            var result = service.FilterMenuAsync(filter);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(1));
            Assert.That(result.Result.Data[0].CategoryID, Is.EqualTo(1));
        }

        [Test]
        public void FilterMenuAsync_ByDate()
        {
            var service = GetMenuManagerService();

            var date = new DateTime();

            date = DateTime.Today;

            var filter = new MenuFilter
            {
                Date = date
            };

            var result = service.FilterMenuAsync(filter);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(1));
            Assert.That(result.Result.Data[0].Prices[0].StartDate, Is.EqualTo(filter.Date));
        }

        [Test]
        public void FilterMenuAsync_ByTimeOfDay()
        {
            var service = GetMenuManagerService();

            var filter = new MenuFilter
            {
                TimeOfDayID = 2,
            };

            var result = service.FilterMenuAsync(filter);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(1));
            Assert.That(result.Result.Data[0].Prices[0].TimeOfDayID, Is.EqualTo(2));
        }

        [Test]
        public void FilterMenuAsync_NoFilter()
        {
            var service = GetMenuManagerService();

            var filter = new MenuFilter();

            var result = service.FilterMenuAsync(filter);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(3));
        }

        [Test]
        public void UpdateItemAsync_Success()
        {
            var service = GetMenuManagerService();

            var item = new Item
            {
                ItemStatusID = 3,
                ItemDescription = "Tasty"
            };

            var result = service.UpdateItemAsync(item);

            Assert.That(result.Result.Ok, Is.True);
        }
    }
}