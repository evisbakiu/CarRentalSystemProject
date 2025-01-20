using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.ViewModel
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(1000, ErrorMessage = "Message must be less than 1000 characters.")]
        public string Message { get; set; }
    }
}
