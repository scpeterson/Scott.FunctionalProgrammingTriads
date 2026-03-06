using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.AsyncEffTriad;

public class LanguageExtAsyncEffWorkflowDemo : IDemo
{
    public string Key => "langext-eff-async-workflow";
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "async", "eff"];

    public Either<string, Unit> Run(string? name, string? number) =>
        BuildProgram(number)
            .Run()
            .Match(
                Succ: task => task.GetAwaiter().GetResult().Map(_ => unit),
                Fail: error => Left<string, Unit>(error.Message));

    private static Eff<Task<Either<string, int>>> BuildProgram(string? input) =>
        Eff(() => ComposeAsync(input));

    private static async Task<Either<string, int>> ComposeAsync(string? input)
    {
        var validated = ValidateInput(input);
        return await validated.Match(
            Right: async value =>
            {
                var doubled = await Task.FromResult(value * 2);
                var finalValue = await Task.FromResult(doubled + 10);
                return Right<string, int>(finalValue);
            },
            Left: error => Task.FromResult(Left<string, int>(error)));
    }

    private static Either<string, int> ValidateInput(string? input) =>
        !string.IsNullOrWhiteSpace(input) && int.TryParse(input, out var parsed)
            ? Right<string, int>(parsed)
            : Left<string, int>("Input must be a non-empty integer.");
}
