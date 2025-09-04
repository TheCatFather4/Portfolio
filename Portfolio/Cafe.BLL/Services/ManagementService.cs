using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;

namespace Cafe.BLL.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IManagementRepository _managementRepository;

        public ManagementService(IManagementRepository managementRepository)
        {
            _managementRepository = managementRepository;
        }

        public Result AddItem(Item item)
        {
            var duplicateItem = _managementRepository.IsDuplicateItem(item.ItemName);

            if (duplicateItem)
            {
                return ResultFactory.Fail("This item already exists on the menu.");
            }

            try
            {
                _managementRepository.AddItem(item);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result AddServer(Server server)
        {
            try
            {
                _managementRepository.AddServer(server);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<Item> GetMenuItemById(int itemID)
        {
            try
            {
                var item = _managementRepository.GetMenuItemById(itemID);

                if (item == null)
                {
                    return ResultFactory.Fail<Item>("Item not found.");
                }

                return ResultFactory.Success(item);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Item>(ex.Message);
            }
        }

        public Result<Server> GetServerById(int serverID)
        {
            try
            {
                var server = _managementRepository.GetServerById(serverID);

                if (server == null)
                {
                    return ResultFactory.Fail<Server>("Server not found.");
                }

                return ResultFactory.Success(server);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Server>(ex.Message);
            }
        }

        public Result<List<Server>> GetServers()
        {
            try
            {
                var servers = _managementRepository.GetServers();

                if (servers.Count() == 0)
                {
                    return ResultFactory.Fail<List<Server>>("Error gettting servers.");
                }

                return ResultFactory.Success(servers);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Server>>(ex.Message);
            }
        }

        public Result UpdateMenu(Item item)
        {
            try
            {
                _managementRepository.UpdateMenu(item);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
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
                return ResultFactory.Fail(ex.Message);
            }
        }
    }
}
