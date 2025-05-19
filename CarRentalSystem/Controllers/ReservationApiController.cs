using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/reservations/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservations()
        {
            var reservations = await _context.Reservation
                .Include(r => r.Car)
                .Include(r => r.Status)
                .Include(r => r.User)
                .ToListAsync();

            return Ok(reservations);
        }

        // GET: api/reservations/details/{id}
        [HttpGet("details/{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(Guid id)
        {
            var reservation = await _context.Reservation
                .Include(r => r.Car)
                .Include(r => r.Status)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        // POST: api/reservations/create
        [HttpPost("create")]
        public async Task<ActionResult<Reservation>> CreateReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
        }

        // DELETE: api/reservations/cancel/{id}
        [HttpDelete("cancel/{id}")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
                return NotFound();

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
