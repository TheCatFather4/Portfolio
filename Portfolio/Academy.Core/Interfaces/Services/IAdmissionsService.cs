using Academy.Core.DTOs;
using Academy.Core.DTOs.Requests;
using Academy.Core.Entities;

namespace Academy.Core.Interfaces.Services
{
    public interface IAdmissionsService
    {
        Task<Result<List<Power>>> GetPowersAsync();
        Task<Result<List<Weakness>>> GetWeaknessesAsync();
        Task<AcademyResult> RegisterStudentAsync(NewStudentRequest dto);
    }
}