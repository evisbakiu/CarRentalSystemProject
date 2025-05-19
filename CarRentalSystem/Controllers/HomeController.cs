using CarRentalSystem.Data;
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
        var cars = _context!.Car!.ToList();

        var isConsentGiven = Request.Cookies["userConsent"] == "true";
        ViewBag.ConsentGiven = isConsentGiven;

        if (isConsentGiven)
        {
            var fullName = HttpContext.Session.GetString("FullName");

            if (!string.IsNullOrEmpty(fullName))
            {
                Response.Cookies.Append("WelcomeName", fullName, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    IsEssential = true
                });

                HttpContext.Session.Remove("FullName");
                ViewBag.ShowWelcome = true;
                ViewBag.FullName = fullName;
            }
            else
            {
                var cookieName = Request.Cookies["WelcomeName"];
                ViewBag.FullName = cookieName;
                ViewBag.ShowWelcome = !string.IsNullOrEmpty(cookieName);
            }
        }
        else
        {
            // No consent: don't personalize, don't show modal
            Response.Cookies.Delete("WelcomeName");
            ViewBag.ShowWelcome = false;
            ViewBag.FullName = null;
        }

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
        var availableCars = _context!.Car!
            .Where(car => !_context!.Reservation!
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
        var car = _context!.Car!.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();

        return View(car);
    }
}