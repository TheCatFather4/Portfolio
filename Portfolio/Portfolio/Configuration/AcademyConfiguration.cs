using Academy.Core.Interfaces.Application;

namespace Portfolio.Configuration
{
    /// <summary>
    /// Implements IAcademyConfiguration to handle the configuration of the 4th Wall Academy application.
    /// </summary>
    public class AcademyConfiguration : IAcademyConfiguration
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructs a configuration object with the required dependency.
        /// </summary>
        /// <param name="configuration">An implementation of the IConfiguration interface.</param>
        public AcademyConfiguration(IConfiguration configuration)
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
    }
}