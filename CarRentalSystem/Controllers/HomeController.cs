using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Homepage with search form
    public IActionResult Index()
    {
        return View();
    }

    // POST: Search for available cars
    [HttpPost]
    public IActionResult Search(DateTime pickUpDate, DateTime dropOffDate)
    {
        var availableCars = _context.Car
            .Where(car => !_context.Reservation
                .Any(reservation =>
                    reservation.CarId == car.Id &&
                    ((pickUpDate >= reservation.StartDate && pickUpDate <= reservation.EndDate) ||
                     (dropOffDate >= reservation.StartDate && dropOffDate <= reservation.EndDate))))
            .Include(car => car.Category)
            .ToList();

        return View("SearchResults", availableCars);
    }

    // GET: Show details of a car
    public IActionResult Details(Guid id)
    {
        var car = _context.Car.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();

        return View(car);
    }
}
