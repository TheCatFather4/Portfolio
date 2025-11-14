using Cafe.Core.DTOs;

namespace Cafe.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<CafeOrderResponse>> CreateNewOrderAsync(OrderRequest dto);
        Task<Result<CafeOrderResponse>> GetOrderDetailsAsync(int orderId);
        Task<Result<List<CafeOrderResponse>>> GetOrderHistoryAsync(int customerId);
    }
}