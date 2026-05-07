using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationSourceAcquisitionTriad;

public class LanguageExtConfigurationSourceAcquisitionComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtConfigurationSourceAcquisitionComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtConfigurationSourceAcquisitionComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-startup-config-source-comparison";

    public string Key => DemoKey;
    public string Category => "langext";
    public IReadOnlyCollection<string> Tags => ["langext", "comparison", "configuration", "source", "startup", "validation"];
    public string Description => "LanguageExt Validation pipeline that acquires canonical startup settings from external-style source keys with accumulated missing-source errors.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationSourceAcquisitionRules.ExecuteLanguageExtPipeline(name);
            result.Match(
                Right: source =>
                {
                    _output.WriteLine("Result: external startup source acquired.");
                    _output.WriteLine(ConfigurationSourceAcquisitionRules.FormatSummary(source));
                },
                Left: error => _output.WriteLine($"Failed: {error}"));
        }, "LanguageExt Configuration Source Acquisition Comparison");
}
