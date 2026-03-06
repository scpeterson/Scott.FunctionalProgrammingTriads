namespace Scott.FizzBuzz.Core.MonadicFunctions;

public static class ImpureFunctions
{
    private static string _suffix = string.Empty;

    // Hidden mutable state is an implicit dependency not visible from the function signature.
    public static void SetSuffix(string suffix) => _suffix = suffix;

    // `string -> int` looks pure, but depends on mutable _suffix.
    public static int ClosureLength(string input) => input.Length + _suffix.Length;

    // Throws for divide-by-zero; caller must guard with try/catch.
    public static decimal Divide(decimal numerator, decimal denominator) => numerator / denominator;
}
