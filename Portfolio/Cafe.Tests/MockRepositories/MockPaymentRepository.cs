using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockPaymentRepository : IPaymentRepository
    {
        public Task AddPaymentAsync(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            throw new NotImplementedException();
        }
    }
}