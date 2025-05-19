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
        private readonly RoleManager<Role> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetReservationDetails(DateTime? startDate, DateTime? endDate)
        {
            // Default to current month if no dates provided
            if (!startDate.HasValue)
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (!endDate.HasValue)
                endDate = startDate.Value.AddMonths(1).AddDays(-1);

            var reservations = await _context.Reservation
                .Include(r => r.Car)
                .Include(r => r.User)
                .Include(r => r.Status)
                .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
                .Select(r => new
                {
                    id = r.Id,
                    carName = r.Car.Name,
                    carLicensePlate = r.Car.LicensePlate,
                    userName = r.User.FullName,
                    userEmail = r.User.Email,
                    startDate = r.StartDate,
                    endDate = r.EndDate,
                    totalCost = r.TotalCost,
                    status = r.Status.Name,
                    durationDays = (r.EndDate - r.StartDate).Days
                })
                .ToListAsync();

            return Json(new { data = reservations });
        }


        [HttpGet]
        public async Task<IActionResult> GetFuelTypeDistribution()
        {
            var fuelTypes = await _context.Car
                .GroupBy(c => c.FuelType)
                .Select(g => new
                {
                    fuelType = g.Key,
                    count = g.Count()
                })
                .OrderByDescending(x => x.count)
                .ToListAsync();

            return Json(fuelTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationsByMonth()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var monthlyData = new List<object>();

                for (int month = 1; month <= 12; month++)
                {
                    DateTime monthStart, monthEnd;
                    try
                    {
                        monthStart = new DateTime(currentYear, month, 1);
                        monthEnd = monthStart.AddMonths(1).AddDays(-1);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        continue;
                    }

                    var reservationCount = await _context.Reservation
                        .CountAsync(r => r.StartDate >= monthStart && r.StartDate <= monthEnd);

                    monthlyData.Add(new
                    {
                        month = monthStart.ToString("MMMM"),
                        reservationCount
                    });
                }

                return Json(monthlyData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCarAvailabilityRate()
        {
            int totalCars = await _context.Car.CountAsync();
            int availableCars = await _context.Car.CountAsync(c => c.IsAvailable);

            var availabilityRate = totalCars > 0 ? (double)availableCars / totalCars * 100 : 0;

            return Json(new
            {
                totalCars,
                availableCars,
                availabilityRate = Math.Round(availabilityRate, 2)
            });
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
            if (ModelState.IsValid)
            {
                car.PricingTiers ??= PricingRanges.Ranges.Select(range => new PricingTier
                {
                    MinDays = range.Min,
                    MaxDays = range.Max,
                    PricePerDay = decimal.Parse(HttpContext.Request.Form[$"PricingTiers[{range.Min}].PricePerDay"])
                }).ToList();

                _context.Car.Add(car);
                await _context.SaveChangesAsync();

                if (selectedFeatures?.Any() == true)
                {
                    foreach (var feature in selectedFeatures)
                    {
                        _context.CarFeature.Add(new CarFeature { Name = feature, CarId = car.Id });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["Success"] = "Car added successfully!";
                return RedirectToAction("ManageCars");
            }

            ViewBag.Categories = _context.Category.ToList();
            ViewBag.Classes = _context.CarClass.ToList();
            ViewBag.Features = _context.CarFeature.Select(f => f.Name).Distinct().ToList();
            return View("AddOrEditCar", car);
        }



        [HttpGet]
        public async Task<IActionResult> EditCar(Guid id)
        {
            var car = await _context.Car
                .Include(c => c.PricingTiers)
                .Include(c => c.Features)
                .Include(c => c.Class)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit Car";
            ViewData["Action"] = "EditCar";

            var pricingRanges = PricingRanges.Ranges;
            var pricingTiers = new List<PricingTier>();
            foreach (var range in pricingRanges)
            {
                var existingTier = car.PricingTiers.FirstOrDefault(pt => pt.MinDays == range.Min && pt.MaxDays == range.Max);
                pricingTiers.Add(existingTier ?? new PricingTier
                {
                    MinDays = range.Min,
                    MaxDays = range.Max,
                    PricePerDay = 0 
                });
            }

            ViewBag.PricingTiers = pricingTiers;
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
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Category.ToList();
                ViewBag.Classes = _context.CarClass.ToList();
                ViewBag.FuelTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Petrol", Text = "Petrol" },
            new SelectListItem { Value = "Diesel", Text = "Diesel" },
            new SelectListItem { Value = "Hybrid", Text = "Hybrid" },
            new SelectListItem { Value = "Electric", Text = "Electric" }
        };
                return View("AddOrEditCar", car);
            }

            var existingCar = await _context.Car.Include(c => c.PricingTiers).FirstOrDefaultAsync(c => c.Id == car.Id);

            if (existingCar == null)
            {
                return NotFound();
            }

            // Update basic car properties
            existingCar.Name = car.Name;
            existingCar.LicensePlate = car.LicensePlate;
            existingCar.Year = car.Year;
            existingCar.Gearbox = car.Gearbox;
            existingCar.FuelType = car.FuelType;
            existingCar.FuelUsage = car.FuelUsage;
            existingCar.CategoryId = car.CategoryId;
            existingCar.ClassId = car.ClassId;

            // Remove existing PricingTiers
            _context.PricingTier.RemoveRange(existingCar.PricingTiers);

            // Add new PricingTiers
            if (car.PricingTiers != null && car.PricingTiers.Any())
            {
                foreach (var tier in car.PricingTiers)
                {
                    existingCar.PricingTiers.Add(new PricingTier
                    {
                        MinDays = tier.MinDays,
                        MaxDays = tier.MaxDays,
                        PricePerDay = tier.PricePerDay
                    });
                }
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Car updated successfully!";
            return RedirectToAction("ManageCars");
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
        public async Task<IActionResult> Users(string roleFilter, string emailSearch)
        {
            var usersQuery = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(emailSearch))
            {
                usersQuery = usersQuery.Where(u => u.Email.Contains(emailSearch));
            }

            var allUsers = await usersQuery.ToListAsync();
            var model = new List<ManageUserViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (!string.IsNullOrEmpty(roleFilter) && !roles.Contains(roleFilter))
                    continue;

                model.Add(new ManageUserViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Fullname = user.FullName,
                    DateOfBirth = user.DateOfBirth,
                    Roles = roles.ToList()
                });
            }

            ViewBag.AvailableRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.RoleFilter = roleFilter;
            ViewBag.EmailSearch = emailSearch;

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

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Roles = allRoles.Select(role => new RoleSelectionViewModel
                {
                    RoleName = role,
                    IsSelected = userRoles.Contains(role)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            // Update profile info
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.DateOfBirth;

            await _userManager.UpdateAsync(user);

            // Update roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName);

            var rolesToAdd = selectedRoles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(selectedRoles);

            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction("Users");
        }

        #endregion

        #region Manage Reports
        public IActionResult Reports()
        {
            var reports = _context.Report.Include(r => r.User).ToList();
            return View(reports);
        }

        public async Task<IActionResult> GenerateReport()
        {
            var userId = _userManager.GetUserId(User); 

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "User not authenticated. Cannot generate report.";
                return RedirectToAction("Reports");
            }

            var user = await _userManager.GetUserAsync(User);

            var report = new Report
            {
                DateGenerated = DateTime.Now,
                ReportDetails = $"Total Reservations: {_context.Reservation.Count()}",
                UserId = Guid.Parse(userId) ,
                User = user
            };

            _context.Report.Add(report);
            _context.SaveChanges();

            return RedirectToAction("Reports");
        }

        [HttpPost]
        public IActionResult ChangeReservationStatus(Guid id)
        {
            var reservation = _context.Reservation.Include(x=>x.Status).FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return Json(new { success = false, message = "Reservation not found." });
            }

            if (reservation.Status.Name == "Confirmed")
            {
                reservation.Status = _context.Status.FirstOrDefault(s => s.Name == "Pending");
            }
            else
            {
                reservation.Status = _context.Status.FirstOrDefault(s => s.Name == "Confirmed");
            }

            _context.SaveChanges();

            return Json(new
            {
                success = true,
                status = reservation.Status.Name
            });
        }

        #endregion


    }
}
