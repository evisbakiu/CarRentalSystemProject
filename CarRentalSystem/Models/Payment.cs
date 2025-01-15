namespace CarRentalSystem.Models
{
    public class Payment : BaseEntity
    {
        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }

    }
}
