using Airport.Core.DTOs;

namespace Airport.Core.Repositories
{
    public class LockerRepository
    {
        private readonly Locker[] _lockers;

        public LockerRepository()
        {
            var lockers = new Locker[100];

            for (int i = 0; i < lockers.Length; i++)
            {
                lockers[i] = new Locker();
            }

            _lockers = lockers;
            _lockers[4].LockerNumber = 5;
            _lockers[4].RenterName = "Boot";
            _lockers[4].Contents = "Salmon";
            _lockers[99].LockerNumber = 100;
            _lockers[99].RenterName = "Nut";
            _lockers[99].Contents = "Treats";
        }

        public string? EndLockerRental(int lockerNumber)
        {
            string? item = _lockers[lockerNumber - 1].Contents;

            _lockers[lockerNumber - 1].RenterName = null;
            _lockers[lockerNumber - 1].Contents = null;

            return item;
        }

        public Locker[] GetAllLockers()
        {
            return _lockers;
        }

        public Locker GetLocker(int lockerNumber)
        {
            return _lockers[lockerNumber - 1];
        }

        public bool IsRented(int lockerNumber)
        {
            if (_lockers[lockerNumber - 1].RenterName != null)
            {
                return true;
            }

            return false;
        }

        public void RentLocker(Locker locker)
        {
            _lockers[locker.LockerNumber - 1].LockerNumber = locker.LockerNumber;
            _lockers[locker.LockerNumber - 1].RenterName = locker.RenterName;
            _lockers[locker.LockerNumber - 1].Contents = locker.Contents;
        }
    }
}