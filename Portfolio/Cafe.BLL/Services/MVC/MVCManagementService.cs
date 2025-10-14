using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services.MVC
{
    public class MVCManagementService : IManagementService
    {
        private readonly ILogger _logger;
        private readonly IManagementRepository _managementRepository;

        public MVCManagementService(ILogger<MVCManagementService> logger, IManagementRepository managementRepository)
        {
            _logger = logger;
            _managementRepository = managementRepository;
        }

        public Result AddItem(Item item)
        {
            var duplicateItem = _managementRepository.IsDuplicateItem(item.ItemName);

            if (duplicateItem)
            {
                return ResultFactory.Fail("The menu already has an item with this name.");
            }

            try
            {
                _managementRepository.AddItem(item);
                return ResultFactory.Success($"{item.ItemName} successfully added to the menu!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add an item to the menu: {ex.Message}");
                return ResultFactory.Fail("An error ocurred. Please contact the administrator.");
            }
        }

        public Result AddServer(Server server)
        {
            try
            {
                _managementRepository.AddServer(server);
                return ResultFactory.Success("New server successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add a server: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact the administrator.");
            }
        }

        public Result<Item> GetMenuItemById(int itemID)
        {
            try
            {
                var item = _managementRepository.GetMenuItemById(itemID);

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

        public Result<Server> GetServerById(int serverID)
        {
            try
            {
                var server = _managementRepository.GetServerById(serverID);

                if (server == null)
                {
                    _logger.LogError($"Server with id: {serverID} not found.");
                    return ResultFactory.Fail<Server>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(server);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when retrieving a server: {ex.Message}");
                return ResultFactory.Fail<Server>("An error occurred. Please contact the administrator.");
            }
        }

        public Result<List<Server>> GetServers()
        {
            try
            {
                var servers = _managementRepository.GetServers();

                if (servers.Count() == 0)
                {
                    _logger.LogError("No servers were found upon attempting to retrieve.");
                    return ResultFactory.Fail<List<Server>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(servers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when retrieving list of servers: {ex.Message}");
                return ResultFactory.Fail<List<Server>>("An error occurred. Please contact the site administrator.");
            }
        }

        public Result UpdateMenu(Item item)
        {
            try
            {
                _managementRepository.UpdateMenu(item);
                return ResultFactory.Success("Item successfully updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred when attempting to update item.");
                return ResultFactory.Fail("An error occurred. Please contact the site administrator.");
            }
        }

        public Result UpdateServer(Server server)
        {
            try
            {
                _managementRepository.UpdateServer(server);
                return ResultFactory.Success("Server successfully updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred when attempting to update server.");
                return ResultFactory.Fail("An error occurred. Please contact the site administrator.");
            }
        }
    }
}