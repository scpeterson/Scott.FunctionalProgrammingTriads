namespace Scott.FizzBuzz.Core.Demos.ReaderMonadTriad;

public static class ReaderMonadSampleData
{
    private static readonly IReadOnlyDictionary<string, ReaderPricingContext> Profiles =
        new Dictionary<string, ReaderPricingContext>(StringComparer.OrdinalIgnoreCase)
        {
            ["standard"] = new ReaderPricingContext
            {
                ProfileName = "Standard",
                TaxRate = 0.07m,
                ServiceFee = 2.50m,
                Currency = "USD"
            },
            ["vip"] = new ReaderPricingContext
            {
                ProfileName = "VIP",
                TaxRate = 0.04m,
                ServiceFee = 0.50m,
                Currency = "USD"
            },
            ["intl"] = new ReaderPricingContext
            {
                ProfileName = "International",
                TaxRate = 0.10m,
                ServiceFee = 3.00m,
                Currency = "EUR"
            },
            ["scott"] = new ReaderPricingContext
            {
                ProfileName = "Scott",
                TaxRate = 0.06m,
                ServiceFee = 1.00m,
                Currency = "USD"
            }
        };

    public static ReaderPricingContext? ResolveProfile(string? key)
    {
        var normalized = string.IsNullOrWhiteSpace(key) ? "standard" : key.Trim();
        return Profiles.TryGetValue(normalized, out var context) ? context : null;
    }
}
