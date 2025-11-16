using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Repositories.EF
{
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

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            return await _dbContext.PaymentType
                .ToListAsync();
        }
    }
}