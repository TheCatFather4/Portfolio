using Cafe.Core.DTOs;

namespace Cafe.Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Result<PaymentResponse> ProcessPayment(PaymentRequest dto);
    }
}
