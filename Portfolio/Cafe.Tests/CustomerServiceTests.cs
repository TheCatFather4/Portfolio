using Cafe.BLL.Services;
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
        public void GetCustomerByEmailAsync_Success()
        {
            var service = GetCustomerService();

            var result = service.GetCustomerByEmailAsync("Customer1@fwcafe.com");

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void GetDuplicateEmailAsync_Success()
        {
            var service = GetCustomerService();

            var result = service.GetDuplicateEmailAsync("Customer2@fwcafe.com");

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void UpdateCustomerAsync_Success()
        {
            var service = GetCustomerService();

            var result = service.UpdateCustomerAsync(new Core.Entities.Customer());

            Assert.That(result.Result.Ok, Is.True);

        }
    }
}