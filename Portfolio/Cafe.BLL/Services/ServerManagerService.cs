using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class ServerManagerService : IServerManagerService
    {
        private readonly ILogger _logger;
        private readonly IServerManagerRepository _serverManagerRepository;

        public ServerManagerService(ILogger<ServerManagerService> logger, IServerManagerRepository serverManagerRepository)
        {
            _logger = logger;
            _serverManagerRepository = serverManagerRepository;
        }

        public Result AddServer(Server server)
        {
            try
            {
                _serverManagerRepository.AddServer(server);
                return ResultFactory.Success("New server successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add a server: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact the administrator.");
            }
        }

        public Result<Server> GetServerById(int serverID)
        {
            try
            {
                var server = _serverManagerRepository.GetServerById(serverID);

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
                var servers = _serverManagerRepository.GetServers();

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

        public Result UpdateServer(Server server)
        {
            try
            {
                _serverManagerRepository.UpdateServer(server);
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