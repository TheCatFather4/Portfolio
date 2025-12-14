using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IServerManagerRepository
    {
        Task AddServerAsync(Server server);
        Task<List<Server>> GetAllServersAsync();
        Task<Server?> GetServerByIdAsync(int serverId);
        Task UpdateServerAsync(Server server);
    }
}