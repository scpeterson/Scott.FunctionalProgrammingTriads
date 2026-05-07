using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.ConfigurationLoadingTriad;

public class ConfigurationLoadingTriadShould
{
    [Fact]
    public void RunAllConfigurationLoadingVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        IDemo[] demos =
        [
            new ImperativeConfigurationLoadingComparisonDemo(output),
            new CSharpConfigurationLoadingComparisonDemo(output),
            new LanguageExtConfigurationLoadingComparisonDemo(output)
        ];

        foreach (var demo in demos)
        {
            demo.Run("success", null).ShouldBeRight();
        }
    }

    [Fact]
    public void ImperativeLoadingStopsAtTheFirstMissingSetting()
    {
        var result = ConfigurationLoadingRules.LoadImperative(MissingSource);
        Assert.False(result.IsSuccess);
        Assert.Equal(["APP_NAME is required."], result.Errors);
    }

    [Fact]
    public void CSharpLoadingAccumulatesMissingSettings()
    {
        var result = ConfigurationLoadingRules.LoadCSharp(MissingSource);
        Assert.False(result.IsSuccess);
        Assert.Equal(
            [
                "APP_NAME is required.",
                "DATABASE_URL is required.",
                "LOG_LEVEL is required."
            ],
            result.Errors);
    }

    [Fact]
    public void LanguageExtLoadingSupportsAliasesAndNormalizesValues()
    {
        var result = ConfigurationLoadingRules.ExecuteLanguageExtPipeline("broken");
        result.ShouldBeRight();
        var config = result.RightToSeq().Head();
        Assert.Equal("worker-service", config.AppName);
        Assert.Equal("dev", config.EnvironmentName);
        Assert.Equal("postgres://localhost/worker", config.DatabaseUrl);
        Assert.Equal("9090", config.PortText);
        Assert.Equal("info", config.LogLevel);
    }

    private static readonly ConfigurationLoadingRules.StartupSettingSource MissingSource =
        new(new Dictionary<string, string>
        {
            ["APP_NAME"] = "  ",
            ["ENVIRONMENT"] = " staging ",
            ["DB_URL"] = "",
            ["APP_PORT"] = "7000"
        });
}
