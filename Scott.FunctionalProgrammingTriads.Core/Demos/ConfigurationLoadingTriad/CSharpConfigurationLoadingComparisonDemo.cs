using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad;

public class CSharpConfigurationLoadingComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public CSharpConfigurationLoadingComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public CSharpConfigurationLoadingComparisonDemo(IOutput output)
    {
        _output = output;
    }

    public const string DemoKey = "csharp-startup-config-loading-comparison";

    public string Key => DemoKey;
    public string Category => "csharp";
    public IReadOnlyCollection<string> Tags => ["csharp", "comparison", "configuration", "loading", "startup"];
    public string Description => "Composed C# loading pipeline that trims aliases and accumulates missing-setting errors.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationLoadingRules.ExecuteCSharpPipeline(name);

            if (result.IsSuccess && result.Config is not null)
            {
                _output.WriteLine("Result: configuration loaded.");
                _output.WriteLine(ConfigurationLoadingRules.FormatSummary(result.Config));
            }
            else
            {
                _output.WriteLine($"Failed: {string.Join(" ", result.Errors)}");
            }
        }, "C# Configuration Loading Comparison");
}
