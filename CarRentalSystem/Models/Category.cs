namespace CarRentalSystem.Models
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}
