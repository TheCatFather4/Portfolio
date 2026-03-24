using Academy.Core.Entities;

namespace Academy.Core.Interfaces.Repositories
{
    public interface IAdmissionsRepository
    {
        Task<int> AddStudentPowerAsync(StudentPower studentPower);
        Task<int> AddStudentAsync(Student student);
        Task<List<Power>> GetPowersAsync();
        Task<List<Weakness>> GetWeaknessesAsync();
    }
}