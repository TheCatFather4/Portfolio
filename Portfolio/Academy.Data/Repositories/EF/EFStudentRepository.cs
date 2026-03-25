using Academy.Core.Entities;
using Academy.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academy.Data.Repositories.EF
{
    public class EFStudentRepository : IStudentRepository
    {
        private readonly AcademyContext _dbContext;

        public EFStudentRepository(string connectionString)
        {
            _dbContext = new AcademyContext(connectionString);
        }

        public async Task<List<StudentSection>> GetGradesAsync(int studentId)
        {
            return await _dbContext.StudentSection
                .Where(s => s.StudentID == studentId)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentProfileAsync(string email)
        {
            return await _dbContext.Student
                .FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}