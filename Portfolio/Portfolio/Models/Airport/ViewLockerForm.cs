using Airport.Core.DTOs;

namespace Portfolio.Models.Airport
{
    public class ViewLockerForm
    {
        public string? LockerNumber { get; set; }
        public string? Message { get; set; }
        public Locker? Locker { get; set; }
    }
}