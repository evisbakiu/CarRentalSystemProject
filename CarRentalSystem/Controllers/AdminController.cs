using CarRentalSystem.Constants;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using CarRentalSystem.Validators;
using CarRentalSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        #region Manage Cars

        public async Task<IActionResult> ManageCars()
        {
            var cars = await _context.Car.Include(c => c.PricingTiers).ToListAsync();
            return View("~/Views/Admin/CarList.cshtml", cars);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            ViewData["Title"] = "Add New Car";
            ViewData["Action"] = "AddCar";

            ViewBag.Categories = _context.Category.ToList();
            ViewBag.Classes = _context.CarClass.ToList();
            ViewBag.Features = _context.CarFeature.Select(f => f.Name).Distinct().ToList();
            ViewBag.FuelTypes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Petrol", Text = "Petrol" },
                    new SelectListItem { Value = "Diesel", Text = "Diesel" },
                    new SelectListItem { Value = "Hybrid", Text = "Hybrid" },
                    new SelectListItem { Value = "Electric", Text = "Electric" }
                };

            var car = new Car
            {
                PricingTiers = new List<PricingTier> { new PricingTier() }
            };

            return View("AddOrEditCar", car);
        }


        [HttpPost]
        public async Task<IActionResult> AddCar(Car car, List<string> selectedFeatures)
        {
            if (car.PricingTiers == null || !car.PricingTiers.Any())
            {
                car.PricingTiers = new List<PricingTier>();

                foreach (var range in PricingRanges.Ranges)
                {
                    var pricePerDay = HttpContext.Request.Form[$"PricingTiers[{range.Min}].PricePerDay"];
                    if (!string.IsNullOrEmpty(pricePerDay) && decimal.TryParse(pricePerDay, out decimal parsedPrice))
                    {
                        car.PricingTiers.Add(new PricingTier
                        {
                            MinDays = range.Min,
                            MaxDays = range.Max,
                            PricePerDay = parsedPrice
                        });
                    }
                }
            }

            if (!ModelState.IsValid || car.PricingTiers.Count != PricingRanges.Ranges.Count)
            {
                ModelState.AddModelError("", "All pricing tiers must be filled.");
                ViewBag.Categories = _context.Category.ToList();
                ViewBag.Classes = _context.CarClass.ToList();
                ViewBag.Features = _context.CarFeature.Select(f => f.Name).Distinct().ToList();
                return View("AddOrEditCar", car);
            }

            car.PricePerDay = car.PricingTiers.FirstOrDefault()?.PricePerDay ?? 0;

            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            if (selectedFeatures != null && selectedFeatures.Any())
            {
                foreach (var feature in selectedFeatures)
                {
                    _context.CarFeature.Add(new CarFeature
                    {
                        Id = Guid.NewGuid(),
                        Name = feature,
                        CarId = car.Id
                    });
                }
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Car added successfully!";
            return RedirectToAction("ManageCars");
        }



        [HttpGet]
        public async Task<IActionResult> EditCar(Guid id)
        {
            var car = await _context.Car
                .Include(c => c.PricingTiers)
                .Include(c => c.Features)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit Car";
            ViewData["Action"] = "EditCar";

            ViewBag.Categories = _context.Category.ToList();
            ViewBag.Classes = _context.CarClass.ToList();
            ViewBag.Features = _context.CarFeature.Select(f => f.Name).Distinct().ToList();
            ViewBag.FuelTypes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Petrol", Text = "Petrol" },
                    new SelectListItem { Value = "Diesel", Text = "Diesel" },
                    new SelectListItem { Value = "Hybrid", Text = "Hybrid" },
                    new SelectListItem { Value = "Electric", Text = "Electric" }
                };

            return View("AddOrEditCar", car);
        }


        [HttpPost]
        public async Task<IActionResult> EditCar(Car car)
        {
            if (!ValidatePricingTiers.Value(car.PricingTiers))
            {
                ModelState.AddModelError("", "Please provide pricing tiers for all required ranges.");
                ViewBag.Categories = _context.Category.ToList();
                return View("AddOrEditCar", car);
            }

            if (ModelState.IsValid)
            {
                var existingCar = await _context.Car.Include(c => c.PricingTiers).FirstOrDefaultAsync(c => c.Id == car.Id);
                if (existingCar == null) return NotFound();

                existingCar.Name = car.Name;
                existingCar.LicensePlate = car.LicensePlate;
                existingCar.Year = car.Year;
                existingCar.PricePerDay = car.PricePerDay;
                existingCar.CategoryId = car.CategoryId;

                _context.PricingTier.RemoveRange(existingCar.PricingTiers);
                foreach (var tier in car.PricingTiers)
                {
                    tier.CarId = car.Id;
                    _context.PricingTier.Add(tier);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("ManageCars");
            }

            ViewBag.Categories = _context.Category.ToList();
            return View("AddOrEditCar", car);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null) return NotFound();

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageCars");
        }


        #endregion

        #region Manage Reservations

        public IActionResult Reservations()
        {
            var reservations = _context.Reservation
                .Include(r => r.Car)
                .Include(r => r.User)
                .Include(r => r.Status)
                .ToList();
            return View(reservations);
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

        #endregion

        #region Manage Users
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();

            var model = new List<ManageUserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new ManageUserViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = string.Join(", ", roles)
                });
            }

            return View(model);
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
        #endregion

        #region Manage Reports
        public IActionResult Reports()
        {
            var reports = _context.Report.Include(r => r.User).ToList();
            return View(reports);
        }

        public IActionResult GenerateReport()
        {
            // Get the current user's ID
            var userId = _userManager.GetUserId(User); 

            if (string.IsNullOrEmpty(userId))
            {
                // Handle case when the user is not authenticated or the ID is not found
                TempData["Error"] = "User not authenticated. Cannot generate report.";
                return RedirectToAction("Reports");
            }

            var report = new Report
            {
                DateGenerated = DateTime.Now,
                ReportDetails = $"Total Reservations: {_context.Reservation.Count()}",
                UserId = Guid.Parse(userId) // Parse the UserId if it's stored as a Guid
            };

            _context.Report.Add(report);
            _context.SaveChanges();

            return RedirectToAction("Reports");
        }

        #endregion
    }
}
