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
    /// Factory methods used to instantiate services.
    /// </summary>
    public class ServiceFactory
    {
        private readonly IAppConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _jwtConfig;

        /// <summary>
        /// Constructs a factory that has the state required to get a connection string, set a database mode, create loggers, and generate JSON Web Tokens.
        /// </summary>
        /// <param name="config">An implementation of the IAppConfiguration interface.</param>
        /// <param name="loggerFactory">An implmentation of the ILoggerFactory interface.</param>
        /// <param name="jwtConfig">An implementation of the IConfiguration interface.</param>
        public ServiceFactory(IAppConfiguration config, ILoggerFactory loggerFactory, IConfiguration jwtConfig)
        {
            _config = config;
            _loggerFactory = loggerFactory;
            _jwtConfig = jwtConfig;
        }

        /// <summary>
        /// Instantiates a service concerning customers
        /// </summary>
        /// <returns>A customer service implementatation</returns>
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
        /// Instantiates a service concerning menu management
        /// </summary>
        /// <returns>A menu management service implementatation</returns>
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
        /// Instantiates a service concerning menu retrieval
        /// </summary>
        /// <returns>A menu retrieval service implementatation</returns>
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
        /// Instantiates a service concerning cafe orders
        /// </summary>
        /// <returns>A cafe order service implementatation</returns>
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
        /// Instantiates a service concerning payments
        /// </summary>
        /// <returns>A payment service implementatation</returns>
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
        /// Instantiates a service concerning menu management
        /// </summary>
        /// <returns>A menu management service implementatation</returns>
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
        /// Instantiates a service concerning server management
        /// </summary>
        /// <returns>A server service implementatation</returns>
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
        /// Instantiates a service concerning shopping bags
        /// </summary>
        /// <returns>A shopping bag service implementatation</returns>
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
        /// Instantiates a service concerning JSON Web Tokens
        /// </summary>
        /// <returns>A Json Web Token service implementatation</returns>
        public IWebTokenService CreateWebTokenService()
        {
            return new WebTokenService(_jwtConfig);
        }
    }
}