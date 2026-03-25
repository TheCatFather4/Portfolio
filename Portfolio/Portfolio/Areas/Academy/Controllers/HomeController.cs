using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Areas.Academy.Controllers
{
    [Area("Academy")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}