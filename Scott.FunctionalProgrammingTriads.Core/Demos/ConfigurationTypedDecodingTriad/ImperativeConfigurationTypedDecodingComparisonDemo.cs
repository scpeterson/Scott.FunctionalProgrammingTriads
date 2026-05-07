using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationTypedDecodingTriad;

public class ImperativeConfigurationTypedDecodingComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public ImperativeConfigurationTypedDecodingComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public ImperativeConfigurationTypedDecodingComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "imperative-startup-config-decoding-comparison";

    public string Key => DemoKey;
    public string Category => "imperative";
    public IReadOnlyCollection<string> Tags => ["imperative", "comparison", "configuration", "decoding", "startup"];
    public string Description => "Inline fail-fast decoding from raw startup strings into typed environment, port, and log-level values.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationTypedDecodingRules.ExecuteImperative(name);
            if (result.IsSuccess && result.Config is not null)
            {
                _output.WriteLine("Result: startup config decoded.");
                _output.WriteLine(ConfigurationTypedDecodingRules.FormatSummary(result.Config));
            }
            else
            {
                _output.WriteLine($"Failed: {string.Join(" ", result.Errors)}");
            }
        }, "Imperative Configuration Typed Decoding Comparison");
}
