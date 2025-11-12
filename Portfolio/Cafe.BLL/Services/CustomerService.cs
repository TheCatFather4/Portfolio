using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingBagRepository _shoppingBagRepository;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository, IShoppingBagRepository shoppingBagRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _shoppingBagRepository = shoppingBagRepository;
        }

        public async Task<Result> AddCustomerAsync(AddCustomerRequest dto)
        {
            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Id = dto.IdentityId
            };

            var customerId = await _customerRepository.AddCustomerAsync(customer);

            if (customerId == 0)
            {
                _logger.LogError($"Customer Id returned with a value of: {customerId} ");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            // If using Dapper database mode
            if (customer.CustomerID == 0)
            {
                customer.CustomerID = customerId;
            }

            var shoppingBag = new ShoppingBag
            {
                CustomerID = customerId
            };

            var shoppingBagId = await _shoppingBagRepository.CreateShoppingBagAsync(shoppingBag);

            if (shoppingBagId == 0)
            {
                _logger.LogError($"Shopping Bag Id returned with a value of: {shoppingBagId}");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            customer.ShoppingBagID = shoppingBagId;

            await _customerRepository.UpdateCustomerAsync(customer);
            return ResultFactory.Success("New customer successfully registered!");
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