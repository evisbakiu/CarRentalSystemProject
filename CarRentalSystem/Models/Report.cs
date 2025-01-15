namespace CarRentalSystem.Models
{
    public class Report : BaseEntity
    {
        public DateTime DateGenerated { get; set; }
        public string? ReportDetails { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
