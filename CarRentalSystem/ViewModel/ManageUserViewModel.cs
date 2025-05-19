namespace CarRentalSystem.ViewModel
{
    public class ManageUserViewModel
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
