using Cafe.BLL.Services;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class ServerManagerServiceTests
    {
        public IServerManagerService GetServerManagerService()
        {
            var service = new ServerManagerService(
                new MockServerManagerLogger(),
                new MockServerManagerRepository());

            return service;
        }

        [Test]
        public void AddServerAsync_Success()
        {
            var service = GetServerManagerService();

            var server = new Server
            {
                ServerID = 1,
                FirstName = "Tim",
                LastName = "Smith",
                HireDate = DateTime.Today,
                DoB = DateTime.Today.AddYears(-20)
            };

            var result = service.AddServerAsync(server);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void GetAllServersAsync_Success()
        {
            var service = GetServerManagerService();

            var result = service.GetAllServersAsync();

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Has.Count.EqualTo(2));
        }

        [Test]
        public void GetServerByIdAsync_NotFound()
        {
            var service = GetServerManagerService();

            var result = service.GetServerByIdAsync(2);

            Assert.That(result.Result.Ok, Is.False);
            Assert.That(result.Result.Data, Is.Null);
        }

        [Test]
        public void GetServerByIdAsync_Success()
        {
            var service = GetServerManagerService();

            var result = service.GetServerByIdAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.ServerID, Is.EqualTo(1));
        }

        [Test]
        public void UpdateServerAsync_Success()
        {
            var service = GetServerManagerService();

            var server = new Server
            {
                FirstName = "John",
                LastName = "Smith",
            };

            var result = service.UpdateServerAsync(server);

            Assert.That(result.Result.Ok, Is.True);
        }
    }
}