using CarRentalSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalSystem.Data
{
    public static class SeedData
    {
        public static async Task SeedDataForTheSystem(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. Seed Roles
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Seed Admin User
            var adminEmail = "admin@carrental.com";
            var adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var createAdminResult = await userManager.CreateAsync(newAdmin, adminPassword);

                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }

            // 3. Seed Car Categories
            var categories = new List<Category>
            {
                new Category { Name = "SUV" },
                new Category { Name = "Sedan" },
                new Category { Name = "Convertible" },
                new Category { Name = "Hatchback" },
                new Category { Name = "Coupe" },
                new Category { Name = "Van" },
                new Category { Name = "Pickup Truck" },
                new Category { Name = "Sports Car" },
                new Category { Name = "Luxury Car" },
                new Category { Name = "Electric Car" },
                new Category { Name = "Hybrid Car" },
                new Category { Name = "Minivan" },
                new Category { Name = "Off-Road" },
                new Category { Name = "Compact" },
                new Category { Name = "Crossover" }
            };

            foreach (var category in categories)
            {
                if (!context.Category.Any(c => c.Name == category.Name))
                {
                    context.Category.Add(category);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
