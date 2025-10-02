using Cafe.Core.Enums;
using Cafe.Core.Interfaces.Application;

namespace Portfolio
{
    public class AppConfiguration : IAppConfiguration
    {
        private readonly IConfiguration _configuration;

        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("CafeDb") ?? "";
        }

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