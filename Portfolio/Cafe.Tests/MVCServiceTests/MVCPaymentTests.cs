using Cafe.BLL.Services.MVC;
using Cafe.Core.Entities;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests.MVCServiceTests
{
    [TestFixture]
    public class MVCPaymentTests
    {
        [Test]
        public void GetPaymentTypes_Success()
        {
            var service = new MVCPaymentService(
                new MockPaymentLogger(),
                new MockPaymentRepository());

            var result = service.GetPaymentTypes();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void ProcessPayment_Success()
        {
            var service = new MVCPaymentService(
                new MockPaymentLogger(),
                new MockPaymentRepository());

            var payment = new Payment
            {
                PaymentID = 1,
                PaymentStatusID = 1
            };

            var result = service.ProcessPayment(payment);

            Assert.That(result.Ok, Is.True);
        }
    }
}