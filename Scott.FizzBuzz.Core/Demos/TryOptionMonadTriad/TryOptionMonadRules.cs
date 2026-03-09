using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.TryOptionMonadTriad;

public static class TryOptionMonadRules
{
    private static readonly IReadOnlyDictionary<int, decimal> Values = new Dictionary<int, decimal>
    {
        [1] = 10.5m,
        [2] = 22.0m,
        [21] = 99.9m
    };

    public static Either<string, int> ParseId(string? number) =>
        int.TryParse(number, out var id)
            ? Right<string, int>(id)
            : Left<string, int>("Id must be numeric.");

    public static decimal? LookupNullable(int id)
    {
        if (id == 13)
        {
            throw new InvalidOperationException("Repository unavailable for id 13.");
        }

        return Values.TryGetValue(id, out var value) ? value : null;
    }

    public static Option<decimal> LookupOption(int id)
    {
        if (id == 13)
        {
            throw new InvalidOperationException("Repository unavailable for id 13.");
        }

        return Values.TryGetValue(id, out var value) ? Some(value) : None;
    }

    public static TryOption<decimal> LookupTryOption(int id) =>
        TryOption(() => LookupOption(id));
}
