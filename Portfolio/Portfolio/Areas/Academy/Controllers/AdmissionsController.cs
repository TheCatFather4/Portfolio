using Academy.Core.DTOs.Requests;
using Academy.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.Academy.Models.Admissions;
using Portfolio.Areas.Academy.Utilities;
using Portfolio.Utilities;

namespace Portfolio.Areas.Academy.Controllers
{
    /// <summary>
    /// Handles requests concerning student admissions.
    /// An admissions role is required to utilize these controller methods.
    /// </summary>
    [Area("Academy")]
    [Authorize(Roles = "Admissions")]
    public class AdmissionsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdmissionsService _admissionsService;
        private readonly IAcademySelectList _selectListBuilder;

        /// <summary>
        /// Constructs a controller with the required dependencies to register new students.
        /// </summary>
        public AdmissionsController(UserManager<IdentityUser> userManager, IAdmissionsService admissionsService, IAcademySelectList selectListBuilder)
        {
            _userManager = userManager;
            _admissionsService = admissionsService;
            _selectListBuilder = selectListBuilder;
        }

        /// <summary>
        /// Takes the user to the main admissions page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Takes the user to a web page where they can register a new student.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> NewStudent()
        {
            var model = new NewStudentForm();

            model.Powers = await _selectListBuilder.BuildPowersAsync(TempData);
            model.Weaknesses = await _selectListBuilder.BuildWeaknessesAsync(TempData);

            if (model.Powers == null || model.Weaknesses == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        /// <summary>
        /// Attempts to register a new student. If successful, the student's credentials are returned.
        /// </summary>
        /// <param name="model">Used to register a student.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewStudent(NewStudentForm model)
        {
            if (ModelState.IsValid)
            {
                string email = $"{model.FirstName}.{model.LastName}@fwacademy.com";

                var user = new IdentityUser
                {
                    UserName = email,
                    Email = email
                };

                string password = PasswordGenerator.GeneratePassword();

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var request = new NewStudentRequest
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Alias = model.Alias,
                        DoB = model.DoB,
                        PowerID = model.SelectedPowerID,
                        WeaknessID = model.SelectedWeaknessID,
                        IdentityId = user.Id,
                        Email = email
                    };

                    var registerResult = await _admissionsService.RegisterStudentAsync(request);

                    if (registerResult.Ok)
                    {
                        await _userManager.AddToRoleAsync(user, "Student");
                        TempData["Alert"] = Alert.CreateSuccess($"New student successfully registered! UserName: {user.UserName} Password: {password}");
                        return RedirectToAction("Index", "Home");
                    }

                    TempData["Alert"] = Alert.CreateError(registerResult.Message);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                TempData["Alert"] = Alert.CreateError($"{result.Errors.First().Description}");
            }

            model.Powers = await _selectListBuilder.BuildPowersAsync(TempData);
            model.Weaknesses = await _selectListBuilder.BuildWeaknessesAsync(TempData);
            return View(model);
        }
    }
}