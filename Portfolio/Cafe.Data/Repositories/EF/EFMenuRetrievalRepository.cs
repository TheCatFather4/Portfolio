using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class EFMenuRetrievalRepository : IMenuRetrievalRepository
    {
        private readonly CafeContext _dbContext;

        public EFMenuRetrievalRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _dbContext.Item
                .Include(i => i.Prices)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Category
                .ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int itemId)
        {
            return await _dbContext.Item
                .Include(i => i.Prices)
                .FirstOrDefaultAsync(i => i.ItemID == itemId);
        }

        public async Task<ItemPrice?> GetItemPriceByItemIdAsync(int itemId)
        {
            return await _dbContext.ItemPrice
                .FirstOrDefaultAsync(ip => ip.ItemID == itemId);
        }

        public async Task<List<Item>> GetItemsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Item
                .Where(i => i.CategoryID == categoryId)
                .ToListAsync();
        }

        public async Task<List<TimeOfDay>> GetTimeOfDaysAsync()
        {
            return await _dbContext.TimeOfDay
                .ToListAsync();
        }
    }
}