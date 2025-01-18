using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cars()
        {
            var cars = _context.Car.Include(c => c.Category).ToList();
            return View(cars);
        }

        public IActionResult Reservations()
        {
            var reservations = _context.Reservation
                .Include(r => r.Car)
                .Include(r => r.User)
                .Include(r => r.Status)
                .ToList();
            return View(reservations);
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Reports()
        {
            var reports = _context.Report.Include(r => r.User).ToList();
            return View(reports);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            ViewBag.Categories = _context.Category.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/cars");
                var filePath = Path.Combine(uploadsFolder, ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                car.ImagePath = "/images/cars/" + ImageFile.FileName;
            }

            _context.Car.Add(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cars");
        }

        [HttpGet]
        public IActionResult EditCar(int id)
        {
            var car = _context.Car.Find(id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(Car car)
        {
            _context.Car.Update(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cars");
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Cars");
        }

        public async Task<IActionResult> CancelReservation(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                reservation.StatusId = _context.Status.First(s => s.Name == "Canceled").Id;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Reservations");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Users");
        }

        public IActionResult GenerateReport()
        {
            var report = new Report
            {
                DateGenerated = DateTime.Now,
                ReportDetails = $"Total Reservations: {_context.Reservation.Count()}",
                //UserId = _userManager.GetUserId(User)
            };

            _context.Report.Add(report);
            _context.SaveChanges();

            return RedirectToAction("Reports");
        }

    }
}
