using Cafe.BLL.Services.MVC;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.MVCServiceTests
{
    [TestFixture]
    public class MVCCustomerTests
    {
        [Test]
        public void GetCustomerByEmail_Success()
        {
            var service = new MVCCustomerService(
                new MockCustomerLogger(),
                new MockCustomerRepository());

            var result = service.GetCustomerByEmailAsync("customer1@fwcafe.com");

            Assert.That(result.IsCompleted);
        }

        [Test]
        public void RegisterCustomer_Success()
        {
            var service = new MVCCustomerService(
                new MockCustomerLogger(),
                new MockCustomerRepository());

            var result = service.RegisterCustomerAsync("test@tester.com", "1234-abcd-5678-efgh");

            Assert.That(result.IsCompleted);
        }
    }
}