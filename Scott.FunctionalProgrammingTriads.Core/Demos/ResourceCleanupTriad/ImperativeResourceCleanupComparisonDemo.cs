using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ResourceCleanupTriad;

public class ImperativeResourceCleanupComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public ImperativeResourceCleanupComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public ImperativeResourceCleanupComparisonDemo(IOutput output)
    {
        _output = output;
    }

    public const string DemoKey = "imperative-resource-cleanup-comparison";

    public string Key => DemoKey;
    public string Category => "imperative";
    public IReadOnlyCollection<string> Tags => ["imperative", "comparison", "resource", "cleanup", "lifecycle"];
    public string Description => "Manual acquire/use/release orchestration with try/finally and inline failure handling.";

    public DemoExecutionResult Run(string? name, string? number)
    {
        if (!ResourceCleanupRules.TryResolveScenario(name, out var scenario, out var error))
        {
            return DemoExecutionResult.Failure(error ?? "Unknown scenario.");
        }

        var result = ResourceCleanupRules.ExecuteImperative(scenario!);
        var spacingResult = ExecuteWithSpacing(_output, () => Render(result), "Imperative Resource Cleanup Comparison");

        if (!spacingResult.IsSuccess)
        {
            return spacingResult;
        }

        return result.Success ? DemoExecutionResult.Success() : DemoExecutionResult.Failure(result.Message);
    }

    private void Render(ResourceCleanupRules.ResourceCleanupResult result)
    {
        _output.WriteLine($"Result: {ResourceCleanupRules.FormatSummary(result)}");
        _output.WriteLine($"Trace: {ResourceCleanupRules.FormatTrace(result.Trace)}");
    }
}
