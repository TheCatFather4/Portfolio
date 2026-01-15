using Airport.Core.DTOs;

namespace Airport.Core.Interfaces
{
    public interface ILockerRepository
    {
        string? EndLockerRental(int lockerNumber);
        Locker[] GetAllLockers();
        Locker GetLocker(int lockerNumber);
        bool IsRented(int lockerNumber);
        void RentLocker(Locker locker);
    }
}