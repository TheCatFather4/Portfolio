using Airport.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Airport;

namespace Portfolio.Controllers.Airport
{
    /// <summary>
    /// Handles HTTP requests concerning the web version of the Airport Locker Rental application.
    /// </summary>
    public class AirportController : Controller
    {
        private readonly LockerManager _lockerManager;

        /// <summary>
        /// Constructs a controller with the necessary member in order to access lockers.
        /// </summary>
        /// <param name="lockerManager"></param>
        public AirportController(LockerManager lockerManager)
        {
            _lockerManager = lockerManager;
        }

        /// <summary>
        /// Takes the user to the main menu page.
        /// </summary>
        /// <returns>A ViewResult object with the model state.</returns>
        public IActionResult Menu()
        {
            var model = new AirportMenu();
            return View(model);
        }

        /// <summary>
        /// Validates the user input and switches their choice to the appropriate controller.
        /// </summary>
        /// <param name="model">Used for menu input and display</param>
        /// <returns>A RedirectToActionResult object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Menu(AirportMenu model)
        {
            if (int.TryParse(model.MenuChoice, out int choice) && choice >= 1 && choice <= 5)
            {
                switch (choice)
                {
                    case 1:
                        return RedirectToAction("ViewLocker");
                    case 2:
                        return RedirectToAction("RentLocker");
                    case 3:
                        return RedirectToAction("EndRental");
                    case 4:
                        return RedirectToAction("AllLockers");
                    case 5:
                        return RedirectToAction("Exit");
                }
            }

            model.Message = "Invalid input. Please choose a number from 1 to 5.";
            return View(model);
        }

        /// <summary>
        /// Takes the user to a page where they are "prompted" to select a locker to view.
        /// (Menu Choice 1: Input)
        /// </summary>
        /// <returns>A ViewResult object with the model state.</returns>
        public IActionResult ViewLocker()
        {
            var model = new ViewLockerForm();
            return View(model);
        }

        /// <summary>
        /// Validates the user input and retrieves the appropriate locker object.
        /// (Menu Choice 1: Validation)
        /// </summary>
        /// <param name="model">Used to retrieve locker choice and display locker data.</param>
        /// <returns>A ViewResult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewLocker(ViewLockerForm model)
        {
            if (int.TryParse(model.LockerNumber, out int choice) && choice >= 1 && choice <= 100)
            {
                var result = _lockerManager.GetLocker(choice);

                if (result.Ok)
                {
                    model.Locker = result.Data;
                    return View(model);
                }

                model.Message = result.Message;
                return View(model);
            }

            model.Message = "Invalid input. Please choose a number from 1 to 100.";
            return View(model);
        }

        /// <summary>
        /// Takes the user to a web page where they can select a locker number for rental.
        /// (Menu Choice 2.1: Input)
        /// </summary>
        /// <returns>A ViewResult object with the model state.</returns>
        public IActionResult RentLocker()
        {
            var model = new RentLockerForm();
            return View(model);
        }

        /// <summary>
        /// Validates the user's input. If successful the user is redirected to a web page where they
        /// enter their renter details. (Display for Menu Choice 2.1: Validation)
        /// </summary>
        /// <param name="model">Used to retrieve locker number choice and display result message.</param>
        /// <returns>If selected locker is available, A RedirectToActionResult object is returned.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RentLocker(RentLockerForm model)
        {
            if (int.TryParse(model.LockerNumber, out int choice) && choice >= 1 && choice <= 100)
            {
                var isRented = _lockerManager.IsRented(choice);

                if (isRented)
                {
                    model.Message = $"Locker {choice} is already rented. Please try another locker number.";
                    return View(model);
                }

                return RedirectToAction("AddLocker", new { id = choice });
            }

            model.Message = "Invalid input. Please choose a number from 1 to 100.";
            return View(model);
        }

        /// <summary>
        /// Takes the user to a web page where they can supply their renter details. (Menu Choice 2.2: Input)
        /// </summary>
        /// <param name="id">The selected locker number.</param>
        /// <returns>A ViewResult object with the model state.</returns>
        public IActionResult AddLocker(int id)
        {
            var model = new AddLockerForm();
            model.LockerNumber = id;
            return View(model);
        }

        /// <summary>
        /// Rents a locker for a user. (Menu Choice 2.2: Validation)
        /// </summary>
        /// <param name="model">The renter's data.</param>
        /// <returns>A Viewresult object with the model state.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLocker(AddLockerForm model)
        {
            if (string.IsNullOrWhiteSpace(model.RenterName))
            {
                model.Message = "You must provide a name.";
                return View(model);
            }
            else if (string.IsNullOrWhiteSpace(model.Contents))
            {
                model.Message = "You must provide an item to store in your locker.";
                return View(model);
            }

            var locker = new Locker
            {
                LockerNumber = model.LockerNumber,
                RenterName = model.RenterName,
                Contents = model.Contents
            };

            var result = _lockerManager.RentLocker(locker);

            if (result.Ok)
            {
                model.SuccessMessage = result.Message;
                return View(model);
            }

            return RedirectToAction("Menu");
        }

        /// <summary>
        /// Takes the user to a web page where they can select a locker to end their rental. (Menu Choice 3: Input)
        /// </summary>
        /// <returns>A Viewresult object with the model state.</returns>
        public IActionResult EndRental()
        {
            var model = new EndRentalForm();
            return View(model);
        }

        /// <summary>
        /// Validates the user's input for ending a locker rental. (Menu Choice 3: Validation)
        /// </summary>
        /// <param name="model">Used for user input and validation.</param>
        /// <returns>A RedirectToActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EndRental(EndRentalForm model)
        {
            if (int.TryParse(model.LockerNumber, out int choice) && choice >= 1 && choice <= 100)
            {
                var isRented = _lockerManager.IsRented(choice);

                if (!isRented)
                {
                    model.Message = $"Locker {choice} is not currently rented. Please try another locker number.";
                    return View(model);
                }

                return RedirectToAction("DeleteLocker", new { id = choice });
            }

            model.Message = "Invalid input. Please choose a number from 1 to 100.";
            return View(model);
        }

        /// <summary>
        /// Ends the locker rental for the user. (Menu Choice 3: Action)
        /// </summary>
        /// <param name="id">The selected locker number to delete.</param>
        /// <returns>A Viewresult object with the model state.</returns>
        public IActionResult DeleteLocker(int id)
        {
            var result = _lockerManager.EndLockerRental(id);

            if (result.Ok)
            {
                var model = new DeleteLockerDisplay();
                model.Message = result.Message;
                return View(model);
            }

            return RedirectToAction("Menu");
        }

        /// <summary>
        /// Displays all rented lockers and their contents.
        /// </summary>
        /// <returns>A ViewResult object.</returns>
        public IActionResult AllLockers()
        {
            var model = new AllLockersDisplay();
            var result = _lockerManager.GetAllLockers();

            if (result.Ok)
            {
                model.Lockers = result.Data;
                return View(model);
            }

            model.Message = result.Message;
            return View();
        }

        /// <summary>
        /// Take the user to a web page where they "exit" the application.
        /// </summary>
        /// <returns>A ViewResult object.</returns>
        public IActionResult Exit()
        {
            return View();
        }
    }
}