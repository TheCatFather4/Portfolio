using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IManagementService
    {
        Result<List<Server>> GetServers();
        Result<Server> GetServerById(int serverID);
        Result UpdateServer(Server server);
        Result AddServer(Server server);
        Result<Item> GetMenuItemById(int itemID);
        Result UpdateMenu(Item item);
        Result AddItem (Item item);
    }
}
