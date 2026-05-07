using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationSourceAcquisitionTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.ConfigurationSourceAcquisitionTriad;

public class ConfigurationSourceAcquisitionTriadShould
{
    [Fact]
    public void RunAllConfigurationSourceAcquisitionVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        IDemo[] demos =
        [
            new ImperativeConfigurationSourceAcquisitionComparisonDemo(output),
            new CSharpConfigurationSourceAcquisitionComparisonDemo(output),
            new LanguageExtConfigurationSourceAcquisitionComparisonDemo(output)
        ];

        foreach (var demo in demos)
        {
            demo.Run("success", null).ShouldBeRight();
        }
    }

    [Fact]
    public void ImperativeAcquisitionStopsAtTheFirstMissingExternalSetting()
    {
        var result = ConfigurationSourceAcquisitionRules.AcquireImperative(MissingSource);
        Assert.False(result.IsSuccess);
        Assert.Equal(["TRIADS_APP_NAME is required from the external source."], result.Errors);
    }

    [Fact]
    public void CSharpAcquisitionAccumulatesMissingExternalSettings()
    {
        var result = ConfigurationSourceAcquisitionRules.AcquireCSharp(MissingSource);
        Assert.False(result.IsSuccess);
        Assert.Equal(
            [
                "TRIADS_APP_NAME is required from the external source.",
                "TRIADS_DATABASE_URL is required from the external source.",
                "TRIADS_LOG_LEVEL is required from the external source."
            ],
            result.Errors);
    }

    [Fact]
    public void LanguageExtAcquisitionSupportsExternalAliasesAndCanonicalizesKeys()
    {
        var result = ConfigurationSourceAcquisitionRules.ExecuteLanguageExtPipeline("alias");
        result.ShouldBeRight();
        var source = result.RightToSeq().Head();
        Assert.Equal("worker-service", source.Values["APP_NAME"]);
        Assert.Equal("dev", source.Values["ENVIRONMENT"]);
        Assert.Equal("postgres://localhost/worker", source.Values["DATABASE_URL"]);
        Assert.Equal("9090", source.Values["PORT"]);
        Assert.Equal("info", source.Values["LOG_LEVEL"]);
    }

    private static readonly ConfigurationSourceAcquisitionRules.ExternalSettingSource MissingSource =
        new(new Dictionary<string, string>
        {
            ["TRIADS_APP_NAME"] = "  ",
            ["TRIADS_ENVIRONMENT"] = "staging",
            ["TRIADS_APP_PORT"] = "7000"
        });
}
