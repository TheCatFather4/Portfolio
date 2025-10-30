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
        public void AddNewItem_Success()
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

            var result = service.AddNewItem(item);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void FilterMenu_ByCategory()
        {
            var service = GetMenuManagerService();

            var filter = new MenuFilter
            {
                CategoryID = 1,
            };

            var result = service.FilterMenu(filter);

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
            Assert.That(result.Data[0].CategoryID, Is.EqualTo(1));
        }

        [Test]
        public void FilterMenu_ByDate()
        {
            var service = GetMenuManagerService();

            var date = new DateTime();

            date = DateTime.Today;

            var filter = new MenuFilter
            {
                Date = date
            };

            var result = service.FilterMenu(filter);

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
            Assert.That(result.Data[0].Prices[0].StartDate, Is.EqualTo(filter.Date));
        }

        [Test]
        public void FilterMenu_ByTimeOfDay()
        {
            var service = GetMenuManagerService();

            var filter = new MenuFilter
            {
                TimeOfDayID = 2,
            };

            var result = service.FilterMenu(filter);

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(1));
            Assert.That(result.Data[0].Prices[0].TimeOfDayID, Is.EqualTo(2));
        }

        [Test]
        public void FilterMenu_NoFilter()
        {
            var service = GetMenuManagerService();

            var filter = new MenuFilter();

            var result = service.FilterMenu(filter);

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(3));
        }

        [Test]
        public void UpdateItem_Success()
        {
            var service = GetMenuManagerService();

            var item = new Item
            {
                ItemStatusID = 3,
                ItemDescription = "Tasty"
            };

            var result = service.UpdateItem(item);

            Assert.That(result.Ok, Is.True);
        }
    }
}