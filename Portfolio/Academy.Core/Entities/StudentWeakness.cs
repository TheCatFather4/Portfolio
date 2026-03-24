namespace Academy.Core.Entities
{
    public class StudentWeakness
    {
        public int StudentWeaknessID { get; set; }
        public int StudentID { get; set; }
        public int WeaknessID { get; set; }
        public byte RiskLevel { get; set; }
    }
}