using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;
using static Scott.FizzBuzz.Core.OutputUtilities;

namespace Scott.FizzBuzz.Core.Demos.AsyncEffTriad;

public class CSharpAsyncCompositionDemo : IDemo
{
    private readonly IOutput _output;

    public CSharpAsyncCompositionDemo() : this(new ConsoleOutput())
    {
    }

    public CSharpAsyncCompositionDemo(IOutput output)
    {
        _output = output;
    }

    public string Key => "csharp-async-composition";
    public string Category => "csharp";
    public IReadOnlyCollection<string> Tags => ["fp", "csharp", "comparison", "async"];

    public Either<string, Unit> Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ComposeAsync(number ?? "10").GetAwaiter().GetResult();
            result.Match(
                Right: value => _output.WriteLine($"Result: {value}"),
                Left: error => _output.WriteLine(error));
        }, "C# Async Composition");

    private static async Task<Either<string, int>> ComposeAsync(string input)
    {
        if (!int.TryParse(input, out var parsed))
            return Left<string, int>("Input must be an integer.");

        var doubled = await Task.FromResult(parsed * 2);
        var finalValue = await Task.FromResult(doubled + 10);
        return Right<string, int>(finalValue);
    }
}
