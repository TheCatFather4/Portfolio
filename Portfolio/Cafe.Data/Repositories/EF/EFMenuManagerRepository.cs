using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Data.Repositories.EF
{
    /// <summary>
    /// Handles data persistence concerning Item entities.
    /// Implements IMenuManagerRepository by utilizing Entity Framework Core.
    /// </summary>
    public class EFMenuManagerRepository : IMenuManagerRepository
    {
        private readonly CafeContext _dbContext;

        public EFMenuManagerRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task AddItemAsync(Item item)
        {
            _dbContext.Item.Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            _dbContext.Update(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}