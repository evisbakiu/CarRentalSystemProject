using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Models
{
    public class Role : IdentityRole
    {
        public string? Code { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
