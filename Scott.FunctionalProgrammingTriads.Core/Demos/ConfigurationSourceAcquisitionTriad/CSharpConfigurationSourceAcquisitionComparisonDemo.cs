using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationSourceAcquisitionTriad;

public class CSharpConfigurationSourceAcquisitionComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public CSharpConfigurationSourceAcquisitionComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public CSharpConfigurationSourceAcquisitionComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "csharp-startup-config-source-comparison";

    public string Key => DemoKey;
    public string Category => "csharp";
    public IReadOnlyCollection<string> Tags => ["csharp", "comparison", "configuration", "source", "startup"];
    public string Description => "Composed C# acquisition pipeline that gathers canonical startup settings from external-style source keys while accumulating missing-source errors.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationSourceAcquisitionRules.ExecuteCSharpPipeline(name);
            if (result.IsSuccess && result.Source is not null)
            {
                _output.WriteLine("Result: external startup source acquired.");
                _output.WriteLine(ConfigurationSourceAcquisitionRules.FormatSummary(result.Source));
            }
            else
            {
                _output.WriteLine($"Failed: {string.Join(" ", result.Errors)}");
            }
        }, "C# Configuration Source Acquisition Comparison");
}
