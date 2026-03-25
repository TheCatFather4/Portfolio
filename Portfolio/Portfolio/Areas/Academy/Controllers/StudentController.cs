using Academy.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.Academy.Models.Student;
using Portfolio.Utilities;

namespace Portfolio.Areas.Academy.Controllers
{
    [Area("Academy")]
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _studentService.GetStudentProfileAsync(User.Identity.Name);

            if (result.Ok)
            {
                var model = new StudentProfile
                {
                    StudentID = result.Data.StudentID,
                    FirstName = result.Data.FirstName,
                    LastName = result.Data.LastName,
                    Alias = result.Data.Alias,
                    DoB = result.Data.DoB
                };

                return View(model);
            }

            TempData["Alert"] = Alert.CreateError(result.Message);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Grades(int studentId)
        {
            var result = await _studentService.GetGradesAsync(studentId);

            if (result.Ok)
            {
                var model = new List<StudentGrade>();

                foreach (var ss in result.Data)
                {
                    string grade;
                    byte absences;

                    if (ss.Grade == null)
                    {
                        grade = "Not Yet Graded";
                        absences = 0;
                    }
                    else
                    {
                        grade = ss.Grade.ToString();
                        absences = (byte)ss.Absences;
                    }


                    var sg = new StudentGrade
                    {
                        SectionID = ss.SectionID,
                        Grade = grade,
                        Absences = absences
                    };

                    model.Add(sg);
                }

                return View(model);
            }

            TempData["Alert"] = Alert.CreateError(result.Message);
            return RedirectToAction("Index", "Student");
        }
    }
}