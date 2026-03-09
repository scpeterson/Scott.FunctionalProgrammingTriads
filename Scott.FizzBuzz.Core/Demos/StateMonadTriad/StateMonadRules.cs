using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.StateMonadTriad;

public static class StateMonadRules
{
    private static readonly IReadOnlyDictionary<string, Seq<string>> Plans =
        new Dictionary<string, Seq<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["standard"] = Seq("add", "boost", "add"),
            ["aggressive"] = Seq("boost", "add", "add", "penalty", "add"),
            ["defensive"] = Seq("add", "penalty", "add"),
            ["scott"] = Seq("add", "boost", "add")
        };

    public static Either<string, int> ParseStep(string? input) =>
        int.TryParse(input, out var parsed) && parsed is >= 1 and <= 100
            ? Right<string, int>(parsed)
            : Left<string, int>("Step must be a number between 1 and 100.");

    public static Either<string, Seq<string>> ResolvePlan(string? name)
    {
        var key = string.IsNullOrWhiteSpace(name) ? "standard" : name.Trim();
        return Plans.TryGetValue(key, out var plan)
            ? Right<string, Seq<string>>(plan)
            : Left<string, Seq<string>>("Unknown plan. Use standard, aggressive, or defensive.");
    }

    public static StateGame Apply(string operation, int step, StateGame state) =>
        operation switch
        {
            "add" => state with { Score = state.Score + (step * state.Multiplier) },
            "boost" => state with { Multiplier = state.Multiplier + 1 },
            "penalty" => state with { Score = state.Score - 3, Penalties = state.Penalties + 1 },
            _ => state
        };
}
