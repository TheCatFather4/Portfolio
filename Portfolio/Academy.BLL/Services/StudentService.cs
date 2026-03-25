using Academy.Core.DTOs;
using Academy.Core.Entities;
using Academy.Core.Interfaces.Repositories;
using Academy.Core.Interfaces.Services;

namespace Academy.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Result<List<StudentSection>>> GetGradesAsync(int studentId)
        {
            try
            {
                var grades = await _studentRepository.GetGradesAsync(studentId);

                if (grades == null)
                {
                    return ResultFactory.Fail<List<StudentSection>>("Grades not found.");
                }

                return ResultFactory.Success(grades);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<StudentSection>>($"{ex}");
            }
        }

        public async Task<Result<Student?>> GetStudentProfileAsync(string email)
        {
            try
            {
                var student = await _studentRepository.GetStudentProfileAsync(email);

                if (student == null)
                {
                    return ResultFactory.Fail<Student?>("No student found.");
                }

                return ResultFactory.Success<Student?>(student);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Student?>($"{ex}");
            }
        }
    }
}