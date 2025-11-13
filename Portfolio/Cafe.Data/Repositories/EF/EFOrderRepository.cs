using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly CafeContext _dbContext;

        public EFOrderRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items)
        {
            await _dbContext.CafeOrder.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            foreach (var item in items)
            {
                item.OrderID = order.OrderID;
                await _dbContext.OrderItem.AddAsync(item);
            }

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<CafeOrder>> GetAllOrdersAsync()
        {
            return await _dbContext.CafeOrder
                .ToListAsync();
        }

        public async Task<CafeOrder> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.CafeOrder
                .Include(co => co.OrderItems)
                .FirstOrDefaultAsync(co => co.OrderID == orderId);
        }

        public List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId)
        {
            return _dbContext.OrderItem
                .Include(co => co.CafeOrder)
                .Where(co => co.CafeOrder.PaymentStatusID == 1 && co.ItemPriceID == itemPriceId)
                .ToList();
        }

        public async Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _dbContext.CafeOrder.Include(co => co.OrderItems)
                .Where(co => co.CustomerID == customerId)
                .ToListAsync();
        }
    }
}