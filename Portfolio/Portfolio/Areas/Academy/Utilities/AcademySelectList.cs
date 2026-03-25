using Academy.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Portfolio.Areas.Academy.Utilities
{
    public interface IAcademySelectList
    {
        public Task<SelectList?> BuildPowersAsync(ITempDataDictionary tempData);
        public Task<SelectList?> BuildWeaknessesAsync(ITempDataDictionary tempData);
    }

    public class AcademySelectList : IAcademySelectList
    {
        private readonly IAdmissionsService _admissionsService;

        public AcademySelectList(IAdmissionsService admissionsService)
        {
            _admissionsService = admissionsService;
        }

        public async Task<SelectList?> BuildPowersAsync(ITempDataDictionary tempData)
        {
            var powersResult = await _admissionsService.GetPowersAsync();

            if (powersResult.Ok)
            {
                return new SelectList(powersResult.Data, "PowerID", "PowerName");
            }

            return null;
        }

        public async Task<SelectList?> BuildWeaknessesAsync(ITempDataDictionary tempData)
        {
            var weaknessesResult = await _admissionsService.GetWeaknessesAsync();

            if (weaknessesResult.Ok)
            {
                return new SelectList(weaknessesResult.Data, "WeaknessID", "WeaknessName");
            }

            return null;
        }
    }
}