using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Areas.Cafe.Controllers
{
    [Area("Cafe")]
    public class APIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}