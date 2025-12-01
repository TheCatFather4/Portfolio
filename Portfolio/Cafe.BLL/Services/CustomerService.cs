using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles business logic concerning customer entities
    /// </summary>
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

        /// <summary>
        /// Handles the logic for adding a new customer record. If using the Dapper database mode, 
        /// a conditional check is present, as Dapper does not automatically update the CustomerID.
        /// </summary>
        /// <param name="dto">An object to map to a new customer object</param>
        /// <returns>A result dto</returns>
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

        /// <summary>
        /// Handles the logic for looking up a customer by email address
        /// </summary>
        /// <param name="email">A string in the form of an email address</param>
        /// <returns>A result dto with a customer entity as its data</returns>
        public async Task<Result<Customer>> GetCustomerByEmailAsync(string email)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(email);

                if (customer == null)
                {
                    _logger.LogError($"Customer not found with email: {email}");
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

        /// <summary>
        /// Checks to see if a particular email address is already present in the database
        /// </summary>
        /// <param name="email">A string in the form of an email address</param>
        /// <returns>A result dto</returns>
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

        /// <summary>
        /// Handles the logic for updating a Customer record
        /// </summary>
        /// <param name="customer">Customer Entity</param>
        /// <returns>A result dto</returns>
        public async Task<Result> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                await _customerRepository.UpdateCustomerAsync(customer);
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