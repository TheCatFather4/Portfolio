using Cafe.BLL.Services;
using Cafe.Core.Enums;
using Cafe.Core.Interfaces.Application;
using Cafe.Core.Interfaces.Services;
using Cafe.Data.Repositories.Dapper;
using Cafe.Data.Repositories.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL
{
    /// <summary>
    /// A factory class that handles the business logic for instantiating services.
    /// </summary>
    public class ServiceFactory
    {
        private readonly IAppConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _jwtConfig;

        /// <summary>
        /// Constructs a factory that has the state required for connection strings, database settings, logger creation, web token generation.
        /// </summary>
        /// <param name="config">A dependency used for retrieving connection strings and database settings.</param>
        /// <param name="loggerFactory">A dependency used for instantiating loggers.</param>
        /// <param name="jwtConfig">A dependency used for generating JSON web tokens.</param>
        public ServiceFactory(IAppConfiguration config, ILoggerFactory loggerFactory, IConfiguration jwtConfig)
        {
            _config = config;
            _loggerFactory = loggerFactory;
            _jwtConfig = jwtConfig;
        }

        /// <summary>
        /// Instantiates a logger and returns a customer service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A customer service dependency.</returns>
        public ICustomerService CreateCustomerService()
        {
            var logger = _loggerFactory.CreateLogger<CustomerService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new CustomerService(
                    logger,
                    new EFCustomerRepository(_config.GetConnectionString()),
                    new EFShoppingBagRepository(_config.GetConnectionString()));
            }
            else
            {
                return new CustomerService(
                    logger,
                    new DapperCustomerRepository(_config.GetConnectionString()),
                    new DapperShoppingBagRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Instantiates a logger and returns a menu manager service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A menu manager service dependency.</returns>
        public IMenuManagerService CreateMenuManagerService()
        {
            var logger = _loggerFactory.CreateLogger<MenuManagerService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new MenuManagerService(
                    logger,
                    new EFMenuManagerRepository(_config.GetConnectionString()),
                    new EFMenuRetrievalRepository(_config.GetConnectionString()));
            }
            else
            {
                return new MenuManagerService(
                    logger,
                    new DapperMenuManagerRepository(_config.GetConnectionString()),
                    new DapperMenuRetrievalRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Instantiates a logger and returns a menu retrieval service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A menu retrieval service dependency.</returns>
        public IMenuRetrievalService CreateMenuRetrievalService()
        {
            var logger = _loggerFactory.CreateLogger<MenuRetrievalService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new MenuRetrievalService(
                    logger,
                    new EFMenuRetrievalRepository(_config.GetConnectionString()));
            }
            else
            {
                return new MenuRetrievalService(
                    logger,
                    new DapperMenuRetrievalRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Instantiates a logger and returns an order service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>An order service dependency.</returns>
        public IOrderService CreateOrderService()
        {
            var logger = _loggerFactory.CreateLogger<OrderService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new OrderService(
                    logger,
                    new EFMenuRetrievalRepository(_config.GetConnectionString()),
                    new EFOrderRepository(_config.GetConnectionString()),
                    new EFShoppingBagRepository(_config.GetConnectionString()));
            }
            else
            {
                return new OrderService(
                    logger,
                    new DapperMenuRetrievalRepository(_config.GetConnectionString()),
                    new DapperOrderRepository(_config.GetConnectionString()),
                    new DapperShoppingBagRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Instantiates a logger and returns a payment service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A payment service dependency.</returns>
        public IPaymentService CreatePaymentService()
        {
            var logger = _loggerFactory.CreateLogger<PaymentService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new PaymentService(
                    logger,
                    new EFOrderRepository(_config.GetConnectionString()),
                    new EFPaymentRepository(_config.GetConnectionString()));
            }
            else
            {
                return new PaymentService(
                    logger,
                    new DapperOrderRepository(_config.GetConnectionString()),
                    new DapperPaymentRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Instantiates a logger and returns a sales report service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A sales report service dependency.</returns>
        public ISalesReportService CreateSalesReportService()
        {
            var logger = _loggerFactory.CreateLogger<SalesReportService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new SalesReportService(
                    logger,
                    new EFMenuRetrievalRepository(_config.GetConnectionString()),
                    new EFOrderRepository(_config.GetConnectionString()));
            }
            else
            {
                return new SalesReportService(
                    logger,
                    new DapperMenuRetrievalRepository(_config.GetConnectionString()),
                    new DapperOrderRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Instantiates a logger and returns a server manager service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A server manager service dependency.</returns>
        public IServerManagerService CreateServerManagerService()
        {
            var logger = _loggerFactory.CreateLogger<ServerManagerService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new ServerManagerService(
                    logger,
                    new EFServerManagerRepository(_config.GetConnectionString()));
            }
            else
            {
                return new ServerManagerService(
                    logger,
                    new DapperServerManagerRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Instantiates a logger and returns a shopping bag service dependency.
        /// The repositories returned are in accord with the current database setting.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A shopping bag service dependency.</returns>
        public IShoppingBagService CreateShoppingBagService()
        {
            var logger = _loggerFactory.CreateLogger<ShoppingBagService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new ShoppingBagService(
                    logger,
                    new EFShoppingBagRepository(_config.GetConnectionString()));
            }
            else
            {
                return new ShoppingBagService(
                    logger,
                    new DapperShoppingBagRepository(_config.GetConnectionString()));
            }
        }

        /// <summary>
        /// Uses the configuration implementation to instatiate a web token service.
        /// See the appsettings.json file for more details.
        /// </summary>
        /// <returns>A web token service dependency.</returns>
        public IWebTokenService CreateWebTokenService()
        {
            return new WebTokenService(_jwtConfig);
        }
    }
}