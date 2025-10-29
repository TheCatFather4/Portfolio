using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IManagementRepository
    {
        void AddItem(Item item);
        void AddServer(Server server);
        Server GetServerById(int serverId);
        List<Server> GetServers();
        void UpdateItem(Item item);
        void UpdateServer(Server server);
    }
}