namespace CarRentalSystem.Models
{
    public class PricingTier : BaseEntity
    {
        public Guid CarId { get; set; }
        public Car? Car { get; set; }

        public int MinDays { get; set; }
        public int MaxDays { get; set; }
        public decimal PricePerDay { get; set; }
    }

}
