using Cafe.BLL.Services.API;
using Cafe.Core.DTOs;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.APIServiceTests
{
    [TestFixture]
    public class APIPaymentTests
    {
        [Test]
        public void ProcessPayment_Success()
        {
            var service = new APIPaymentService(
                new MockPaymentRepository(),
                new MockPaymentLogger());

            var dto = new PaymentRequest();

            var result = service.ProcessPayment(dto);

            Assert.That(result.Ok, Is.True);
        }
    }
}