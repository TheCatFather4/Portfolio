using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task<List<PaymentType>> GetPaymentTypesAsync();
    }
}