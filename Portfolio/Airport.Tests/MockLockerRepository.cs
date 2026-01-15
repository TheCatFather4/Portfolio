using Airport.Core.DTOs;
using Airport.Core.Interfaces;

namespace Airport.Tests
{
    public class MockLockerRepository : ILockerRepository
    {
        public string? EndLockerRental(int lockerNumber)
        {
            throw new NotImplementedException();
        }

        public Locker[] GetAllLockers()
        {
            var lockers = new Locker[2];

            lockers[0] = new Locker
            {
                RenterName = "A",
                Contents = "Food"
            };

            lockers[1] = new Locker();

            return lockers;
        }

        public Locker GetLocker(int lockerNumber)
        {
            if (lockerNumber == 1)
            {
                return new Locker
                {
                    LockerNumber = lockerNumber,
                    RenterName = "Test",
                    Contents = "Food"
                };
            }

            return new Locker();
        }

        public bool IsRented(int lockerNumber)
        {
            if (lockerNumber == 1)
            {
                return true;
            }

            return false;
        }

        public void RentLocker(Locker locker)
        {
            throw new NotImplementedException();
        }
    }
}