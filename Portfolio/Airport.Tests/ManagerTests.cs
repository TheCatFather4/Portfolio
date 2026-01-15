using NUnit.Framework;
using Portfolio.Models.Airport;

namespace Airport.Tests
{
    [TestFixture]
    public class ManagerTests
    {
        public LockerManager GetLockerManager()
        {
            return new LockerManager(new MockLockerRepository());
        }

        [Test]
        public void GetAllLockers_Success()
        {
            var mgr = GetLockerManager();
            var result = mgr.GetAllLockers();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data?.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetLocker_Success()
        {
            var mgr = GetLockerManager();
            var result = mgr.GetLocker(1);

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data?.RenterName, Is.EqualTo("Test"));
        }

        [Test]
        public void GetLocker_Fail()
        {
            var mgr = GetLockerManager();
            var result = mgr.GetLocker(2);

            Assert.That(result.Ok, Is.False);
        }

        [Test]
        public void IsRented_Yes()
        {
            var mgr = GetLockerManager();
            var tf = mgr.IsRented(1);

            Assert.That(tf, Is.True);
        }

        [Test]
        public void IsRented_No()
        {
            var mgr = GetLockerManager();
            var tf = mgr.IsRented(2);

            Assert.That(tf, Is.False);
        }
    }
}