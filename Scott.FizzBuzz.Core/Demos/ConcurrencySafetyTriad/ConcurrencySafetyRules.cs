using LanguageExt;
using System.Threading;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.ConcurrencySafetyTriad;

public static class ConcurrencySafetyRules
{
    public sealed record ConcurrencySimulationResult(int InitialBalance, int ExpectedBalance, int FinalBalance)
    {
        public int LostUpdates => Math.Max(0, ExpectedBalance - FinalBalance);
    }

    public static Either<string, int> ParseIterations(string? value) =>
        int.TryParse(value, out var parsed)
            ? parsed is >= 1 and <= 1_000_000
                ? Right<string, int>(parsed)
                : Left<string, int>("Iterations must be between 1 and 1000000.")
            : Left<string, int>("Iterations must be an integer.");

    // Simulates unsafe read-modify-write interleaving for two concurrent writers.
    public static ConcurrencySimulationResult ExecuteImperativeUnsafe(int iterations)
    {
        var balance = 0;

        for (var i = 0; i < iterations; i++)
        {
            var workerARead = balance;
            var workerBRead = balance;

            balance = workerARead + 1;
            balance = workerBRead + 1;
        }

        return new ConcurrencySimulationResult(InitialBalance: 0, ExpectedBalance: iterations * 2, FinalBalance: balance);
    }

    public static ConcurrencySimulationResult ExecuteCSharpAtomic(int iterations)
    {
        var balance = 0;

        for (var i = 0; i < iterations * 2; i++)
        {
            Interlocked.Increment(ref balance);
        }

        return new ConcurrencySimulationResult(InitialBalance: 0, ExpectedBalance: iterations * 2, FinalBalance: balance);
    }

    public static ConcurrencySimulationResult ExecuteLanguageExtPure(int iterations)
    {
        var finalBalance = Range(1, iterations * 2).Fold(0, (state, _) => state + 1);
        return new ConcurrencySimulationResult(InitialBalance: 0, ExpectedBalance: iterations * 2, FinalBalance: finalBalance);
    }

    public static string FormatSummary(ConcurrencySimulationResult result) =>
        $"Expected={result.ExpectedBalance}, Final={result.FinalBalance}, LostUpdates={result.LostUpdates}";
}
