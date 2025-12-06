using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic for Customer related tasks.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingBagRepository _shoppingBagRepository;

        /// <summary>
        /// Constructs a service with the dependencies required for Customer related tasks.
        /// </summary>
        /// <param name="logger">An dependency used for logging error messages.</param>
        /// <param name="customerRepository">An dependency used for invoking data methods concerning Customer records.</param>
        /// <param name="shoppingBagRepository">An dependency used for creating new ShoppingBag records.</param>
        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository, IShoppingBagRepository shoppingBagRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _shoppingBagRepository = shoppingBagRepository;
        }

        /// <summary>
        /// An ICustomerRepository method is invoked to create a new Customer record. If successful, a ShoppingBag is created for the
        /// Customer. The new record is then updated with the new ShoppingBagID.
        /// </summary>
        /// <param name="dto">The data required to create a new Customer record. The data is mapped to a new Customer instance.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
        public async Task<Result> AddCustomerAsync(AddCustomerRequest dto)
        {
            // If using Entity Framework Core:
            // The CustomerID property will be automatically assigned to this object instance.
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

            // If using Dapper:
            // The CustomerID property must be manually assigned to the object instance.
            if (customer.CustomerID == 0)
            {
                customer.CustomerID = customerId;
            }

            var shoppingBag = new ShoppingBag
            {
                CustomerID = customerId
            };

            // Create a new ShoppingBag for the Customer.
            var shoppingBagId = await _shoppingBagRepository.CreateShoppingBagAsync(shoppingBag);

            if (shoppingBagId == 0)
            {
                _logger.LogError($"Shopping Bag Id returned with a value of: {shoppingBagId}");
                return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
            }

            // Update Customer record.
            customer.ShoppingBagID = shoppingBagId;
            await _customerRepository.UpdateCustomerAsync(customer);
            return ResultFactory.Success("New customer successfully registered!");
        }

        /// <summary>
        /// An ICustomerRepository method is invoked to determine whether a given Customer exists or not.
        /// </summary>
        /// <param name="email">A string in the form of an email address.</param>
        /// <returns>A Result DTO with a Customer entity as its data. If a Customer was not found, the Result data is null.</returns>
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
                _logger.LogError($"An error occurred when attempting to retrieve Customer data for {email}: {ex.Message}");
                return ResultFactory.Fail<Customer>("An error occurred. Please contact our management team for assistance.");
            }
        }

        /// <summary>
        /// An ICustomerRepository method is invoked to determine whether a given email address is a duplicate or not.
        /// </summary>
        /// <param name="email">A string in the form of an email address.</param>
        /// <returns>A Result DTO. A message is returned if the email address already exists.</returns>
        public async Task<Result> GetDuplicateEmailAsync(string email)
        {
            try
            {
                var duplicateEmail = await _customerRepository.GetEmailAddressAsync(email);

                if (duplicateEmail == null)
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
        /// An ICustomerRepository method is invoked to update a Customer record.
        /// </summary>
        /// <param name="customer">The Customer data to be send to the database.</param>
        /// <returns>A Result DTO with a confirmation message.</returns>
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