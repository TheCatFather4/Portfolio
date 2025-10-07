using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services.MVC
{
    public class MVCCustomerService : IMVCCustomerService
    {
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customerRepository;

        public MVCCustomerService(ILogger<MVCCustomerService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<Result<Customer>> GetCustomerByEmailAsync(string identityId)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(identityId);

                if (customer == null)
                {
                    _logger.LogError($"Customer not found with email: {identityId}");
                    return ResultFactory.Fail<Customer>("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<Customer>("An error occurred. Please contact our management team for assistance.");
            }
        }

        public async Task<Result> GetDuplicateEmailAsync(string email)
        {
            try
            {
                var currentCustomer = await _customerRepository.GetCustomerByEmailAsync(email);

                if (currentCustomer == null)
                {
                    return ResultFactory.Success();
                }

                return ResultFactory.Fail($"Customer with email: {email} already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when checking for a duplicate email: {ex.Message}");
                return ResultFactory.Fail<Customer>("An error occurred. Please contact our management team for assistance.");
            }
        }

        public async Task<Result> RegisterCustomerAsync(string email, string identityId)
        {
            if (string.IsNullOrEmpty(identityId))
            {
                _logger.LogError("Customer registration failed.");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            var customer = new Customer
            {
                FirstName = "New",
                LastName = "Customer",
                Email = email,
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

                return ResultFactory.Success("New customer successfully created!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error ocurred when registering a customer: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result> UpdateCustomerAsync(Customer entity)
        {
            try
            {
                await _customerRepository.UpdateCustomerAsync(entity);
                return ResultFactory.Success("Your customer information is successfully updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to update customer profile: {ex.Message}");
                return ResultFactory.Fail("An error occurred. Please contact our management team.");
            }
        }
    }
}
