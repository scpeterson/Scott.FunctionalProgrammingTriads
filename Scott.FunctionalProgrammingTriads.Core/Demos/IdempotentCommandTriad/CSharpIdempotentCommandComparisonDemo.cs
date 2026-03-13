using Scott.FunctionalProgrammingTriads.Core.Demos.Shared;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.IdempotentCommandTriad;

public class CSharpIdempotentCommandComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public CSharpIdempotentCommandComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public CSharpIdempotentCommandComparisonDemo(IOutput output)
    {
        _output = output;
    }

    public const string DemoKey = "csharp-idempotent-command";

    public string Key => DemoKey;
    public string Category => "csharp";
    public IReadOnlyCollection<string> Tags => ["fp", "csharp", "comparison", "idempotency", "triad"];
    public string Description => "Plain C# idempotency pipeline that returns updated command state instead of mutating inline.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var env = new InMemoryFunctionalDemoEnvironment();
            var commandId = IdempotentCommandRules.NormalizeCommandId(name);

            if (!IdempotentCommandRules.TryParseAmount(number, out var amount, out var error))
            {
                _output.WriteLine($"Failed: {error}");
                return;
            }

            var handled = IdempotentCommandRules.HandleCSharp(env.SeedProcessedCommandIds, commandId);
            var outcome = handled.IsDuplicate ? "Duplicate ignored" : "Processed";
            _output.WriteLine($"{outcome}: {commandId} ({amount:0.00})");
        }, "C# Idempotent Command Comparison");
}
