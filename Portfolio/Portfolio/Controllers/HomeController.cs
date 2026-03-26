using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    /// <summary>
    /// The controller for my portfolio website.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Takes the user to my portfolio homepage.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns a webpage to users that are not authorized to access an area.
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Takes the user to the contact page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Takes the user to the docs and diagrams page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Documentation()
        {
            return View();
        }
    }
}