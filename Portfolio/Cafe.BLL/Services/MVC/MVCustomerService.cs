using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services.MVC;

namespace Cafe.BLL.Services.MVC
{
    public class MVCustomerService : IMVCustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public MVCustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> RegisterCustomerAsync(string email, string identityId)
        {

            if (string.IsNullOrEmpty(identityId))
            {
                return ResultFactory.Fail("Unable to register as a customer.");
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
                await _customerRepository.AddCustomerAsync(customer);

                var shoppingBag = new ShoppingBag
                {
                    CustomerID = customer.CustomerID
                };

                await _customerRepository.CreateShoppingBagAsync(shoppingBag);

                customer.ShoppingBagID = shoppingBag.CustomerID;

                await _customerRepository.UpdateCustomerAsync(customer);

                return ResultFactory.Success("New customer successfully created!");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail($"An unexpected error ocurred: {ex.Message}");
            }
        }
    }
}
