using Academy.Core.Entities;

namespace Academy.Core.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentProfileAsync(string email);
        Task<List<StudentSection>> GetGradesAsync(int studentId);
    }
}