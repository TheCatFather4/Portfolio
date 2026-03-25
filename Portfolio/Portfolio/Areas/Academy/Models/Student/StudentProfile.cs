namespace Portfolio.Areas.Academy.Models.Student
{
    public class StudentProfile
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public DateTime DoB { get; set; }
    }
}