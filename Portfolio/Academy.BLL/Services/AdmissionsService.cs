using Academy.Core.DTOs;
using Academy.Core.DTOs.Requests;
using Academy.Core.Entities;
using Academy.Core.Interfaces.Repositories;
using Academy.Core.Interfaces.Services;

namespace Academy.BLL.Services
{
    public class AdmissionsService : IAdmissionsService
    {
        private readonly IAdmissionsRepository _admissionsRepository;

        public AdmissionsService(IAdmissionsRepository admissionsRepository)
        {
            _admissionsRepository = admissionsRepository;
        }

        public async Task<Result<List<Power>>> GetPowersAsync()
        {
            try
            {
                var powers = await _admissionsRepository.GetPowersAsync();
                return ResultFactory.Success(powers);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Power>>($"{ex.Message}");
            }
        }

        public async Task<Result<List<Weakness>>> GetWeaknessesAsync()
        {
            try
            {
                var weaknesses = await _admissionsRepository.GetWeaknessesAsync();
                return ResultFactory.Success(weaknesses);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Weakness>>($"{ex.Message}");
            }
        }

        public async Task<AcademyResult> RegisterStudentAsync(NewStudentRequest dto)
        {
            try
            {
                var student = new Student()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Alias = dto.Alias,
                    DoB = dto.DoB,
                    Id = dto.IdentityId,
                    Email = dto.Email
                };

                var studentId = await _admissionsRepository.AddStudentAsync(student);

                if (studentId == 0)
                {
                    return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
                }

                Random rng = new Random();
                byte rating = (byte)rng.Next(1, 101);

                var studentPower = new StudentPower
                {
                    StudentID = studentId,
                    PowerID = dto.PowerID,
                    Rating = rating
                };

                var studentPowerID = await _admissionsRepository.AddStudentPowerAsync(studentPower);

                if (studentPowerID == 0)
                {
                    return ResultFactory.Fail("An error occurred. Please try again in a few minutes.");
                }

                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail($"{ex.Message}");
            }
        }
    }
}