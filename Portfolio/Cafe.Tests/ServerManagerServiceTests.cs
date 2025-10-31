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
        public void AddServer_Success()
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

            var result = service.AddServer(server);

            Assert.That(result.Ok, Is.True);
        }

        [Test]
        public void GetAllServers_Success()
        {
            var service = GetServerManagerService();

            var result = service.GetAllServers();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Has.Count.EqualTo(2));
        }

        [Test]
        public void GetServerById_Success()
        {
            var service = GetServerManagerService();

            var result = service.GetServerById(1);

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data.ServerID, Is.EqualTo(1));
        }

        [Test]
        public void UpdateServer_Success()
        {
            var service = GetServerManagerService();

            var server = new Server
            {
                FirstName = "John",
                LastName = "Smith",
            };

            var result = service.UpdateServer(server);

            Assert.That(result.Ok, Is.True);
        }
    }
}