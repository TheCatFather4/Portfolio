using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services.MVC
{
    public interface IMVOrderService
    {
        Task<Result<CafeOrder>> CreateNewOrderAsync(int customerId, int paymentTypeId, decimal tip);
        Task<Result<CafeOrder>> GetOrderByIdAsync(int orderId);
        Task<Result<List<CafeOrder>>> GetOrderHistoryAsync(int customerId);
    }
}
