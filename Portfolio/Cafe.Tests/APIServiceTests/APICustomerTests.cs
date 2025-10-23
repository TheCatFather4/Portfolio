using Cafe.BLL.Services.API;
using Cafe.Core.DTOs;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.APIServiceTests
{
    [TestFixture]
    public class APICustomerTests
    {
        [Test]
        public void Register_Async_Success()
        {
            var service = new APICustomerService(
                new MockCustomerRepository(),
                new MockCustomerLogger());

            var dto = new RegisterRequest();
            var id = "1234-abcd-5678-efgh";


            var result = service.Register(dto, id);

            Assert.That(result.IsCompleted);
        }
    }
}