using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockServerManagerRepository : IServerManagerRepository
    {
        public async Task AddServerAsync(Server server)
        {
            List<Server> servers = new List<Server>();
            servers.Add(server);
            await Task.Delay(1000);
        }

        public async Task<List<Server>> GetAllServersAsync()
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

            await Task.Delay(1000);
            return servers;
        }

        public async Task<Server?> GetServerByIdAsync(int serverId)
        {
            var server = new Server
            {
                ServerID = 1,
                FirstName = "John",
                LastName = "Smith",
                HireDate = DateTime.Now,
                DoB = DateTime.Today.AddYears(-25)
            };

            if (serverId == 1)
            {
                await Task.Delay(1000);
                return server;
            }

            return null;
        }

        public async Task UpdateServerAsync(Server server)
        {
            var server2 = new Server();
            server2 = server;
            await Task.Delay(1000);
        }
    }
}