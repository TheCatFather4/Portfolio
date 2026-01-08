namespace Portfolio.Models.Airport
{
    public class AddLockerForm
    {
        public int LockerNumber { get; set; }
        public string? RenterName { get; set; }
        public string? Contents { get; set; }
        public string? Message { get; set; }
        public string? SuccessMessage { get; set; }
    }
}