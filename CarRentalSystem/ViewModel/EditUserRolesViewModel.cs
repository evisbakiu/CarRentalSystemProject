using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.ViewModel
{
    public class EditUserViewModel
    {
        public string? UserId { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
        [BindProperty]
        public List<RoleSelectionViewModel> Roles { get; set; } = new();
    }
}
