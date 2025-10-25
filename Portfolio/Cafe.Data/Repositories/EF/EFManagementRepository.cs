using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Data.Repositories.EF
{
    public class EFManagementRepository : IManagementRepository
    {
        private readonly CafeContext _dbContext;

        public EFManagementRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public void AddItem(Item item)
        {
            _dbContext.Item.Add(item);
            _dbContext.SaveChanges();
        }

        public void AddServer(Server server)
        {
            _dbContext.Server.Add(server);
            _dbContext.SaveChanges();
        }

        public bool IsDuplicateItem(string itemName)
        {
            var item = _dbContext.Item
                .FirstOrDefault(i => i.ItemName == itemName);

            if (item != null)
            {
                return true;
            }

            return false;
        }

        public Server GetServerById(int serverID)
        {
            return _dbContext.Server
                .FirstOrDefault(s => s.ServerID == serverID);
        }

        public List<Server> GetServers()
        {
            return _dbContext.Server
                .ToList();
        }

        public void UpdateMenu(Item item)
        {
            _dbContext.Update(item);
            _dbContext.SaveChanges();
        }

        public void UpdateServer(Server server)
        {
            _dbContext.Update(server);
            _dbContext.SaveChanges();
        }
    }
}