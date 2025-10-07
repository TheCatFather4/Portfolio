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

        public Result<ItemPrice> GetItemPriceByItemId(int itemId)
        {
            try
            {
                var itemPrice = _accountantRepository.GetItemPriceByItemId(itemId);

                if (itemPrice == null)
                {
                    _logger.LogError($"Item price with item id: {itemId} not found.");
                    return ResultFactory.Fail<ItemPrice>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(itemPrice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving item price: {ex.Message}");
                return ResultFactory.Fail<ItemPrice>("An error occurred. Please contact the administrator.");
            }
        }

        public Result<List<Item>> GetItemsByCategoryID(int categoryID)
        {
            try
            {
                var items = _accountantRepository.GetItemsByCategoryID(categoryID);

                if (items.Count() == 0)
                {
                    _logger.LogError($"Items with category id: {categoryID} not found.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving items by category: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact the administrator.");
            }
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