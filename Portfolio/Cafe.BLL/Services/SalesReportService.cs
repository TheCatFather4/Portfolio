using Cafe.Core.DTOs;
using Cafe.Core.Entities;
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

        public async Task<Result<ItemReportFilter>> FilterItemsByItemIdAsync(int itemId)
        {
            try
            {
                var dto = new ItemReportFilter()
                {
                    SelectedItemID = itemId
                };

                var itemPrice = await _menuRetrievalRepository.GetItemPriceByItemIdAsync(itemId);

                if (itemPrice == null)
                {
                    _logger.LogError($"Item price not found for Item ID {itemId}.");
                    return ResultFactory.Fail<ItemReportFilter>("An error occurred. Please contact the site administrator");
                }

                var orderItems = _orderRepository.GetOrderItemsByItemPriceId((int)itemPrice.ItemPriceID);

                if (orderItems.Count() == 0)
                {
                    _logger.LogError($"No order items found for ItemPrice ID: {itemPrice.ItemPriceID}.");
                    return ResultFactory.Fail<ItemReportFilter>("An error occurred. Please contact the site administrator");
                }

                var itemDateFilters = new List<ItemDateReportFilter>();

                foreach (var item in orderItems)
                {
                    dto.TotalQuantity += item.Quantity;
                    dto.TotalRevenue += item.ExtendedPrice;

                    var itemDateFilter = new ItemDateReportFilter
                    {
                        Price = (decimal)itemPrice.Price,
                        Quantity = item.Quantity,
                        ExtendedPrice = item.ExtendedPrice,
                        DateSold = item.CafeOrder.OrderDate
                    };

                    itemDateFilters.Add(itemDateFilter);
                }

                dto.Dates = itemDateFilters;

                return ResultFactory.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to filter items by item Id {itemId}: {ex.Message}");
                return ResultFactory.Fail<ItemReportFilter>("An error occurred. Please contact the site administrator.");
            }
        }

        public Result<OrderDateFilter> FilterOrdersByDate(DateTime date)
        {
            try
            {
                decimal revenue = 0.00M;

                var orders = _orderRepository.GetAllOrders();

                if (orders.Count() == 0)
                {
                    _logger.LogError("Cafe orders not found.");
                    return ResultFactory.Fail<OrderDateFilter>("An error occurred. Please try again in a few minutes.");
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

                var filter = new OrderDateFilter
                {
                    Orders = filteredOrders,
                    Revenue = revenue
                };

                return ResultFactory.Success(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving orders: {ex.Message}");
                return ResultFactory.Fail<OrderDateFilter>("An error occurred. Please contact the administrator.");
            }
        }

        public Result<List<OrderItem>> GetOrderItemsByItemPriceId(int itemPriceId)
        {
            try
            {
                var orderItems = _orderRepository.GetOrderItemsByItemPriceId(itemPriceId);

                if (orderItems.Count() == 0)
                {
                    _logger.LogError($"Order items with item price id: {itemPriceId} not found.");
                    return ResultFactory.Fail<List<OrderItem>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(orderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving order items: {ex.Message}");
                return ResultFactory.Fail<List<OrderItem>>("An error occurred. Please contact the administrator.");
            }
        }
    }
}