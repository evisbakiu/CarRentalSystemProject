using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Models;
using CarRentalSystem.Data;
using Microsoft.AspNetCore.Identity;

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

        // GET: /Reservation/Book/{id}
        [HttpGet]
        public async Task<IActionResult> Book(Guid id)
        {
            var car = await _context.Car.FirstOrDefaultAsync(c => c.Id == id && c.IsAvailable);

            if (car == null)
                return NotFound("Car not found or is already booked.");

            return View(car);
        }

        // POST: /Reservation/Book
        [HttpPost]
        public async Task<IActionResult> Book(Guid carId, DateTime startDate, DateTime endDate)
        {
            var user = await _userManager.GetUserAsync(User);
            var car = await _context.Car.FindAsync(carId);

            if (car == null || !car.IsAvailable)
            {
                ModelState.AddModelError("", "Car is not available.");
                return RedirectToAction("Index", "Home");
            }

            if (startDate >= endDate || startDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Invalid reservation dates.");
                return RedirectToAction("Book", new { id = carId });
            }

            var reservation = new Reservation
            {
                CarId = car.Id,
                UserId = user.Id,
                StartDate = startDate,
                EndDate = endDate,
                TotalCost = (endDate - startDate).Days * car.PricePerDay,
                StatusId = _context.Status.First(s => s.Name == "Pending").Id
            };

            car.IsAvailable = false;

            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Car booked successfully!";
            return RedirectToAction("Index", "Home");
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
    }
}
