using Cafe.Core.DTOs;

namespace Cafe.Core.Interfaces.Services
{
    public interface ISalesReportService
    {
        Task<Result<ItemReportFilter>> FilterItemsByCategoryId(int categoryId);
        Task<Result<ItemReportFilter>> FilterItemsByItemIdAsync(int itemId);
        Result<OrderReportFilter> FilterOrdersByDate(DateTime date);
    }
}