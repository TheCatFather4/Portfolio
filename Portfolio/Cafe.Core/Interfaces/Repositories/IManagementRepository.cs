using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IManagementRepository
    {
        List<Server> GetServers();
        Server GetServerById(int serverID);
        void UpdateServer(Server server);
        void AddServer(Server server);
        Item GetMenuItemById(int itemID);
        void UpdateMenu(Item item);
        void AddItem(Item item);
    }
}
