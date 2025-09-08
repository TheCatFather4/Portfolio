using Cafe.BLL.Services;
using Cafe.Core.Entities;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class ManagementTests
    {
        [Test]
        public void DuplicateItemCannotAdd()
        {
            ManagementService service = new ManagementService(new MockManagementRepository());

            Item toAdd = new Item
            {
                CategoryID = 1,
                ItemName = "Tuna Sub",
                ItemDescription = "Delicious",
                ItemStatusID = 1,
            };

            var result = service.AddItem(toAdd);

            Assert.That(result.Ok, Is.False);
        }

        [Test]
        public void MenuItemNotFound()
        {
            ManagementService service = new ManagementService(new MockManagementRepository());

            var result = service.GetMenuItemById(3);

            Assert.That(result.Ok, Is.False);
        }

        [Test]
        public void MenuItemFound()
        {
            ManagementService service = new ManagementService(new MockManagementRepository());

            var result = service.GetMenuItemById(1);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void ServerNotFound()
        {
            ManagementService service = new ManagementService(new MockManagementRepository());

            var result = service.GetServerById(4);

            Assert.That(result.Ok, Is.False);
        }

        [Test]
        public void ServerFound()
        {
            ManagementService service = new ManagementService(new MockManagementRepository());

            var result = service.GetServerById(1);

            Assert.That(result.Ok, Is.True);
        }
    }
}
