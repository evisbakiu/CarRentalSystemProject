﻿using CarRentalSystem.Constants;
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

            var existingCar = await _context.Car
                .Include(c => c.PricingTiers)
                .FirstOrDefaultAsync(c => c.Id == car.Id);

            if (existingCar == null)
            {
                return NotFound();
            }

            existingCar.Name = car.Name;
            existingCar.LicensePlate = car.LicensePlate;
            existingCar.Year = car.Year;
            existingCar.PricePerDay = car.PricePerDay;
            existingCar.CategoryId = car.CategoryId;
            existingCar.ClassId = car.ClassId;
            existingCar.FuelType = car.FuelType;
            existingCar.FuelUsage = car.FuelUsage;

            _context.PricingTier.RemoveRange(existingCar.PricingTiers); 

            foreach (var tier in car.PricingTiers)
            {
                tier.Id = Guid.NewGuid(); 
                tier.CarId = car.Id;
                _context.PricingTier.Add(tier); 
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
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();

            var model = users.Select(user => new ManageUserViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Fullname = user.FullName, 
                DateOfBirth = user.DateOfBirth 
            }).ToList();

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
