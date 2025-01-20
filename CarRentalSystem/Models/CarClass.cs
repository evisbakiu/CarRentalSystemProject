namespace CarRentalSystem.Models
{
    public class CarClass : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }

}
