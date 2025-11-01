using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface ISalesReportService
    {
        Task<Result<ItemReportFilter>> FilterItemsByItemIdAsync(int itemId);
        Result<OrderDateFilter> FilterOrdersByDate(DateTime date);
        Result<List<OrderItem>> GetOrderItemsByItemPriceId(int itemPriceId);
    }
}