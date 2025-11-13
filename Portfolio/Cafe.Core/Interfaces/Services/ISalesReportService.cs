using Cafe.Core.DTOs;

namespace Cafe.Core.Interfaces.Services
{
    public interface ISalesReportService
    {
        Task<Result<ItemCategoryFilter>> FilterItemsByCategoryIdAsync(int categoryId);
        Task<Result<ItemCategoryFilter>> FilterItemsByItemIdAsync(int itemId);
        Task<Result<OrderFilter>> FilterOrdersByDateAsync(DateTime date);
    }
}