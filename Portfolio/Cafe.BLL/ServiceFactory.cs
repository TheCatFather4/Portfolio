using Cafe.BLL.Services;
using Cafe.BLL.Services.API;
using Cafe.BLL.Services.MVC;
using Cafe.Core.Enums;
using Cafe.Core.Interfaces.Application;
using Cafe.Core.Interfaces.Services;
using Cafe.Core.Interfaces.Services.MVC;
using Cafe.Data.Repositories.Dapper;
using Cafe.Data.Repositories.EF;
using Cafe.Data.Repositories.TrainingRepository;
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
            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new ManagementService(
                    new EFManagementRepository(_config.GetConnectionString()));
            }
            else
            {
                return new ManagementService(new TrainingManagementRepository());
            }
        }

        public IAccountantService CreateAccountantService()
        {
            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new AccountantService(
                    new EFAccountantRepository(_config.GetConnectionString()));
            }
            else
            {
                //add mock repo
                return null;
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
            var logger = _loggerFactory.CreateLogger<OrderService>();

            return new OrderService(
                new OrderRepository(_config.GetConnectionString()), 
                CreateShoppingBagService(), 
                CreateMenuService(), 
                logger);
        }

        public ICustomerService CreateCustomerService()
        {
            var logger = _loggerFactory.CreateLogger<CustomerService>();

            return new CustomerService(
                new CustomerRepository(_config.GetConnectionString()), 
                logger);
        }

        public IJwtService CreateJwtService()
        {
            return new JwtService(_jwtConfig);
        }

        public IPaymentService CreatePaymentService()
        {
            var logger = _loggerFactory.CreateLogger<PaymentService>();

            return new PaymentService(
                new PaymentRepository(_config.GetConnectionString()), 
                logger);
        }

        public IMVCustomerService CreateMVCustomerService()
        {
            return new MVCustomerService(new CustomerRepository(_config.GetConnectionString()));
        }

        public IMVOrderService CreateMVOrderService()
        {
            return new MVOrderService(CreateMenuService(), CreateShoppingBagService(), new OrderRepository(_config.GetConnectionString()));
        }

        public IMVPaymentService CreateMVPaymentService()
        {
            return new MVPaymentService(new PaymentRepository(_config.GetConnectionString()));
        }
    }
}
