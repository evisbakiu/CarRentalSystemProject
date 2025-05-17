using System.Formats.Asn1;

namespace CarRentalSystem.Models
{
    public class Car : BaseEntity
    {
        public string? Name { get; set; }
        public string? LicensePlate { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? ImagePath { get; set; }
        public string Gearbox { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<PricingTier>? PricingTiers { get; set; }


        public Guid ClassId { get; set; } 
        public CarClass? Class { get; set; } 

        public string? FuelType { get; set; } 
        public decimal FuelUsage { get; set; } 

        public ICollection<CarFeature>? Features { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }

}
