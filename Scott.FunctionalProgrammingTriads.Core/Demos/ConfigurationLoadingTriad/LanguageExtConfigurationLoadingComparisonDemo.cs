using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad;

public class LanguageExtConfigurationLoadingComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtConfigurationLoadingComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtConfigurationLoadingComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-startup-config-loading-comparison";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "configuration", "loading", "startup"];
    public string Description => "LanguageExt validation pipeline that loads required startup settings with alias support and accumulated errors.";

    public DemoExecutionResult Run(string? name, string? number) =>
        FunctionalDemoOutput.Render(
            _output,
            "LanguageExt Configuration Loading Comparison",
            ConfigurationLoadingRules.ExecuteLanguageExtPipeline(name),
            (output, config) =>
            {
                output.WriteLine("Result: configuration loaded.");
                output.WriteLine(ConfigurationLoadingRules.FormatSummary(config));
            });
}
