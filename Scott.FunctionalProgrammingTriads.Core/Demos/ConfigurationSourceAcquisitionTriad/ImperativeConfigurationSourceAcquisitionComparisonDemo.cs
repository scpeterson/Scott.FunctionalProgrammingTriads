using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationSourceAcquisitionTriad;

public class ImperativeConfigurationSourceAcquisitionComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public ImperativeConfigurationSourceAcquisitionComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public ImperativeConfigurationSourceAcquisitionComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "imperative-startup-config-source-comparison";

    public string Key => DemoKey;
    public string Category => "imperative";
    public IReadOnlyCollection<string> Tags => ["imperative", "comparison", "configuration", "source", "startup"];
    public string Description => "Inline fail-fast acquisition of startup settings from external-style environment keys.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationSourceAcquisitionRules.ExecuteImperative(name);
            if (result.IsSuccess && result.Source is not null)
            {
                _output.WriteLine("Result: external startup source acquired.");
                _output.WriteLine(ConfigurationSourceAcquisitionRules.FormatSummary(result.Source));
            }
            else
            {
                _output.WriteLine($"Failed: {string.Join(" ", result.Errors)}");
            }
        }, "Imperative Configuration Source Acquisition Comparison");
}
