using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.MonadicFunctions;

public static class PureFunctions
{
    // Twice :: int -> int
    public static int Twice(int value) => value * 2;

    // Twice :: decimal -> decimal
    public static decimal Twice(decimal value) => value * 2m;

    // SafeDivide :: decimal -> decimal -> Option<decimal>
    public static Option<decimal> SafeDivide(decimal numerator, decimal denominator) =>
        denominator == 0m ? None : Some(numerator / denominator);
}
