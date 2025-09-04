using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Cafe.BLL.Services.API
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Result> Register(RegisterUser dto, string identityId)
        {
            if (string.IsNullOrEmpty(identityId))
            {
                _logger.LogWarning("Identity key missing for new customer.");
                return ResultFactory.Fail("Unable to retrieve id for new user.");
            }

            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Id = identityId
            };

            await _customerRepository.AddCustomerAsync(customer);

            var shoppingBag = new ShoppingBag
            {
                CustomerID = customer.CustomerID
            };

            await _customerRepository.CreateShoppingBagAsync(shoppingBag);

            customer.ShoppingBagID = shoppingBag.ShoppingBagID;

            await _customerRepository.UpdateCustomerAsync(customer);

            return ResultFactory.Success("User registered successfully");
        }
    }
}
