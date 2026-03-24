using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Core.Entities
{
    public class Section
    {
        public int SectionID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // navigation property
        public Course? Course { get; set; }
    }
}