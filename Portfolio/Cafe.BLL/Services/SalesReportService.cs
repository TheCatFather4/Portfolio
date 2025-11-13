using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class SalesReportService : ISalesReportService
    {
        private readonly ILogger _logger;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;
        private readonly IOrderRepository _orderRepository;

        public SalesReportService(ILogger<SalesReportService> logger, IMenuRetrievalRepository menuRetrievalRepository, IOrderRepository orderRepository)
        {
            _logger = logger;
            _menuRetrievalRepository = menuRetrievalRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<ItemCategoryFilter>> FilterItemsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var dto = new ItemCategoryFilter
                {
                    SelectedCategoryID = categoryId
                };

                // 1. Get items by CategoryID
                var items = _menuRetrievalRepository.GetItemsByCategoryId(categoryId);

                // 2. If ok, continue
                if (items.Count() == 0)
                {
                    _logger.LogError($"No items were found for Category ID: {categoryId}.");
                    return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
                }

                // 3. This is the list of lists
                var categoryReports = new List<CategoryFilter>();

                // 4. Loop through each item from step 1
                foreach (var item in items)
                {
                    // for the current item being iterated....

                    // 5. New CategoryReport
                    var categoryReport = new CategoryFilter();

                    // 6. Map item name to the new report
                    categoryReport.ItemName = item.ItemName;

                    // 7. Get item price
                    var itemPrice = await _menuRetrievalRepository.GetItemPriceByItemIdAsync((int)item.ItemID);

                    // 8. If ok, continue
                    if (itemPrice == null)
                    {
                        _logger.LogError($"No item price was found for Item ID: {item.ItemID}");
                        return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
                    }

                    // 9. Get sold order items
                    var soldOrderItems = await _orderRepository.GetOrderItemsByItemPriceIdAsync((int)itemPrice.ItemPriceID);

                    // 10. If ok, continue
                    if (soldOrderItems.Count() == 0)
                    {
                        _logger.LogError($"No order items were found for ItemPrice ID: {itemPrice.ItemPriceID}.");
                        return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
                    }

                    // 11. Create new Item Date Report List - list of lists
                    var itemDateReports = new List<ItemFilter>();

                    // 12. Loop through sold items
                    foreach (var soi in soldOrderItems)
                    {
                        // model quantity
                        dto.TotalQuantity += soi.Quantity;

                        // model extended price
                        dto.TotalRevenue += soi.ExtendedPrice;

                        // 13. Map each sold item's data to a corresponding Item Date Report - many ItemDateReports for ONE CategoryReport 
                        var itemDateReport = new ItemFilter
                        {
                            Price = (decimal)itemPrice.Price, // from step 7.
                            Quantity = soi.Quantity,
                            ExtendedPrice = soi.ExtendedPrice,
                            DateSold = soi.CafeOrder.OrderDate
                        };

                        itemDateReports.Add(itemDateReport);
                    }

                    // 14. Map list of ItemDateReports to the Category report (that was instantiated at step 5)
                    categoryReport.ItemReports = itemDateReports;

                    // 15. Add Category Report to List of Category Reports
                    categoryReports.Add(categoryReport);
                }

                // 16. After looping through each item, and all category reports are added to the list, map category list to model
                dto.Categories = categoryReports;

                return ResultFactory.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to filter items by Category ID {categoryId}: {ex.Message}");
                return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
            }
        }

        public async Task<Result<ItemCategoryFilter>> FilterItemsByItemIdAsync(int itemId)
        {
            try
            {
                var dto = new ItemCategoryFilter()
                {
                    SelectedItemID = itemId
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

                var itemDateFilters = new List<ItemFilter>();

                foreach (var item in orderItems)
                {
                    dto.TotalQuantity += item.Quantity;
                    dto.TotalRevenue += item.ExtendedPrice;

                    var itemDateFilter = new ItemFilter
                    {
                        Price = (decimal)itemPrice.Price,
                        Quantity = item.Quantity,
                        ExtendedPrice = item.ExtendedPrice,
                        DateSold = item.CafeOrder.OrderDate
                    };

                    itemDateFilters.Add(itemDateFilter);
                }

                dto.Items = itemDateFilters;

                return ResultFactory.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to filter items by item Id {itemId}: {ex.Message}");
                return ResultFactory.Fail<ItemCategoryFilter>("An error occurred. Please contact the site administrator.");
            }
        }

        public async Task<Result<OrderFilter>> FilterOrdersByDateAsync(DateTime date)
        {
            try
            {
                decimal revenue = 0.00M;

                var orders = await _orderRepository.GetAllOrdersAsync();

                if (orders.Count() == 0)
                {
                    _logger.LogError("Cafe orders not found.");
                    return ResultFactory.Fail<OrderFilter>("An error occurred. Please try again in a few minutes.");
                }

                var filteredOrders = orders
                    .Where(o => o.OrderDate.Date == date.Date)
                    .ToList();

                foreach (var o in filteredOrders)
                {
                    if (o.PaymentStatusID == 1)
                    {
                        revenue += o.SubTotal;
                    }
                }

                var filter = new OrderFilter
                {
                    Orders = filteredOrders,
                    Revenue = revenue
                };

                return ResultFactory.Success(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving orders: {ex.Message}");
                return ResultFactory.Fail<OrderFilter>("An error occurred. Please contact the administrator.");
            }
        }
    }
}