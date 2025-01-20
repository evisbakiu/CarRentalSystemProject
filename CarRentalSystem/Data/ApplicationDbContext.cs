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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .Property(c => c.IsAvailable)
                .HasDefaultValue(true);

            // Seed Categories
            var economyCategoryId = Guid.NewGuid();
            var standardCategoryId = Guid.NewGuid();
            var luxuryCategoryId = Guid.NewGuid();
            var businessCategoryId = Guid.NewGuid();
            var familyCategoryId = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = economyCategoryId, Name = "Economy" },
                new Category { Id = standardCategoryId, Name = "Standard" },
                new Category { Id = luxuryCategoryId, Name = "Luxury" },
                new Category { Id = businessCategoryId, Name = "Business" },
                new Category { Id = familyCategoryId, Name = "Family" }
            );

            // Seed Classes
            var smallClassId = Guid.NewGuid();
            var compactClassId = Guid.NewGuid();
            var suvClassId = Guid.NewGuid();
            var luxuryClassId = Guid.NewGuid();
            var vanClassId = Guid.NewGuid();

            modelBuilder.Entity<CarClass>().HasData(
                new CarClass { Id = smallClassId, Name = "Small" },
                new CarClass { Id = compactClassId, Name = "Compact" },
                new CarClass { Id = suvClassId, Name = "SUV" },
                new CarClass { Id = luxuryClassId, Name = "Luxury" },
                new CarClass { Id = vanClassId, Name = "Van" }
            );

            // Seed Cars
            var cars = new List<Car>
    {
        // Small Cars
        new Car { Id = Guid.NewGuid(), Name = "Fiat Panda", LicensePlate = "AA-111-BB", Year = 2020, PricePerDay = 12, IsAvailable = true, ImagePath = "/images/fiat_panda.jpg", FuelType = "Petrol", Gearbox = "Manual", FuelUsage = 4, ClassId = smallClassId, CategoryId = economyCategoryId },
        new Car { Id = Guid.NewGuid(), Name = "Hyundai i10", LicensePlate = "BB-222-CC", Year = 2019, PricePerDay = 10, IsAvailable = true, ImagePath = "/images/hyundai_i10.jpg", FuelType = "Petrol", Gearbox = "Manual", FuelUsage = 3.8M, ClassId = smallClassId, CategoryId = economyCategoryId },

        // Compact Cars
        new Car { Id = Guid.NewGuid(), Name = "Volkswagen Golf", LicensePlate = "CC-333-DD", Year = 2021, PricePerDay = 18, IsAvailable = true, ImagePath = "/images/vw_golf.jpg", FuelType = "Diesel", Gearbox = "Automatic", FuelUsage = 5.1M, ClassId = compactClassId, CategoryId = standardCategoryId },
        new Car { Id = Guid.NewGuid(), Name = "Ford Focus", LicensePlate = "DD-444-EE", Year = 2020, PricePerDay = 17, IsAvailable = true, ImagePath = "/images/ford_focus.jpg", FuelType = "Petrol", Gearbox = "Manual", FuelUsage = 4.8M, ClassId = compactClassId, CategoryId = standardCategoryId },

        // SUVs
        new Car { Id = Guid.NewGuid(), Name = "Toyota RAV4", LicensePlate = "EE-555-FF", Year = 2022, PricePerDay = 25, IsAvailable = true, ImagePath = "/images/toyota_rav4.jpg", FuelType = "Hybrid", Gearbox = "Automatic", FuelUsage = 4.2M, ClassId = suvClassId, CategoryId = familyCategoryId },
        new Car { Id = Guid.NewGuid(), Name = "Honda CR-V", LicensePlate = "FF-666-GG", Year = 2023, PricePerDay = 27, IsAvailable = true, ImagePath = "/images/honda_crv.jpg", FuelType = "Petrol", Gearbox = "Automatic", FuelUsage = 6, ClassId = suvClassId, CategoryId = familyCategoryId },

        // Luxury Cars
        new Car { Id = Guid.NewGuid(), Name = "Mercedes-Benz S-Class", LicensePlate = "GG-777-HH", Year = 2023, PricePerDay = 85, IsAvailable = true, ImagePath = "/images/mercedes_s_class.jpg", FuelType = "Petrol", Gearbox = "Automatic", FuelUsage = 9, ClassId = luxuryClassId, CategoryId = luxuryCategoryId },
        new Car { Id = Guid.NewGuid(), Name = "BMW 7 Series", LicensePlate = "HH-888-II", Year = 2023, PricePerDay = 80, IsAvailable = true, ImagePath = "/images/bmw_7_series.jpg", FuelType = "Diesel", Gearbox = "Automatic", FuelUsage = 8.5M, ClassId = luxuryClassId, CategoryId = businessCategoryId },

        // Vans
        new Car { Id = Guid.NewGuid(), Name = "Volkswagen Transporter", LicensePlate = "II-999-JJ", Year = 2021, PricePerDay = 40, IsAvailable = true, ImagePath = "/images/vw_transporter.jpg", FuelType = "Diesel", Gearbox = "Manual", FuelUsage = 8, ClassId = vanClassId, CategoryId = familyCategoryId },
        new Car { Id = Guid.NewGuid(), Name = "Ford Transit", LicensePlate = "JJ-101-KK", Year = 2022, PricePerDay = 38, IsAvailable = true, ImagePath = "/images/ford_transit.jpg", FuelType = "Diesel", Gearbox = "Manual", FuelUsage = 7.5M, ClassId = vanClassId, CategoryId = familyCategoryId }
    };

            modelBuilder.Entity<Car>().HasData(cars);

            // Seed PricingTiers
            var pricingTiers = new List<PricingTier>();
            foreach (var car in cars)
            {
                pricingTiers.AddRange(new[]
                {
            new PricingTier { Id = Guid.NewGuid(), CarId = car.Id, MinDays = 1, MaxDays = 3, PricePerDay = car.PricePerDay },
            new PricingTier { Id = Guid.NewGuid(), CarId = car.Id, MinDays = 4, MaxDays = 10, PricePerDay = car.PricePerDay * 0.9M },
            new PricingTier { Id = Guid.NewGuid(), CarId = car.Id, MinDays = 11, MaxDays = 15, PricePerDay = car.PricePerDay * 0.85M },
            new PricingTier { Id = Guid.NewGuid(), CarId = car.Id, MinDays = 16, MaxDays = 20, PricePerDay = car.PricePerDay * 0.8M },
            new PricingTier { Id = Guid.NewGuid(), CarId = car.Id, MinDays = 21, MaxDays = 365, PricePerDay = car.PricePerDay * 0.75M }
        });
            }

            modelBuilder.Entity<PricingTier>().HasData(pricingTiers);

            // Seed Features
            var features = new List<CarFeature>();
            foreach (var car in cars)
            {
                features.AddRange(new[]
                {
            new CarFeature { Id = Guid.NewGuid(), Name = "A/C", CarId = car.Id },
            new CarFeature { Id = Guid.NewGuid(), Name = "Airbags", CarId = car.Id },
            new CarFeature { Id = Guid.NewGuid(), Name = "Central Locking", CarId = car.Id },
            new CarFeature { Id = Guid.NewGuid(), Name = "Electric Windows", CarId = car.Id }
        });
            }

            modelBuilder.Entity<CarFeature>().HasData(features);
        }

        public DbSet<Car>? Car { get; set; }
        public DbSet<Reservation>? Reservation { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Report>? Report { get; set; }
        public DbSet<Status>? Status { get; set; }
        public DbSet<Payment>? Payment { get; set; }
        public DbSet<PricingTier>? PricingTier { get; set; }
        public DbSet<CarClass>? CarClass { get; set; }
        public DbSet<CarFeature>? CarFeature { get; set; }

    }
}
