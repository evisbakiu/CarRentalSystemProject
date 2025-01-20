using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Models
{
    public class User : IdentityUser
    {
        public int Age { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }


        public ICollection<Role>? Roles { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}
