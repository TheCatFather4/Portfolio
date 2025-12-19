using Cafe.BLL.Services;
using Cafe.Core.DTOs;
using Cafe.Core.Interfaces.Services;
using Cafe.Tests.MockLoggers;
using Cafe.Tests.MockRepositories;
using NUnit.Framework;

namespace Cafe.Tests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        public IPaymentService GetPaymentService()
        {
            var service = new PaymentService(
                new MockPaymentLogger(),
                new MockOrderRepository(),
                new MockPaymentRepository());

            return service;
        }

        [Test]
        public void GetFinalTotalAsync_Success()
        {
            var service = GetPaymentService();

            var result = service.GetFinalTotalAsync(1);

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data, Is.EqualTo(20.00M));
        }

        [Test]
        public void GetFinalTotalAsync_Zero()
        {
            var service = GetPaymentService();

            var result = service.GetFinalTotalAsync(3);

            Assert.That(result.Result.Ok, Is.False);
            Assert.That(result.Result.Data, Is.EqualTo(0));
        }

        [Test]
        public void GetPaymentTypesAsync_Success()
        {
            var service = GetPaymentService();

            var result = service.GetPaymentTypesAsync();

            Assert.That(result.Result.Ok, Is.True);
            Assert.That(result.Result.Data?.Count(), Is.EqualTo(3));
        }

        [Test]
        public void ProcessPaymentAsync_NotFound()
        {
            var service = GetPaymentService();

            var dto = new PaymentRequest
            {
                OrderID = 2,
                PaymentTypeID = 1,
                Amount = 10.90M
            };

            var result = service.ProcessPaymentAsync(dto);

            Assert.That(result.Result.Ok, Is.False);
        }

        [Test]
        public void ProcessPaymentAsync_Success()
        {
            var service = GetPaymentService();

            var dto = new PaymentRequest
            {
                OrderID = 1,
                PaymentTypeID = 1,
                Amount = 10.90M
            };

            var result = service.ProcessPaymentAsync(dto);

            Assert.That(result.Result.Ok, Is.True);
        }

        [Test]
        public void ProcessPaymentAsync_WrongAmountDue()
        {
            var service = GetPaymentService();

            var dto = new PaymentRequest
            {
                OrderID = 1,
                PaymentTypeID = 1,
                Amount = 2.00M
            };

            var result = service.ProcessPaymentAsync(dto);

            Assert.That(result.Result.Ok, Is.False);
        }
    }
}