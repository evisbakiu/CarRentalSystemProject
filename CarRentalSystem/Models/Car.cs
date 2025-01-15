namespace CarRentalSystem.Models
{
    public class Car : BaseEntity
    {
        public string? Name { get; set; }
        public string? LicensePlate { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
