using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class EFAccountantRepository : IAccountantRepository
    {
        private CafeContext _dbContext;

        public EFAccountantRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public List<ItemPrice> GetItemPrices()
        {
            return _dbContext.ItemPrice
                .Include(ip => ip.OrderItems)
                .ToList();
        }

        public List<Item> GetItemsByCategoryID(int categoryID)
        {
            return _dbContext.Item
                .Where(i => i.CategoryID == categoryID)
                .ToList();
        }

        public List<CafeOrder> GetOrders()
        {
            return _dbContext.CafeOrder
                .ToList();
        }
    }
}
