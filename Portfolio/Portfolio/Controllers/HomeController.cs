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
        /// Takes the user to the Battleship main page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Battleship()
        {
            return View();
        }

        /// <summary>
        /// Takes the user to the 4th Wall Café main page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Cafe()
        {
            return View();
        }

        /// <summary>
        /// Takes the user to the Café API web page.
        /// </summary>
        /// <returns></returns>
        public IActionResult CafeAPI()
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