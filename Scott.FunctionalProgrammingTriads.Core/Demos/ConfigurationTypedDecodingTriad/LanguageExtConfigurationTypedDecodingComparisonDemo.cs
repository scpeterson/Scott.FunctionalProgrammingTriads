using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationTypedDecodingTriad;

public class LanguageExtConfigurationTypedDecodingComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtConfigurationTypedDecodingComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtConfigurationTypedDecodingComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-startup-config-decoding-comparison";

    public string Key => DemoKey;
    public string Category => "langext";
    public IReadOnlyCollection<string> Tags => ["langext", "comparison", "configuration", "decoding", "startup", "validation"];
    public string Description => "LanguageExt Validation pipeline that decodes typed startup values while accumulating conversion failures.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationTypedDecodingRules.ExecuteLanguageExtPipeline(name);
            result.Match(
                Right: config =>
                {
                    _output.WriteLine("Result: startup config decoded.");
                    _output.WriteLine(ConfigurationTypedDecodingRules.FormatSummary(config));
                },
                Left: error => _output.WriteLine($"Failed: {error}"));
        }, "LanguageExt Configuration Typed Decoding Comparison");
}
