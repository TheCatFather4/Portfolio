using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task<decimal> GetFinalTotalAsync(int orderId);
        Task<List<PaymentType>> GetPaymentTypesAsync();
    }
}