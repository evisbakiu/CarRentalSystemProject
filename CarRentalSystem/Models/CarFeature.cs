namespace CarRentalSystem.Models
{
    public class CarFeature : BaseEntity
    {
        public new Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
    }

}
