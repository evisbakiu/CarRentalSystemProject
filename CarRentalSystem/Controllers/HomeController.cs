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

    public IActionResult Index()
    {
        var cars = _context.Car.ToList(); 
        return View(cars);
    }    
    
    public IActionResult Terms()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

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

    public IActionResult Details(Guid id)
    {
        var car = _context.Car.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();

        return View(car);
    }
}
