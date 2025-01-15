namespace CarRentalSystem.Models
{
    public class Status : BaseEntity
    {
        public string? Name { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
