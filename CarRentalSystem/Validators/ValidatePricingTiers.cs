using CarRentalSystem.Constants;
using CarRentalSystem.Models;

namespace CarRentalSystem.Validators
{
    public static class ValidatePricingTiers
    {
        public static bool Value(List<PricingTier> pricingTiers)
        {
            foreach (var range in PricingRanges.Ranges)
            {
                if (!pricingTiers.Any(t => t.MinDays == range.Min && t.MaxDays == range.Max))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
