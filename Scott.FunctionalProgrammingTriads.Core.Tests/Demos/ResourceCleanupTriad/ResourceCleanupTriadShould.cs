using Scott.FunctionalProgrammingTriads.Core.Demos.ResourceCleanupTriad;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.ResourceCleanupTriad;

public class ResourceCleanupTriadShould
{
    [Fact]
    public void RunAllResourceCleanupTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        IDemo[] demos =
        [
            new ImperativeResourceCleanupComparisonDemo(output),
            new CSharpResourceCleanupComparisonDemo(output),
            new LanguageExtResourceCleanupComparisonDemo(output)
        ];

        foreach (var demo in demos)
        {
            demo.Run("success", null).ShouldBeRight();
        }
    }

    [Fact]
    public void RunAllResourceCleanupTriadVariantsForFailurePath()
    {
        var output = new NullOutputSink();

        IDemo[] demos =
        [
            new ImperativeResourceCleanupComparisonDemo(output),
            new CSharpResourceCleanupComparisonDemo(output),
            new LanguageExtResourceCleanupComparisonDemo(output)
        ];

        foreach (var demo in demos)
        {
            demo.Run("fail", null).ShouldBeLeft();
        }
    }

    [Fact]
    public void GuaranteeReleaseInAllVariantsWhenFailureOccurs()
    {
        Assert.Equal(ExpectedFailureTrace, ResourceCleanupRules.ExecuteImperative(FailureScenario).Trace);
        Assert.Equal(ExpectedFailureTrace, ResourceCleanupRules.ExecuteCSharpPipeline(FailureScenario).Trace);
        Assert.Equal(ExpectedFailureTrace, ResourceCleanupRules.ExecuteLanguageExtPipeline(FailureScenario).Trace);
    }

    [Fact]
    public void ProduceDeterministicSuccessTraceInAllVariants()
    {
        Assert.Equal(ExpectedSuccessTrace, ResourceCleanupRules.ExecuteImperative(SuccessScenario).Trace);
        Assert.Equal(ExpectedSuccessTrace, ResourceCleanupRules.ExecuteCSharpPipeline(SuccessScenario).Trace);
        Assert.Equal(ExpectedSuccessTrace, ResourceCleanupRules.ExecuteLanguageExtPipeline(SuccessScenario).Trace);
    }

    private static readonly ResourceCleanupRules.CleanupScenario SuccessScenario = new("success", ShouldFail: false);
    private static readonly ResourceCleanupRules.CleanupScenario FailureScenario = new("fail", ShouldFail: true);

    private static readonly string[] ExpectedSuccessTrace =
    [
        "acquire: audit-resource",
        "write: begin",
        "write: complete",
        "release: audit-resource"
    ];

    private static readonly string[] ExpectedFailureTrace =
    [
        "acquire: audit-resource",
        "write: begin",
        "release: audit-resource"
    ];
}
