using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IServerManagerRepository
    {
        void AddServer(Server server);
        List<Server> GetAllServers();
        Server GetServerById(int serverId);
        void UpdateServer(Server server);
    }
}