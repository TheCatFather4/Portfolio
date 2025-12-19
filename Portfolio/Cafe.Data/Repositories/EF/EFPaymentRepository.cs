using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
    /// <summary>
    /// Handles data persistence concerning Payment entities.
    /// Implements IPaymentRepository by utilizing Entity Framework Core.
    /// </summary>
    public class EFPaymentRepository : IPaymentRepository
    {
        private readonly CafeContext _dbContext;

        public EFPaymentRepository(string connectionString)
        {
            _dbContext = new CafeContext(connectionString);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _dbContext.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetFinalTotalAsync(int orderId)
        {
            CafeOrder? order = await _dbContext.CafeOrder
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order != null)
            {
                return order.FinalTotal;
            }

            return 0;
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            return await _dbContext.PaymentType
                .ToListAsync();
        }
    }
}