using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Models
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }
        public string? Code { get; set; }
    }
}
