using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ResourceCleanupTriad;

public class LanguageExtResourceCleanupComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtResourceCleanupComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtResourceCleanupComparisonDemo(IOutput output)
    {
        _output = output;
    }

    public const string DemoKey = "langext-resource-cleanup-comparison";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "resource", "cleanup", "lifecycle"];
    public string Description => "LanguageExt Either pipeline wrapped in a guaranteed cleanup boundary for deterministic resource traces.";

    public DemoExecutionResult Run(string? name, string? number)
    {
        if (!ResourceCleanupRules.TryResolveScenario(name, out var scenario, out var error))
        {
            return DemoExecutionResult.Failure(error ?? "Unknown scenario.");
        }

        var result = ResourceCleanupRules.ExecuteLanguageExtPipeline(scenario!);
        var spacingResult = ExecuteWithSpacing(_output, () => Render(result), "LanguageExt Resource Cleanup Comparison");

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
