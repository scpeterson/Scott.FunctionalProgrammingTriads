namespace Scott.FizzBuzz.Core.Demos.ReaderMonadTriad;

public sealed class ReaderPricingContext
{
    public required string ProfileName { get; init; }
    public required decimal TaxRate { get; init; }
    public required decimal ServiceFee { get; init; }
    public required string Currency { get; init; }
}
