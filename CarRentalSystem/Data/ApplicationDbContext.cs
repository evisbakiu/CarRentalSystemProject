using CarRentalSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car>? Car { get; set; }
        public DbSet<Reservation>? Reservation { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Report>? Report { get; set; }
        public DbSet<Status>? Status { get; set; }
        public DbSet<Payment>? Payment { get; set; }
    }
}
