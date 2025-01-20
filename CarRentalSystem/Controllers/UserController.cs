using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<User> userManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> CarList()
        {
            var cars = await _context.Car
                .Include(c => c.Class)
                .Include(c => c.Features)
                .OrderBy(c => c.Class.Name)
                .ToListAsync();

            var groupedCars = cars.GroupBy(c => c.Class.Name)
                                  .OrderBy(g => g.Key)
                                  .ToList();

            return View(groupedCars);
        }
        public async Task<IActionResult> CarDetails(Guid id)
        {
            var car = await _context.Car.Include(c => c.PricingTiers).FirstOrDefaultAsync(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        public async Task<IActionResult> Rent(Guid id)
        {
            var car = await _context.Car
                .Include(c => c.Features)
                .Include(c => c.Class)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }


        [HttpPost]
        public async Task<IActionResult> Rent(Guid carId, DateTime startDate, DateTime endDate)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["CarId"] = carId;
                TempData["StartDate"] = startDate;
                TempData["EndDate"] = endDate;
                return RedirectToAction("Login", "Account");
            }

            var car = await _context.Car.Include(c => c.PricingTiers).FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null || !car.IsAvailable) return NotFound();

            int totalDays = (endDate - startDate).Days;
            var tier = car.PricingTiers.FirstOrDefault(t => t.MinDays <= totalDays && t.MaxDays >= totalDays);

            if (tier == null) return BadRequest("Invalid pricing tier.");

            var user = await _userManager.GetUserAsync(User);
            var reservation = new Reservation
            {
                CarId = carId,
                UserId = user.Id,
                StartDate = startDate,
                EndDate = endDate,
                TotalCost = totalDays * tier.PricePerDay,
                StatusId = _context.Status.FirstOrDefault(s => s.Name == "Pending").Id
            };

            car.IsAvailable = false;
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Car booked successfully!";
            return RedirectToAction("Confirmation", new { reservationId = reservation.Id });
        }
        public async Task<IActionResult> Confirmation(Guid reservationId)
        {
            var reservation = await _context.Reservation
                .Include(r => r.Car)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null) return NotFound();
            return View(reservation);
        }

        public async Task<IActionResult> MyBookings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings = await _context.Reservation
                .Where(r => r.UserId == user.Id)
                .Include(r => r.Car)
                .Include(x=>x.Status)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();

            return View(bookings);
        }

        public async Task<IActionResult> Prices()
        {
            var cars = await _context.Car
                .Include(c => c.Category)
                .Include(c => c.PricingTiers)
                .OrderBy(c => c.Category.Name)
                .ToListAsync();

            var groupedCars = cars.GroupBy(c => c.Category.Name)
                                  .OrderBy(g => g.Key)
                                  .ToList();

            var ranges = new List<(int Min, int Max)>
                        {
                            (1, 3),
                            (4, 10),
                            (11, 15),
                            (16, 20),
                            (21, 365)
                        };

            ViewBag.Ranges = ranges;

            return View(groupedCars);
        }

    }
}
