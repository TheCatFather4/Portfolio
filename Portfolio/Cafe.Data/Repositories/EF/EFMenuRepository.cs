using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class EFMenuRepository : IMenuRepository
    {
        private CafeContext _dbContext;

        public EFMenuRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public List<Category> GetCategories()
        {
            return _dbContext.Category
                .ToList();
        }

        public Item GetItem(int id)
        {
            return _dbContext.Item
                .Include(i => i.Prices)
                .FirstOrDefault(i => i.ItemID == id);
        }

        public async Task<Item> GetItemAsync(int itemId)
        {
            return await _dbContext.Item
                .Include(i => i.Prices)
                .FirstOrDefaultAsync(i => i.ItemID == itemId);
        }

        public async Task<ItemPrice> GetItemPriceByIdAsync(int itemId)
        {
            return await _dbContext.ItemPrice
                .FirstOrDefaultAsync(ip => ip.ItemID == itemId);
        }

        public List<Item> GetItems()
        {
            return _dbContext.Item
                .ToList();
        }

        public async Task<Item> GetItemWithPriceAsync(int itemId)
        {
            return await _dbContext.Item
               .Include(i => i.Prices)
               .FirstOrDefaultAsync(i => i.ItemID == itemId);
        }

        public List<Item> GetMenu()
        {
            return _dbContext.Item
                .Include(i => i.Prices)
                .ToList();
        }

        public List<TimeOfDay> GetTimeOfDays()
        {
            return _dbContext.TimeOfDay
                .ToList();
        }
    }
}