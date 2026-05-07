using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationTypedDecodingTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.ConfigurationTypedDecodingTriad;

public class ConfigurationTypedDecodingTriadShould
{
    [Fact]
    public void RunAllConfigurationTypedDecodingVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        IDemo[] demos =
        [
            new ImperativeConfigurationTypedDecodingComparisonDemo(output),
            new CSharpConfigurationTypedDecodingComparisonDemo(output),
            new LanguageExtConfigurationTypedDecodingComparisonDemo(output)
        ];

        foreach (var demo in demos)
        {
            demo.Run("success", null).ShouldBeRight();
        }
    }

    [Fact]
    public void ImperativeDecodingStopsAtTheFirstInvalidTypedValue()
    {
        var result = ConfigurationTypedDecodingRules.DecodeImperative(InvalidRawConfig);
        Assert.False(result.IsSuccess);
        Assert.Equal(["Environment must be development, staging, or production."], result.Errors);
    }

    [Fact]
    public void CSharpDecodingAccumulatesTypedConversionErrors()
    {
        var result = ConfigurationTypedDecodingRules.DecodeCSharp(InvalidRawConfig);
        Assert.False(result.IsSuccess);
        Assert.Equal(
            [
                "Environment must be development, staging, or production.",
                "Port must be an integer.",
                "Log level must be debug, info, warn, or error."
            ],
            result.Errors);
    }

    [Fact]
    public void LanguageExtDecodingSupportsAliasesLikeDevAndProd()
    {
        var result = ConfigurationTypedDecodingRules.ExecuteLanguageExtPipeline("alias");
        result.ShouldBeRight();
        var config = result.RightToSeq().Head();
        Assert.Equal("worker-service", config.AppName);
        Assert.Equal(ConfigurationTypedDecodingRules.StartupEnvironmentName.Development, config.Environment);
        Assert.Equal(9090, config.Port);
        Assert.Equal(ConfigurationTypedDecodingRules.StartupLogLevel.Info, config.LogLevel);
    }

    private static readonly Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad.ConfigurationLoadingRules.RawStartupConfig InvalidRawConfig =
        new(
            "triad-service",
            "qa",
            "postgres://db.example.com/triads",
            "eighty",
            "loud");
}
