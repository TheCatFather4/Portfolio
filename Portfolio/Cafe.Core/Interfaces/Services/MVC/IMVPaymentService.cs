using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services.MVC
{
    public interface IMVPaymentService
    {
        Result<Payment> ProcessPayment(Payment payment);
        Result<List<PaymentType>> GetPaymentTypes();
    }
}
