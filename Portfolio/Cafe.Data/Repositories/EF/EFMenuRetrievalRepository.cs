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

        public List<Item> GetAllItems()
        {
            return _dbContext.Item
                .Include(i => i.Prices)
                .ToList();
        }

        public List<Category> GetCategories()
        {
            return _dbContext.Category
                .ToList();
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return await _dbContext.Item
                .Include(i => i.Prices)
                .FirstOrDefaultAsync(i => i.ItemID == itemId);
        }

        public async Task<ItemPrice> GetItemPriceByItemIdAsync(int itemId)
        {
            return await _dbContext.ItemPrice
                .FirstOrDefaultAsync(ip => ip.ItemID == itemId);
        }

        public List<Item> GetItemsByCategoryId(int categoryId)
        {
            return _dbContext.Item
               .Where(i => i.CategoryID == categoryId)
               .ToList();
        }

        public List<TimeOfDay> GetTimeOfDays()
        {
            return _dbContext.TimeOfDay
                .ToList();
        }
    }
}