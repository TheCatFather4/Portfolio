using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Filters;

namespace Cafe.Core.Interfaces.Services
{
    public interface ISalesReportService
    {
        Task<Result<ItemCategoryFilter>> FilterItemsByCategoryIdAsync(int categoryId);
        Task<Result<ItemCategoryFilter>> FilterItemsByItemIdAsync(int itemId);
        Task<Result<OrderDateFilter>> FilterOrdersByDateAsync(DateTime date);
    }
}