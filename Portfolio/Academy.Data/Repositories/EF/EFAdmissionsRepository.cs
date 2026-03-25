using Academy.Core.Entities;
using Academy.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academy.Data.Repositories.EF
{
    public class EFAdmissionsRepository : IAdmissionsRepository
    {
        private readonly AcademyContext _dbContext;

        public EFAdmissionsRepository(string connectionString)
        {
            _dbContext = new AcademyContext(connectionString);
        }

        public async Task<int> AddStudentAsync(Student student)
        {
            _dbContext.Student.Add(student);
            await _dbContext.SaveChangesAsync();

            return student.StudentID;
        }

        public async Task<int> AddStudentPowerAsync(StudentPower studentPower)
        {
            _dbContext.StudentPower.Add(studentPower);
            await _dbContext.SaveChangesAsync();

            return studentPower.StudentID;
        }

        public async Task<List<Power>> GetPowersAsync()
        {
            return await _dbContext.Power
                .ToListAsync();
        }

        public async Task<List<Weakness>> GetWeaknessesAsync()
        {
            return await _dbContext.Weakness
                .ToListAsync();
        }
    }
}