using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Requests;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Result<decimal>> GetFinalTotalAsync(int orderId);
        Task<Result<List<PaymentType>>> GetPaymentTypesAsync();
        Task<Result<PaymentResponse>> ProcessPaymentAsync(PaymentRequest dto);
    }
}