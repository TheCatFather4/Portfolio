using Cafe.BLL.Services.MVC;
using Cafe.Core.Entities;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class ManagementTests
    {
        [Test]
        public void AddItem_Fail()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            Item toAdd = new Item
            {
                CategoryID = 1,
                ItemName = "Tuna Sub",
                ItemDescription = "Delicious"
            };

            var result = service.AddItem(toAdd);

            Assert.That(result.Ok, Is.False);
        }

        [Test]
        public void AddItem_Success()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            Item toAdd = new Item
            {
                CategoryID = 1,
                ItemName = "Food",
                ItemDescription = "Yummy",
            };

            var result = service.AddItem(toAdd);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void AddServer_Success()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            Server server = new Server
            {
                ServerID = 1,
                FirstName = "Dan",
                LastName = "Danny",
                DoB = new DateTime(2001, 01, 01)
            };

            var result = service.AddServer(server);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void GetAllServers_Success()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            var result = service.GetServers();

            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void MenuItem_Found()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            var result = service.GetMenuItemById(1);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void MenuItem_NotFound()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            var result = service.GetMenuItemById(3);

            Assert.That(result.Data.ItemID, Is.Null);
        }

        [Test]
        public void Server_Found()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            var result = service.GetServerById(1);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void Server_NotFound()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            var result = service.GetServerById(3);

            Assert.That(result.Data.ServerID, Is.Zero);
        }

        [Test]
        public void UpdateServer_Success()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            var server = new Server();

            var result = service.UpdateServer(server);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void UpdateMenu_Success()
        {
            MVCManagementService service = new MVCManagementService(
                new MockManagementLogger(),
                new MockManagementRepository());

            var item = new Item();

            var result = service.UpdateMenu(item);

            Assert.That(result.Ok, Is.True);
        }
    }
}