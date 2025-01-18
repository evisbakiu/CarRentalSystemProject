namespace CarRentalSystem.Models
{
    public class Reservation : BaseEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
    }
}
