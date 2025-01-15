using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Models
{
    public class Role : IdentityRole
    {
        public string? Code { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
