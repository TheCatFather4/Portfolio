using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services.API
{
    public class APICustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _logger;

        public APICustomerService(ICustomerRepository customerRepository, ILogger logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Result> Register(RegisterRequest dto, string identityId)
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

            try
            {
                var customerId = await _customerRepository.AddCustomerAsync(customer);

                if (customerId == 0)
                {
                    _logger.LogError($"Customer id returned with a value of: {customerId} ");
                    return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
                }

                var shoppingBag = new ShoppingBag
                {
                    CustomerID = customerId
                };

                var bagId = await _customerRepository.CreateShoppingBagAsync(shoppingBag);

                if (bagId == 0)
                {
                    _logger.LogError($"Shopping bag id returned with a value of: {bagId}");
                    return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
                }

                customer.ShoppingBagID = bagId;

                // If using Dapper database mode
                if (customer.CustomerID == 0)
                {
                    customer.CustomerID = customerId;
                }

                await _customerRepository.UpdateCustomerAsync(customer);

                return ResultFactory.Success("User registered successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred when registering a new user: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our customer assistance team.");
            }
        }
    }
}
