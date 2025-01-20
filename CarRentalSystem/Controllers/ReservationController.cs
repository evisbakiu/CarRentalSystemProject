using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Models;
using CarRentalSystem.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CarRentalSystem.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ReservationController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var reservations = _context!.Reservation!
                .Include(r => r.Car)
                .Include(r => r.User)
                .Include(r => r.Status)
                .ToList();

            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> Rent(Guid id)
        {
            var car = await _context.Car.FirstOrDefaultAsync(c => c.Id == id && c.IsAvailable);

            if (car == null)
                return NotFound("Car not found or is already booked.");

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(Guid carId, DateTime pickUpDateTime, DateTime returnDateTime)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "You need to log in to book a car.";
                return RedirectToAction("Login", "Account");
            }

            var car = await _context.Car.FindAsync(carId);
            if (car == null || !car.IsAvailable)
            {
                ModelState.AddModelError("", "The selected car is not available.");
                return RedirectToAction("Rent", new { id = carId });
            }

            if (pickUpDateTime >= returnDateTime || pickUpDateTime < DateTime.Now)
            {
                ModelState.AddModelError("", "Invalid pick-up or return date/time.");
                return RedirectToAction("Rent", new { id = carId });
            }

            var reservation = new Reservation
            {
                CarId = carId,
                UserId = user.Id, // Set the UserId
                StartDate = pickUpDateTime,
                EndDate = returnDateTime,
                TotalCost = (returnDateTime - pickUpDateTime).Days * car.PricePerDay,
                StatusId = _context.Status.First(s => s.Name == "Pending").Id
            };

            // Mark the car as unavailable
            car.IsAvailable = false;

            // Save the reservation
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Your booking has been successfully created!";
            return RedirectToAction("MyBookings", "User");
        }


        public async Task<IActionResult> Cancel(Guid id)
        {
            var reservation = await _context!.Reservation!.FindAsync(id);
            if (reservation != null)
            {
                reservation.StatusId = _context!.Status!.First(s => s.Name == "Canceled").Id;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ReviewReservation(Guid carId, DateTime pickUpDateTime, DateTime returnDateTime)
        {
            var car = await _context.Car
                .Include(c => c.Class)
                .Include(c => c.PricingTiers)
                .FirstOrDefaultAsync(c => c.Id == carId);

            if (car == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "You must be logged in to proceed with a reservation.";
                return RedirectToAction("Login", "Account");
            }

            var rentalDays = (returnDateTime - pickUpDateTime).Days;

            var applicableTier = car.PricingTiers.FirstOrDefault(t => rentalDays >= t.MinDays && rentalDays <= t.MaxDays);
            if (applicableTier == null)
            {
                TempData["Error"] = "No pricing tier is available for the selected rental period.";
                return RedirectToAction("CarList", "UserCar");
            }

            var reservationDetails = new ReservationViewModel
            {
                CarId = car.Id, // Ensure CarId is set
                UserName = user.UserName,
                UserEmail = user.Email,
                CarName = car.Name,
                CarClass = car.Class?.Name,
                FuelType = car.FuelType,
                Gearbox = car.Gearbox,
                PricePerDay = applicableTier.PricePerDay,
                TotalCost = rentalDays * applicableTier.PricePerDay,
                PickUpDateTime = pickUpDateTime,
                ReturnDateTime = returnDateTime
            };

            return View(reservationDetails);
        }



        [HttpPost]
        public async Task<IActionResult> ConfirmReservation(Guid carId, DateTime pickUpDateTime, DateTime returnDateTime)
        {
            var car = await _context.Car.FindAsync(carId);
            if (car == null || !car.IsAvailable)
            {
                ModelState.AddModelError("", "The selected car is not available.");
                return RedirectToAction("ReviewReservation", new { carId, pickUpDateTime, returnDateTime });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "You must be logged in to proceed with a reservation.";
                return RedirectToAction("Login", "Account");
            }

            var reservation = new Reservation
            {
                CarId = carId,
                UserId = user.Id,
                StartDate = pickUpDateTime,
                EndDate = returnDateTime,
                TotalCost = (returnDateTime - pickUpDateTime).Days * car.PricePerDay,
                StatusId = _context.Status.FirstOrDefault(s => s.Name == "Pending")!.Id
            };

            car.IsAvailable = false;

            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Your reservation has been successfully confirmed!";
            return RedirectToAction("MyBookings", "User");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var reservation = await _context.Reservation
                .Include(r => r.Car).ThenInclude(x=>x.Class)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

    }
}
