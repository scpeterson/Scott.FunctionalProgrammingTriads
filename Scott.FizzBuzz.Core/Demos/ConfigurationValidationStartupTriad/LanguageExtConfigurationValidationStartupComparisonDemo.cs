using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.ConfigurationValidationStartupTriad;

public class LanguageExtConfigurationValidationStartupComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtConfigurationValidationStartupComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtConfigurationValidationStartupComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-startup-config-validation-comparison";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "configuration", "validation", "startup"];
    public string Description => "Applicative startup config validation with accumulated errors and pure result values.";

    public Either<string, Unit> Run(string? name, string? number) =>
        FunctionalDemoOutput.Render(
            _output,
            "LanguageExt Startup Configuration Validation Comparison",
            ConfigurationValidationStartupRules.ExecuteLanguageExtPipeline(name, number),
            (output, config) =>
            {
                output.WriteLine("Result: configuration valid.");
                output.WriteLine(ConfigurationValidationStartupRules.FormatSummary(config));
            });
}
