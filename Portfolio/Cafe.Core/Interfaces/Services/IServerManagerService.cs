using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IServerManagerService
    {
        Result AddServer(Server server);
        Result<List<Server>> GetAllServers();
        Result<Server> GetServerById(int serverID);
        Result UpdateServer(Server server);
    }
}