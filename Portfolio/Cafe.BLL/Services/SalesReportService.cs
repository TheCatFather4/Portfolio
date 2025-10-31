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
        private readonly IOrderRepository _orderRepository;

        public SalesReportService(ILogger<SalesReportService> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
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
    }
}