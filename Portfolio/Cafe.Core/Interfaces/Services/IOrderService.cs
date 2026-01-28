using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Requests;
using Cafe.Core.DTOs.Responses;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<CafeOrderResponse>> CreateNewOrderAsync(CafeOrderRequest dto);
        Task<ItemPrice?> GetItemPriceByItemIdAsync(int itemId);
        Task<Result<CafeOrderResponse>> GetOrderDetailsAsync(int orderId);
        Task<Result<List<CafeOrderResponse>>> GetOrderHistoryAsync(int customerId);
        Task<Result<decimal>> GetOrderTotalAsync(int customerId);
        Task<ShoppingBag?> GetShoppingBagByCustomerIdAsync(int customerId);
    }
}