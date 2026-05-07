using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ResourceCleanupTriad;

public class CSharpResourceCleanupComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public CSharpResourceCleanupComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public CSharpResourceCleanupComparisonDemo(IOutput output)
    {
        _output = output;
    }

    public const string DemoKey = "csharp-resource-cleanup-comparison";

    public string Key => DemoKey;
    public string Category => "csharp";
    public IReadOnlyCollection<string> Tags => ["fp", "csharp", "comparison", "resource", "cleanup", "lifecycle"];
    public string Description => "Plain C# composition that centralizes resource scope while keeping cleanup explicit and testable.";

    public DemoExecutionResult Run(string? name, string? number)
    {
        if (!ResourceCleanupRules.TryResolveScenario(name, out var scenario, out var error))
        {
            return DemoExecutionResult.Failure(error ?? "Unknown scenario.");
        }

        var result = ResourceCleanupRules.ExecuteCSharpPipeline(scenario!);
        var spacingResult = ExecuteWithSpacing(_output, () => Render(result), "C# Resource Cleanup Comparison");

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
