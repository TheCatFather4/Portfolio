using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Core.Entities
{
    public class StudentSection
    {
        public int StudentSectionID { get; set; }

        [ForeignKey("Section")]
        public int SectionID { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

        public byte? Grade { get; set; }
        public byte? Absences { get; set; }

        // navigation property
        public Section? Section { get; set; }
    }
}