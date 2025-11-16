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
    public class ServiceFactory
    {
        private readonly IAppConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _jwtConfig;

        public ServiceFactory(IAppConfiguration config, ILoggerFactory loggerFactory, IConfiguration jwtConfig)
        {
            _config = config;
            _loggerFactory = loggerFactory;
            _jwtConfig = jwtConfig;
        }

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

        public IWebTokenService CreateWebTokenService()
        {
            return new WebTokenService(_jwtConfig);
        }
    }
}