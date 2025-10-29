using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Data.Repositories.EF
{
    public class EFServerManagerRepository : IServerManagerRepository
    {
        private readonly CafeContext _dbContext;

        public EFServerManagerRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public void AddServer(Server server)
        {
            _dbContext.Server.Add(server);
            _dbContext.SaveChanges();
        }

        public Server GetServerById(int serverId)
        {
            return _dbContext.Server
                .FirstOrDefault(s => s.ServerID == serverId);
        }

        public List<Server> GetServers()
        {
            return _dbContext.Server
                .ToList();
        }

        public void UpdateServer(Server server)
        {
            _dbContext.Update(server);
            _dbContext.SaveChanges();
        }
    }
}