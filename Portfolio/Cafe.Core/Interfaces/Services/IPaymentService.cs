using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Result<List<PaymentType>>> GetPaymentTypesAsync();
        Task<Result<PaymentResponse>> ProcessPaymentAsync(PaymentRequest dto);
    }
}