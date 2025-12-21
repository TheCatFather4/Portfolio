using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning menu retrieval.
    /// </summary>
    public class MenuRetrievalService : IMenuRetrievalService
    {
        private readonly ILogger _logger;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;

        /// <summary>
        /// Constructs a service with the dependencies required for menu retrieval.
        /// </summary>
        /// <param name="logger">A dependency used for logging error messages.</param>
        /// <param name="menuRetrievalRepository">A dependency used for adding and updating Item records.</param>
        public MenuRetrievalService(ILogger<MenuRetrievalService> logger, IMenuRetrievalRepository menuRetrievalRepository)
        {
            _logger = logger;
            _menuRetrievalRepository = menuRetrievalRepository;
        }

        /// <summary>
        /// Invokes the menu retrieval repository and checks if any Item records are found. 
        /// If successful, the entities are mapped to ItemResponse DTOs.
        /// Note: This method is used by the API menu controller.
        /// </summary>
        /// <returns>A Result DTO containing a List of ItemResponse DTOs and their associated ItemPriceResponse DTOs.</returns>
        public async Task<Result<List<ItemResponse>>> GetAllItemsAPIAsync()
        {
            try
            {
                // This method also returns ItemPrice records joined with each associated Item record.
                var menu = await _menuRetrievalRepository.GetAllItemsAsync();

                if (menu != null)
                {
                    var items = new List<ItemResponse>();

                    foreach (var item in menu)
                    {
                        var ir = new ItemResponse();
                        ir.ItemID = (int)item.ItemID;
                        ir.CategoryID = (int)item.CategoryID;
                        ir.ItemName = item.ItemName;
                        ir.ItemDescription = item.ItemDescription;
                        ir.Prices = new List<ItemPriceResponse>();

                        foreach (var price in item.Prices)
                        {
                            var ipr = new ItemPriceResponse();
                            ipr.ItemPriceID = (int)price.ItemPriceID;
                            ipr.TimeOfDayID = (int)price.TimeOfDayID;
                            ipr.Price = (decimal)price.Price;
                            ipr.StartDate = (DateTime)price.StartDate;
                            ipr.EndDate = price.EndDate;

                            ir.Prices.Add(ipr);
                        }

                        items.Add(ir);
                    }

                    return ResultFactory.Success(items);
                }

                _logger.LogError("The menu was not found. Check database connection.");
                return ResultFactory.Fail<List<ItemResponse>>("An error occurred. Please try again in a few minutes.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve the menu: {ex.Message}");
                return ResultFactory.Fail<List<ItemResponse>>("An error occurred. Please contact the customer assistance team.");
            }
        }

        /// <summary>
        /// Invokes the menu retrieval repository and checks if any Item records are found. 
        /// If successful, entities are returned.
        /// Note: This method is used by the MVC menu controller. 
        ///       It is scheduled to be consolidated with the above method.
        /// </summary>
        /// <returns>A Result DTO containing a List of Item and joined ItemPrice entities.</returns>
        public async Task<Result<List<Item>>> GetAllItemsMVCAsync()
        {
            try
            {
                // This method also returns ItemPrice records joined with each associated Item record.
                var items = await _menuRetrievalRepository.GetAllItemsAsync();

                if (items.Count() == 0)
                {
                    _logger.LogError("Items not found in database. Check the database connection.");
                    return ResultFactory.Fail<List<Item>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in retrieving menu items: {ex.Message}");
                return ResultFactory.Fail<List<Item>>("An error occurred. Please contact our management team.");
            }
        }

        /// <summary>
        /// Invokes the menu retrieval repository and counts the retrieved Category records. 
        /// If successful, the list of entities is returned.
        /// </summary>
        /// <returns>A Result DTO containing a List of Category entities.</returns>
        public async Task<Result<List<Category>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _menuRetrievalRepository.GetCategoriesAsync();

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

        /// <summary>
        /// Invokes the menu retrieval repository and checks if an Item record is found. 
        /// If successful, the entity is mapped to an ItemResponse DTO and returned.
        /// Note: This method is used by the API menu controller.
        /// </summary>
        /// <param name="itemId">The ItemID used for looking up an Item record.</param>
        /// <returns>A Result DTO that contains an ItemResponse DTO as its data.</returns>
        public async Task<Result<ItemResponse>> GetItemByIdAPIAsync(int itemId)
        {
            try
            {
                // This method also returns any ItemPrice records joined to each Item record.
                var item = await _menuRetrievalRepository.GetItemByIdAsync(itemId);

                if (item == null)
                {
                    _logger.LogError($"Item with ID: {itemId} was not found.");
                    return ResultFactory.Fail<ItemResponse>("An error occurred. Please try again in a few minutes.");
                }

                var ir = new ItemResponse();
                ir.ItemID = (int)item.ItemID;
                ir.CategoryID = (int)item.CategoryID;
                ir.ItemName = item.ItemName;
                ir.ItemDescription = item.ItemDescription;
                ir.Prices = new List<ItemPriceResponse>();

                foreach (var price in item.Prices)
                {
                    var ipr = new ItemPriceResponse();
                    ipr.ItemPriceID = (int)price.ItemPriceID;
                    ipr.TimeOfDayID = (int)price.TimeOfDayID;
                    ipr.Price = (decimal)price.Price;
                    ipr.StartDate = (DateTime)price.StartDate;
                    ipr.EndDate = price.EndDate;

                    ir.Prices.Add(ipr);
                }

                return ResultFactory.Success(ir);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred in attempting to retrieve an item: {ex.Message}");
                return ResultFactory.Fail<ItemResponse>("An error occurred. Please contact our management team.");
            }
        }

        /// <summary>
        /// Invokes the menu retrieval repository and checks if an Item record is found. 
        /// If successful, the entity is returned.
        /// Note: This method is used by the MVC management and shopping cart controller. 
        ///       It is scheduled to be consolidated with the above method.
        /// </summary>
        /// <param name="itemID">The ItemID used for looking up an Item record.</param>
        /// <returns>A Result DTO that contains an Item entity as its data.</returns>
        public async Task<Result<Item>> GetItemByIdMVCAsync(int itemID)
        {
            try
            {
                var item = await _menuRetrievalRepository.GetItemByIdAsync(itemID);

                if (item == null)
                {
                    _logger.LogError($"Item with id: {itemID} not found.");
                    return ResultFactory.Fail<Item>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when retrieving a menu item: {ex.Message}");
                return ResultFactory.Fail<Item>("An error occurred. Please contact the administrator.");
            }
        }

        /// <summary>
        /// Invokes the menu retrieval repository and counts the retrieved TimeOfDay records. 
        /// If successful, the list of entities is returned.
        /// </summary>
        /// <returns>A Result DTO containing a List of TimeOfDay entities.</returns>
        public async Task<Result<List<TimeOfDay>>> GetTimeOfDaysAsync()
        {
            try
            {
                var times = await _menuRetrievalRepository.GetTimeOfDaysAsync();

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
    }
}