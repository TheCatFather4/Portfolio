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

        /// <summary>
        /// Constructs a service that has a logger and the ability to invoke repository methods concerning customers.
        /// </summary>
        /// <param name="logger">An implementation of the ILogger interface.</param>
        /// <param name="customerRepository">An implementation of the ICustomerRepository interface.</param>
        /// <param name="shoppingBagRepository">An implementation of the IShoppingBagRepository interface.</param>
        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository, IShoppingBagRepository shoppingBagRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _shoppingBagRepository = shoppingBagRepository;
        }

        /// <summary>
        /// Instantiates a new Customer and a new ShoppingBag. Both objects are sent to the appropriate repositories.
        /// The new Customer object will have a new CustomerID if added to the database using the ORM DatabaseMode.
        /// If using the Dapper variant, the CustomerID will need to be manually updated. An additional conditional
        /// check is provided for this below. See Cafe.BLL.ServiceFactory and appsettings.json for more details.
        /// </summary>
        /// <param name="dto">Used in mapping data to a new Customer object.</param>
        /// <returns>A Result DTO.</returns>
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
        /// Sends an email address to the repository to look up a current Customer.
        /// </summary>
        /// <param name="email">A string in the form of an email address.</param>
        /// <returns>A Result DTO with a Customer entity as its data.</returns>
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
        /// Sends an email address to the repository. If the repository doesn't return a matching string,
        /// the email is not a duplicate. This method is used in the workflow of registering a new Customer to the database.
        /// </summary>
        /// <param name="email">A string in the form of an email address.</param>
        /// <returns>A Result DTO.</returns>
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
        /// Sends a current Customer entity to the repository to be updated.
        /// </summary>
        /// <param name="customer">The current Customer to be updated.</param>
        /// <returns>A Result DTO.</returns>
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