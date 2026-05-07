using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationTypedDecodingTriad;

public class CSharpConfigurationTypedDecodingComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public CSharpConfigurationTypedDecodingComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public CSharpConfigurationTypedDecodingComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "csharp-startup-config-decoding-comparison";

    public string Key => DemoKey;
    public string Category => "csharp";
    public IReadOnlyCollection<string> Tags => ["csharp", "comparison", "configuration", "decoding", "startup"];
    public string Description => "Composed C# decoding pipeline that accumulates typed-conversion errors before startup validation.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationTypedDecodingRules.ExecuteCSharpPipeline(name);
            if (result.IsSuccess && result.Config is not null)
            {
                _output.WriteLine("Result: startup config decoded.");
                _output.WriteLine(ConfigurationTypedDecodingRules.FormatSummary(result.Config));
            }
            else
            {
                _output.WriteLine($"Failed: {string.Join(" ", result.Errors)}");
            }
        }, "C# Configuration Typed Decoding Comparison");
}
