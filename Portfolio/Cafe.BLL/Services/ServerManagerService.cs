using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning servers.
    /// </summary>
    public class ServerManagerService : IServerManagerService
    {
        private readonly ILogger _logger;
        private readonly IServerManagerRepository _serverManagerRepository;

        /// <summary>
        /// Constructs a service with the dependencies required to manage servers.
        /// </summary>
        /// <param name="logger">A dependency used for logging errors.</param>
        /// <param name="serverManagerRepository">A dependency used for adding, retrieving, and updating servers.</param>
        public ServerManagerService(ILogger<ServerManagerService> logger, IServerManagerRepository serverManagerRepository)
        {
            _logger = logger;
            _serverManagerRepository = serverManagerRepository;
        }

        public async Task<Result> AddServerAsync(Server server)
        {
            try
            {
                await _serverManagerRepository.AddServerAsync(server);
                return ResultFactory.Success("New server successfully added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to add a server: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact the site administrator.");
            }
        }

        public async Task<Result<List<Server>>> GetAllServersAsync()
        {
            try
            {
                var servers = await _serverManagerRepository.GetAllServersAsync();

                if (servers.Count() == 0)
                {
                    _logger.LogError("No servers were found during retrieval attempt.");
                    return ResultFactory.Fail<List<Server>>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(servers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve a list of servers: {ex.Message}");
                return ResultFactory.Fail<List<Server>>("An error occurred. Please contact the site administrator.");
            }
        }

        public async Task<Result<Server>> GetServerByIdAsync(int serverId)
        {
            try
            {
                var server = await _serverManagerRepository.GetServerByIdAsync(serverId);

                if (server == null)
                {
                    _logger.LogError($"Server with id: {serverId} not found.");
                    return ResultFactory.Fail<Server>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(server);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve a server: {ex.Message}");
                return ResultFactory.Fail<Server>("An error occurred. Please contact the site administrator.");
            }
        }

        public async Task<Result> UpdateServerAsync(Server server)
        {
            try
            {
                await _serverManagerRepository.UpdateServerAsync(server);
                return ResultFactory.Success("Server successfully updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to update a server: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact the site administrator.");
            }
        }
    }
}