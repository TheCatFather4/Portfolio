using Cafe.Core.DTOs;
using Cafe.Core.DTOs.Filters;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning sales reports.
    /// </summary>
    public class SalesReportService : ISalesReportService
    {
        private readonly ILogger _logger;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Constructs a service with the dependencies required to prepare sales reports.
        /// </summary>
        /// <param name="logger">A dependency used for logging errors.</param>
        /// <param name="menuRetrievalRepository">A dependency used for retrieving data concerning Item records.</param>
        /// <param name="orderRepository">A dependency used for retrieving data concerning CafeOrder records.</param>
        public SalesReportService(ILogger<SalesReportService> logger, IMenuRetrievalRepository menuRetrievalRepository, IOrderRepository orderRepository)
        {
            _logger = logger;
            _menuRetrievalRepository = menuRetrievalRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Attempts to retrieve Item records by CategoryID. 
        /// If successful, pricing and sales data is retrieved for each item.
        /// A sales report is generated for each date the item was sold.
        /// An additional report is generated for each item in the Category selected.
        /// </summary>
        /// <param name="categoryId">A CategoryID used to retrieve Item records.</param>
        /// <returns>A Result DTO with an ItemCategoryFilter DTO as its data.</returns>
        public async Task<Result<ItemCategoryFilter>> FilterItemsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var dto = new ItemCategoryFilter
                {
                    SelectedCategoryID = categoryId,
                    CategoryItems = new List<CategoryItemFilter>()
                };

                var items = await _menuRetrievalRepository.GetItemsByCategoryIdAsync(categoryId);

                if (items.Count() == 0)
                {
                    _logger.LogError($"No items were found for Category ID: {categoryId}.");
                    return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
                }

                foreach (var item in items)
                {
                    var itemPrice = await _menuRetrievalRepository.GetItemPriceByItemIdAsync((int)item.ItemID);

                    if (itemPrice == null)
                    {
                        _logger.LogError($"No item price was found for Item ID: {item.ItemID}");
                        return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
                    }

                    var soldItems = await _orderRepository.GetOrderItemsByItemPriceIdAsync((int)itemPrice.ItemPriceID);

                    if (soldItems.Count() == 0)
                    {
                        _logger.LogError($"No order items were found for ItemPrice ID: {itemPrice.ItemPriceID}.");
                        return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
                    }

                    var categoryItem = new CategoryItemFilter
                    {
                        ItemName = item.ItemName,
                        ItemReports = new List<ItemReportFilter>()
                    };

                    foreach (var si in soldItems)
                    {
                        var report = new ItemReportFilter
                        {
                            Price = (decimal)itemPrice.Price,
                            Quantity = si.Quantity,
                            ExtendedPrice = si.ExtendedPrice,
                            DateSold = si.CafeOrder.OrderDate
                        };

                        dto.TotalQuantity += si.Quantity;
                        dto.TotalRevenue += si.ExtendedPrice;

                        categoryItem.ItemReports.Add(report);
                    }

                    dto.CategoryItems.Add(categoryItem);
                }

                return ResultFactory.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to filter items by Category ID {categoryId}: {ex.Message}");
                return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
            }
        }

        /// <summary>
        /// Attempts to retrieve an ItemPrice record. 
        /// If successful, sold items are retrieved and the data is mapped to a report.
        /// A sales report is generated for each date the item was sold.
        /// </summary>
        /// <param name="itemId">An ItemID used to retrieve an ItemPrice record.</param>
        /// <returns>A Result DTO with an ItemCategoryFilter DTO as its data.</returns>
        public async Task<Result<ItemCategoryFilter>> FilterItemsByItemIdAsync(int itemId)
        {
            try
            {
                var dto = new ItemCategoryFilter()
                {
                    SelectedItemID = itemId,
                    Reports = new List<ItemReportFilter>()
                };

                var itemPrice = await _menuRetrievalRepository.GetItemPriceByItemIdAsync(itemId);

                if (itemPrice == null)
                {
                    _logger.LogError($"Item price not found for Item ID {itemId}.");
                    return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator");
                }

                var orderItems = await _orderRepository.GetOrderItemsByItemPriceIdAsync((int)itemPrice.ItemPriceID);

                if (orderItems.Count() == 0)
                {
                    _logger.LogError($"No order items found for ItemPrice ID: {itemPrice.ItemPriceID}.");
                    return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator");
                }

                foreach (var item in orderItems)
                {
                    dto.TotalQuantity += item.Quantity;
                    dto.TotalRevenue += item.ExtendedPrice;

                    var report = new ItemReportFilter
                    {
                        Price = (decimal)itemPrice.Price,
                        Quantity = item.Quantity,
                        ExtendedPrice = item.ExtendedPrice,
                        DateSold = item.CafeOrder.OrderDate
                    };

                    dto.Reports.Add(report);
                }

                return ResultFactory.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to filter items by item Id {itemId}: {ex.Message}");
                return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
            }
        }

        /// <summary>
        /// Retrieves all CafeOrder records. If successful, records are filtered by date and payment status.
        /// Then, revenue is calculated and the filtered data is returned. 
        /// </summary>
        /// <param name="date">A DateTime object used to filter orders.</param>
        /// <returns>A Result DTO with an OrderFilter DTO as its data.</returns>
        public async Task<Result<OrderDateFilter>> FilterOrdersByDateAsync(DateTime date)
        {
            try
            {
                var filter = new OrderDateFilter
                {
                    Orders = new List<CafeOrder>(),
                    Revenue = 0.00M
                };

                var orders = await _orderRepository.GetAllOrdersAsync();

                if (orders.Count() == 0)
                {
                    _logger.LogError("Cafe orders not found.");
                    return ResultFactory.Fail<OrderDateFilter>("An error occurred. Please try again in a few minutes.");
                }

                filter.Orders = orders
                    .Where(o => o.OrderDate.Date == date.Date && o.PaymentStatusID == 1)
                    .ToList();

                foreach (var o in filter.Orders)
                {
                    filter.Revenue += o.SubTotal;
                }

                return ResultFactory.Success(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving orders: {ex.Message}");
                return ResultFactory.Fail<OrderDateFilter>("An error occurred. Please contact the administrator.");
            }
        }
    }
}