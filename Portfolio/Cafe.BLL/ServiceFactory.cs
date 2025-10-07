using Cafe.BLL.Services;
using Cafe.BLL.Services.API;
using Cafe.BLL.Services.MVC;
using Cafe.Core.Enums;
using Cafe.Core.Interfaces.Application;
using Cafe.Core.Interfaces.Services;
using Cafe.Core.Interfaces.Services.MVC;
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

        public IMenuService CreateMenuService()
        {
            var logger = _loggerFactory.CreateLogger<MenuService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new MenuService(
                    logger,
                    new EFMenuRepository(_config.GetConnectionString()));
            }
            else
            {
                return new MenuService(
                    logger,
                    new DapperMenuRepository(_config.GetConnectionString()));
            }
        }

        public IManagementService CreateManagementService()
        {
            var logger = _loggerFactory.CreateLogger<MVCManagementService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new MVCManagementService(
                    logger,
                    new EFManagementRepository(_config.GetConnectionString()));
            }
            else
            {
                return new MVCManagementService(
                    logger,
                    new DapperManagementRepository(_config.GetConnectionString()));
            }
        }

        public IAccountantService CreateAccountantService()
        {
            var logger = _loggerFactory.CreateLogger<MVCAccountantService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new MVCAccountantService(
                    logger,
                    new EFAccountantRepository(_config.GetConnectionString()));
            }
            else
            {
                return new MVCAccountantService(
                    logger,
                    new DapperAccountantRepository(_config.GetConnectionString()));
            }
        }

        public IShoppingBagService CreateShoppingBagService()
        {
            var logger = _loggerFactory.CreateLogger<ShoppingBagService>();

            return new ShoppingBagService(
                new ShoppingBagRepository(_config.GetConnectionString()), 
                new EFMenuRepository(_config.GetConnectionString()), logger);
        }

        public IOrderService CreateOrderService()
        {
            var logger = _loggerFactory.CreateLogger<APIOrderService>();

            return new APIOrderService(
                new OrderRepository(_config.GetConnectionString()), 
                CreateShoppingBagService(), 
                CreateMenuService(), 
                logger);
        }

        public ICustomerService CreateCustomerService()
        {
            var logger = _loggerFactory.CreateLogger<APICustomerService>();

            return new APICustomerService(
                new EFCustomerRepository(_config.GetConnectionString()), 
                logger);
        }

        public IJwtService CreateJwtService()
        {
            return new APIJwtService(_jwtConfig);
        }

        public IPaymentService CreatePaymentService()
        {
            var logger = _loggerFactory.CreateLogger<APIPaymentService>();

            return new APIPaymentService(
                new PaymentRepository(_config.GetConnectionString()), 
                logger);
        }

        public IMVCCustomerService CreateMVCCustomerService()
        {
            var logger = _loggerFactory.CreateLogger<MVCCustomerService>();

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new MVCCustomerService(
                    logger,
                    new EFCustomerRepository(_config.GetConnectionString()));
            }
            else
            {
                return new MVCCustomerService(
                    logger,
                    new DapperCustomerRepository(_config.GetConnectionString()));
            }
        }

        public IMVOrderService CreateMVOrderService()
        {
            return new MVCOrderService(CreateMenuService(), CreateShoppingBagService(), new OrderRepository(_config.GetConnectionString()));
        }

        public IMVPaymentService CreateMVPaymentService()
        {
            return new MVCPaymentService(new PaymentRepository(_config.GetConnectionString()));
        }
    }
}