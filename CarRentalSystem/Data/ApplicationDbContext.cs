using CarRentalSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car>? Cars { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Report>? Reports { get; set; }
        public DbSet<Status>? Statuses { get; set; }
    }
}
