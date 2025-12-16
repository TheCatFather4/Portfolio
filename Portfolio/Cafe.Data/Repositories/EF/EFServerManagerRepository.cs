using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    /// <summary>
    /// Handles data persistence concerning Server entities.
    /// Implements IServerManagerRepository by utilizing Entity Framework Core.
    /// </summary>
    public class EFServerManagerRepository : IServerManagerRepository
    {
        private readonly CafeContext _dbContext;

        public EFServerManagerRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task AddServerAsync(Server server)
        {
            await _dbContext.Server.AddAsync(server);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Server>> GetAllServersAsync()
        {
            return await _dbContext.Server
                .ToListAsync();
        }

        public async Task<Server?> GetServerByIdAsync(int serverId)
        {
            return await _dbContext.Server
                .FirstOrDefaultAsync(s => s.ServerID == serverId);
        }

        public async Task UpdateServerAsync(Server server)
        {
            _dbContext.Update(server);
            await _dbContext.SaveChangesAsync();
        }
    }
}