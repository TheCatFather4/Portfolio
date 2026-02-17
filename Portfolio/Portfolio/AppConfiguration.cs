using Cafe.Core.Enums;
using Cafe.Core.Interfaces.Application;

namespace Portfolio
{
    /// <summary>
    /// Implements IAppConfiguration to handle the configuration of the 4th Wall Café application.
    /// </summary>
    public class AppConfiguration : IAppConfiguration
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructs a configuration object with the required dependency.
        /// </summary>
        /// <param name="configuration">An implementation of the IConfiguration interface.</param>
        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves the connection string from the appsettings.json file.
        /// </summary>
        /// <returns>A connection string</returns>
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("CafeDb") ?? "";
        }

        /// <summary>
        /// Retrieves the database mode from the appsettings.json file.
        /// </summary>
        /// <returns>A DatabaseMode enum.</returns>
        /// <exception cref="Exception"></exception>
        public DatabaseMode GetDatabaseMode()
        {
            if (string.IsNullOrEmpty(_configuration["DatabaseMode"]))
            {
                throw new Exception("DatabaseMode configuration key missing.");
            }

            switch (_configuration["DatabaseMode"])
            {
                case "ORM":
                    return DatabaseMode.ORM;
                case "Dapper":
                    return DatabaseMode.Dapper;
                default:
                    throw new Exception("DatabaseMode configuration key invalid.");
            }
        }
    }
}