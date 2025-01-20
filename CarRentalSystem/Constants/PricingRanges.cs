namespace CarRentalSystem.Constants
{
    public static class PricingRanges
    {
        public static List<(int Min, int Max)> Ranges => new List<(int, int)>
        {
            (1, 3),
            (4, 10),
            (11, 15),
            (16, 20),
            (21, 365)
        };
    }
}
