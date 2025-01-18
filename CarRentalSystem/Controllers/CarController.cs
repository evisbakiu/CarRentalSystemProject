using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cars = _context!.Car!.Include(c => c.Category).ToList();
            return View(cars);
        }

        public IActionResult AddCar()
        {
            ViewBag.Categories = _context!.Category!.ToList();
            return View();
        }

        public IActionResult Book(Guid id)
        {
            var car = _context.Car.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/cars");
                var filePath = Path.Combine(uploadsFolder, imageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                car.ImagePath = "/images/cars/" + imageFile.FileName;
            }

            _context!.Car!.Add(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditCar(Guid id)
        {
            var car = _context!.Car!.Find(id);
            if (car == null) return NotFound();

            ViewBag.Categories = _context!.Category!.ToList();
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(Car car)
        {
            _context!.Car!.Update(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCar(Guid id)
        {
            var car = await _context!.Car!.FindAsync(id);
            if (car != null)
            {
                _context!.Car!.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
