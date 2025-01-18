using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Models;
using CarRentalSystem.Data;

namespace CarRentalSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reports = _context.Report.ToList();
            return View(reports);
        }

        public IActionResult Generate()
        {
            var report = new Report
            {
                DateGenerated = DateTime.Now,
                ReportDetails = $"Total Reservations: {_context.Reservation.Count()}"
            };

            _context.Report.Add(report);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
