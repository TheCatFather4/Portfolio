using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Areas.Cafe.Controllers
{
    [Area("Cafe")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult API()
        {
            return View();
        }
    }
}