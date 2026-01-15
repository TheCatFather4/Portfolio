using Airport.Core.DTOs;
using Airport.Core.Interfaces;

namespace Portfolio.Models.Airport
{
    public class LockerManager
    {
        private readonly ILockerRepository _lockerRepository;

        public LockerManager(ILockerRepository lockerRepository)
        {
            _lockerRepository = lockerRepository;
        }

        public Result EndLockerRental(int lockerNumber)
        {
            try
            {
                var item = _lockerRepository.EndLockerRental(lockerNumber);
                return ResultFactory.Success($"Locker {lockerNumber} rental successfully ended. Please take your {item}.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Result>($"An error occurred. Please contact the site administrator: {ex.Message}");
            }
        }

        public Result<List<Locker>> GetAllLockers()
        {
            try
            {
                var lockers = _lockerRepository.GetAllLockers();

                if (lockers == null)
                {
                    return ResultFactory.Fail<List<Locker>>("An error occurred. Please try again in a few minutes.");
                }

                var lockerList = new List<Locker>();

                for (int i = 0; i < lockers.Length; i++)
                {
                    if (lockers[i].RenterName != null)
                    {
                        lockerList.Add(lockers[i]);
                    }
                }

                return ResultFactory.Success(lockerList);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Locker>>($"An error occurred. Please contact the site administrator: {ex.Message}");
            }
        }

        public Result<Locker> GetLocker(int lockerNumber)
        {
            try
            {
                var locker = _lockerRepository.GetLocker(lockerNumber);

                if (locker.RenterName == null)
                {
                    return ResultFactory.Fail<Locker>("This locker is not rented.");
                }

                return ResultFactory.Success(locker);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Locker>($"An error occurred. Please contact the site administrator: {ex.Message}");
            }
        }

        public bool IsRented(int lockerNumber)
        {
            var result = _lockerRepository.IsRented(lockerNumber);

            if (result)
            {
                return true;
            }

            return false;
        }

        public Result RentLocker(Locker locker)
        {
            try
            {
                _lockerRepository.RentLocker(locker);
                return ResultFactory.Success($"Locker {locker.LockerNumber} successfully rented for {locker.Contents} storage.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Result>($"An error occurred. Please contact the site administrator: {ex.Message}");
            }
        }
    }
}