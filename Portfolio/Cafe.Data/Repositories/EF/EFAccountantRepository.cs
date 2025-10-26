using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class EFAccountantRepository : IAccountantRepository
    {
        private readonly CafeContext _dbContext;

        public EFAccountantRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId)
        {
            return _dbContext.OrderItem
               .Include(co => co.CafeOrder)
               .Where(co => co.CafeOrder.PaymentStatusID == 1 && co.ItemPriceID == itemPriceId)
               .ToList();
        }

        public List<CafeOrder> GetOrders()
        {
            return _dbContext.CafeOrder
                .ToList();
        }
    }
}