using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static Scott.FizzBuzz.Core.OutputUtilities;

namespace Scott.FizzBuzz.Core.Demos.AsyncEffTriad;

public class ImperativeAsyncWorkflowDemo : IDemo
{
    private readonly IOutput _output;

    public ImperativeAsyncWorkflowDemo() : this(new ConsoleOutput())
    {
    }

    public ImperativeAsyncWorkflowDemo(IOutput output)
    {
        _output = output;
    }

    public string Key => "imperative-async-workflow";
    public string Category => "imperative";
    public IReadOnlyCollection<string> Tags => ["imperative", "comparison", "async"];

    public Either<string, Unit> Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var value = int.Parse(number ?? "10");
            var first = DoubleAsync(value).GetAwaiter().GetResult();
            var second = AddTenAsync(first).GetAwaiter().GetResult();
            _output.WriteLine($"Result: {second}");
        }, "Imperative Async Workflow");

    private static Task<int> DoubleAsync(int value) => Task.FromResult(value * 2);
    private static Task<int> AddTenAsync(int value) => Task.FromResult(value + 10);
}
