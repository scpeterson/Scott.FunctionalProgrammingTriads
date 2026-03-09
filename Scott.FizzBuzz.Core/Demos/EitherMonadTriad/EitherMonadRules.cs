using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.EitherMonadTriad;

public static class EitherMonadRules
{
    public static Either<string, decimal> ParseAmount(string? input) =>
        decimal.TryParse(input, out var amount)
            ? Right<string, decimal>(amount)
            : Left<string, decimal>("Amount must be a valid decimal value.");

    public static Either<string, decimal> ValidateAmountRange(decimal amount) =>
        amount is >= 1m and <= 1000m
            ? Right<string, decimal>(amount)
            : Left<string, decimal>("Amount must be between 1 and 1000.");

    public static Either<string, EitherDiscountCode> ParseDiscountCode(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Right<string, EitherDiscountCode>(EitherDiscountCode.None);
        }

        var normalized = code.Trim().ToLowerInvariant();
        return normalized switch
        {
            "vip" => Right<string, EitherDiscountCode>(EitherDiscountCode.Vip),
            "student" => Right<string, EitherDiscountCode>(EitherDiscountCode.Student),
            "employee" => Right<string, EitherDiscountCode>(EitherDiscountCode.Employee),
            "scott" => Right<string, EitherDiscountCode>(EitherDiscountCode.None),
            _ => Left<string, EitherDiscountCode>("Unknown discount code. Use vip, student, or employee.")
        };
    }

    public static decimal DiscountFactor(EitherDiscountCode code) =>
        code switch
        {
            EitherDiscountCode.None => 1.00m,
            EitherDiscountCode.Vip => 0.80m,
            EitherDiscountCode.Student => 0.90m,
            EitherDiscountCode.Employee => 0.75m,
            _ => 1.00m
        };

    public static Either<string, decimal> EnsureMinimumCharge(decimal amount) =>
        amount >= 1m
            ? Right<string, decimal>(amount)
            : Left<string, decimal>("Final amount is below the minimum charge.");
}
