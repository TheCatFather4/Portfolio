using Academy.Core.DTOs;
using Academy.Core.Entities;

namespace Academy.Core.Interfaces.Services
{
    public interface IStudentService
    {
        Task<Result<Student?>> GetStudentProfileAsync(string email);
        Task<Result<List<StudentSection>>> GetGradesAsync(int studentId);
    }
}