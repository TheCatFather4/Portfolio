using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class MenuService : IMenuService
    {
        private readonly ILogger _logger;
        private readonly IMenuRepository _menuRepository;

        public MenuService(ILogger<MenuService> logger, IMenuRepository menuRepository)
        {
            _logger = logger;
            _menuRepository = menuRepository;
        }

        public Result<List<Category>> GetCategories()
        {
            try
            {
                var categories = _menuRepository.GetCategories();

                if (categories.Count() == 0)
                {
                    _logger.LogError("No categories were found. Check the database connection.");
                    return ResultFactory.Fail<List<Category>>("An error occurred. Please wait a few minutes and try again.");
                }

                return ResultFactory.Success(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in attempting to get categories: {ex.Message}");
                return ResultFactory.Fail<List<Category>>("An error occurred. Please contact our management team.");
            }
        }

        public Result<List<TimeOfDay>> GetTimeOfDays()
        {
            try
            {
                var times = _menuRepository.GetTimeOfDays();

                if (times.Count() == 0)
                {
                    _logger.LogError("No times of day were found. Check the database connection.");
                    return ResultFactory.Fail<List<TimeOfDay>>("An error occurred. Please wait a few minutes and try again.");
                }

                return ResultFactory.Success(times);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in attempting to get times of day: {ex.Message}");
                return ResultFactory.Fail<List<TimeOfDay>>("An error occurred. Please contact our management team.");
            }
        }

        public Result<List<Item>> GetMenu()
        {
            try
            {
                var items = _menuRepository.GetMenu();

                if (!items.Any())
                {
                    _logger.LogError("No menu items were found. Check the database connection.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please wait a few minutes and try again.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving the menu: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact our management team.");
            }
        }

        public Result<Item> GetItem(int id)
        {
            try
            {
                var item = _menuRepository.GetItem(id);

                if (item == null)
                {
                    _logger.LogWarning("Item was not found in the repository.");
                    return ResultFactory.Fail<Item>("");
                }

                return ResultFactory.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving an item.");
                return ResultFactory.Fail<Item>("An unexpected error occurred.");
            }
        }

        public Result<List<Item>> GetItems()
        {
            try
            {
                var items = _menuRepository.GetItems();

                if (items.Count() == 0)
                {
                    return ResultFactory.Fail<List<Item>>("Error getting items.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Item>>(ex.Message);
            }
        }

        public async Task<Result<ItemPrice>> GetItemPriceByIdAsync(int itemId)
        {
            var itemPrice = await _menuRepository.GetItemPriceByIdAsync(itemId);
            if (itemPrice == null)
            {
                return ResultFactory.Fail<ItemPrice>($"Item price for item {itemId} not found.");
            }

            return ResultFactory.Success(itemPrice);
        }
    }
}