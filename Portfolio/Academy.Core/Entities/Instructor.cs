namespace Academy.Core.Entities
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public string Alias { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TermDate { get; set; }
    }
}