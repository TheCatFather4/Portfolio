using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cafe()
        {
            return View();
        }

        public IActionResult CafeAPI()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Documentation()
        {
            return View();
        }
    }
}