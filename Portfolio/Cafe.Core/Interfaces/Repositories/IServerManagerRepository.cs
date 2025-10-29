using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IServerManagerRepository
    {
        void AddServer(Server server);
        Server GetServerById(int serverId);
        List<Server> GetServers();
        void UpdateServer(Server server);
    }
}