using Airport.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Airport;

namespace Portfolio.Controllers
{
    public class AirportController : Controller
    {
        private readonly LockerManager _lockerManager;
        public int lockerNumber;

        public AirportController(LockerManager lockerManager)
        {
            _lockerManager = lockerManager;
            lockerNumber = 0;
        }

        public IActionResult Menu()
        {
            var model = new AirportMenu();
            return View(model);
        }

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

        public IActionResult ViewLocker()
        {
            var model = new ViewLockerForm();
            return View(model);
        }

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

        public IActionResult RentLocker()
        {
            var model = new RentLockerForm();
            return View(model);
        }

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

                lockerNumber = choice;
                return RedirectToAction("AddLocker", new { id = lockerNumber });
            }

            model.Message = "Invalid input. Please choose a number from 1 to 100.";
            return View(model);
        }

        public IActionResult AddLocker(int id)
        {
            var model = new AddLockerForm();
            model.LockerNumber = id;
            return View(model);
        }

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

        public IActionResult EndRental()
        {
            var model = new EndRentalForm();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EndRental(EndRentalForm model)
        {
            if ((int.TryParse(model.LockerNumber, out int choice) && choice >= 1 && choice <= 100))
            {
                var isRented = _lockerManager.IsRented(choice);

                if (!isRented)
                {
                    model.Message = $"Locker {choice} is not currently rented. Please try another locker number.";
                    return View(model);
                }

                lockerNumber = choice;
                return RedirectToAction("DeleteLocker", new { id = lockerNumber });
            }

            model.Message = "Invalid input. Please choose a number from 1 to 100.";
            return View(model);
        }

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

        public IActionResult Exit()
        {
            return View();
        }
    }
}