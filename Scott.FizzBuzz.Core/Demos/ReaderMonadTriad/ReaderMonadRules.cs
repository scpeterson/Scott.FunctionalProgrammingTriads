using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.ReaderMonadTriad;

public static class ReaderMonadRules
{
    public static Either<string, decimal> ParseSubtotal(string? input) =>
        decimal.TryParse(input, out var subtotal)
            ? subtotal is >= 1m and <= 10000m
                ? Right<string, decimal>(subtotal)
                : Left<string, decimal>("Subtotal must be between 1 and 10000.")
            : Left<string, decimal>("Subtotal must be a valid decimal value.");

    public static Either<string, ReaderPricingContext> ResolveContext(string? profile) =>
        Optional(ReaderMonadSampleData.ResolveProfile(profile))
            .ToEither("Unknown pricing profile. Use standard, vip, or intl.");

    public static decimal ApplyTax(decimal subtotal, ReaderPricingContext context) =>
        subtotal * (1m + context.TaxRate);

    public static decimal AddFee(decimal taxed, ReaderPricingContext context) =>
        taxed + context.ServiceFee;

    public static string FormatTotal(decimal total, ReaderPricingContext context) =>
        $"{context.ProfileName}: total = {total:0.00} {context.Currency}";
}
