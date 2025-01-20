public class ReservationViewModel
{
    public Guid CarId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string CarName { get; set; }
    public string CarClass { get; set; }
    public string FuelType { get; set; }
    public string Gearbox { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal TotalCost { get; set; }
    public DateTime PickUpDateTime { get; set; }
    public DateTime ReturnDateTime { get; set; }
}
