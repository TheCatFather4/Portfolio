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
                    return ResultFactory.Fail<List<Category>>("Error getting categories.");
                }

                return ResultFactory.Success(categories);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Category>>(ex.Message);
            }
        }

        public Result<List<TimeOfDay>> GetTimeOfDays()
        {
            try
            {
                var times = _menuRepository.GetTimeOfDays();

                if (times.Count() == 0)
                {
                    return ResultFactory.Fail<List<TimeOfDay>>("Error getting times.");
                }

                return ResultFactory.Success(times);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<TimeOfDay>>(ex.Message);
            }
        }

        public Result<List<Item>> GetMenu()
        {
            try
            {
                var items = _menuRepository.GetMenu();

                if (!items.Any())
                {
                    _logger.LogWarning("No menu items were found in the repository.");
                    return ResultFactory.Fail<List<Item>>("No menu items found.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving the menu.");
                return ResultFactory.Fail<List<Item>>("An unexpected error occurred.");
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

        public Result<List<Item>> GetItemsByCatetegory(string catetegoryName)
        {
            try
            {
                var items = _menuRepository.GetItemsByCategory(catetegoryName);

                if (items.Count() == 0)
                {
                    return ResultFactory.Fail<List<Item>>("There are no items for that category");
                }
                
                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Item>>(ex.Message);
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
