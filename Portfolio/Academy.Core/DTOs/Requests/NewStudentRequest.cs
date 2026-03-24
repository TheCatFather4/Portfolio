namespace Academy.Core.DTOs.Requests
{
    public class NewStudentRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Alias { get; set; }
        public DateTime DoB { get; set; }
        public int PowerID { get; set; }
        public int WeaknessID { get; set; }
        public string? IdentityId { get; set; }
        public string? Email { get; set; }
    }
}