using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<CafeOrderResponse>> CreateOrderAsync(int customerId, int paymentTypeId, decimal tip);
        Task<Result<CafeOrderResponse>> GetOrderDetailsAsync(int orderId);
        Task<Result<List<CafeOrderResponse>>> GetOrdersAsync(int customerId);
    }
}
