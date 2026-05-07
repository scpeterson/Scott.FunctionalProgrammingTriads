using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using static Scott.FunctionalProgrammingTriads.Core.OutputUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad;

public class ImperativeConfigurationLoadingComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public ImperativeConfigurationLoadingComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public ImperativeConfigurationLoadingComparisonDemo(IOutput output)
    {
        _output = output;
    }

    public const string DemoKey = "imperative-startup-config-loading-comparison";

    public string Key => DemoKey;
    public string Category => "imperative";
    public IReadOnlyCollection<string> Tags => ["imperative", "comparison", "configuration", "loading", "startup"];
    public string Description => "Fail-fast configuration loading that stops on the first missing required setting.";

    public DemoExecutionResult Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var result = ConfigurationLoadingRules.ExecuteImperative(name);

            if (result.IsSuccess && result.Config is not null)
            {
                _output.WriteLine("Result: configuration loaded.");
                _output.WriteLine(ConfigurationLoadingRules.FormatSummary(result.Config));
            }
            else
            {
                _output.WriteLine($"Failed: {string.Join(" ", result.Errors)}");
            }
        }, "Imperative Configuration Loading Comparison");
}
