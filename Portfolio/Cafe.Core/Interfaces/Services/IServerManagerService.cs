using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IServerManagerService
    {
        Task<Result> AddServerAsync(Server server);
        Task<Result<List<Server>>> GetAllServersAsync();
        Task<Result<Server>> GetServerByIdAsync(int serverId);
        Task<Result> UpdateServerAsync(Server server);
    }
}