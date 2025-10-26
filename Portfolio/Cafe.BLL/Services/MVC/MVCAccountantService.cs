using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services.MVC
{
    public class MVCAccountantService : IAccountantService
    {
        private readonly ILogger _logger;
        private readonly IAccountantRepository _accountantRepository;

        public MVCAccountantService(ILogger<MVCAccountantService> logger, IAccountantRepository accountantRepository)
        {
            _logger = logger;
            _accountantRepository = accountantRepository;
        }

        public Result<List<OrderItem>> GetOrderItemsByItemPriceId(int itemPriceId)
        {
            try
            {
                var orderItems = _accountantRepository.GetOrderItemsByItemPriceId(itemPriceId);

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

        public Result<List<CafeOrder>> GetOrders()
        {
            try
            {
                var orders = _accountantRepository.GetOrders();

                if (orders.Count() == 0)
                {
                    _logger.LogError("Cafe orders not found.");
                    return ResultFactory.Fail<List<CafeOrder>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving orders: {ex.Message}");
                return ResultFactory.Fail<List<CafeOrder>>("An error occurred. Please contact the administrator.");
            }
        }
    }
}