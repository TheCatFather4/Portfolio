using Cafe.BLL.Services;
using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        public ICustomerService GetCustomerService()
        {
            var service = new CustomerService(
                new MockCustomerLogger(),
                new MockCustomerRepository(),
                new MockShoppingBagRepository());

            return service;
        }

        [Test]
        public void AddCustomerAsync_Success()
        {
            var service = GetCustomerService();

            var request = new AddCustomerRequest
            {
                FirstName = "Test",
                LastName = "Tester",
                Email = "Test@Tester.com",
                Password = "Testing",
                IdentityId = "123abc"
            };

            var result = service.AddCustomerAsync(request);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void GetCustomerByEmailAsync_NotFound()
        {
            var service = GetCustomerService();

            var result = service.GetCustomerByEmailAsync("Yugi!");

            Assert.That(result.Result.Ok, Is.EqualTo(false));
            Assert.That(result.Result.Data, Is.Null);
        }

        [Test]
        public void GetCustomerByEmailAsync_Success()
        {
            var service = GetCustomerService();

            var result = service.GetCustomerByEmailAsync("Customer1@fwcafe.com");

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.Email, Is.EqualTo("Customer1@fwcafe.com"));
        }

        [Test]
        public void GetDuplicateEmailAsync_IsDuplicate()
        {
            var service = GetCustomerService();

            var result = service.GetDuplicateEmailAsync("Customer1@fwcafe.com");

            Assert.That(result.Result.Ok, Is.False);
            Assert.That(result.Result.Message, Is.EqualTo("Customer with email: Customer1@fwcafe.com already exists."));
        }

        [Test]
        public void GetDuplicateEmailAsync_IsUnique()
        {
            var service = GetCustomerService();

            var result = service.GetDuplicateEmailAsync("Customer2@fwcafe.com");

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Message, Is.EqualTo(string.Empty));
        }

        [Test]
        public void UpdateCustomerAsync_Success()
        {
            var service = GetCustomerService();

            var result = service.UpdateCustomerAsync(new Customer());

            Assert.That(result.Result.Ok, Is.True);
        }
    }
}