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

        /// <summary>
        /// Attempts to add a new Server record by sending an entity to the repository.
        /// </summary>
        /// <param name="server">A Server entity to be added to the database.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
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

        /// <summary>
        /// Makes a call to the repository to retrieve all Server records.
        /// </summary>
        /// <returns>A Result DTO with a list of Server entities as its data.</returns>
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

        /// <summary>
        /// Attempts to retrieve a Server record by its Id. If successful, the data is returned.
        /// </summary>
        /// <param name="serverId">A ServerID used in retrieving a Server record.</param>
        /// <returns>A Result DTO with a Server entity as its data.</returns>
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

        /// <summary>
        /// Sends a current Server record to the repository to be updated in the database.
        /// </summary>
        /// <param name="server">The current Server record to be updated.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
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