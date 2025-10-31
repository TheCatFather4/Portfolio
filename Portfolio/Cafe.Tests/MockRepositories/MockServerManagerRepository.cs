using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockServerManagerRepository : IServerManagerRepository
    {
        public void AddServer(Server server)
        {
            List<Server> servers = new List<Server>();
            servers.Add(server);
        }

        public List<Server> GetAllServers()
        {
            var servers = new List<Server>();

            var server1 = new Server
            {
                ServerID = 1,
                FirstName = "John",
                LastName = "Smith",
                HireDate = DateTime.Now,
                DoB = DateTime.Today.AddYears(-25)
            };

            var server2 = new Server
            {
                ServerID = 2,
                FirstName = "Jessie",
                LastName = "Rocket",
                HireDate = DateTime.Now,
                DoB = DateTime.Today.AddYears(-23)
            };

            servers.Add(server1);
            servers.Add(server2);

            return servers;
        }

        public Server GetServerById(int serverId)
        {
            var server = new Server
            {
                ServerID = 1,
                FirstName = "John",
                LastName = "Smith",
                HireDate = DateTime.Now,
                DoB = DateTime.Today.AddYears(-25)
            };

            return server;
        }

        public void UpdateServer(Server server)
        {
            var server2 = new Server();
            server2 = server;
        }
    }
}