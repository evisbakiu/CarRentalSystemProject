using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Models
{
    public class User : IdentityUser
    {
        public string? Username { get; set; }


        public ICollection<Role>? Roles { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}
