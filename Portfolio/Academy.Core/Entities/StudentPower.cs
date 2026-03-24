namespace Academy.Core.Entities
{
    public class StudentPower
    {
        public int StudentPowerID { get; set; }
        public int StudentID { get; set; }
        public int PowerID { get; set; }
        public byte Rating { get; set; }
    }
}