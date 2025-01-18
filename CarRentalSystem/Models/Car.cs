namespace CarRentalSystem.Models
{
    public class Car : BaseEntity
    {
        public string? Name { get; set; }
        public string? LicensePlate { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImagePath { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
