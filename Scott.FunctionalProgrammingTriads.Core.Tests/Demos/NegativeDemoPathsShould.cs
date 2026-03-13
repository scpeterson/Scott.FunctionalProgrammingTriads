using FluentAssertions;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.AsyncEffTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos;

public class NegativeDemoPathsShould
{
    [Fact]
    public void HandleInvalidNumberInImperativeAsyncWorkflowWithoutThrowing()
    {
        var output = new RecordingOutputSink();
        var demo = new ImperativeAsyncWorkflowDemo(output);

        Action act = () => _ = demo.Run("Scott", "bad");

        act.Should().NotThrow();
        _ = demo.Run("Scott", "bad");
        output.Messages.Should().Contain(message => message.Contains("Failed:", StringComparison.Ordinal));
    }

    [Fact]
    public void HandleInvalidNumberInCSharpAsyncWorkflowWithoutThrowing()
    {
        var output = new RecordingOutputSink();
        var demo = new CSharpAsyncCompositionDemo(output);

        Action act = () => _ = demo.Run("Scott", "bad");

        act.Should().NotThrow();
        _ = demo.Run("Scott", "bad");
        output.Messages.Should().Contain(message => message.Contains("Failed:", StringComparison.Ordinal));
    }

    [Fact]
    public void EmitLeftOutputForStrictJsonFailureScenarios()
    {
        var output = new RecordingOutputSink();
        var demo = new FpJsonStrictValidationDemo(output);

        _ = demo.Run(null, null);

        output.Messages.Should().Contain(message => message.Contains("Scenario: Unknown field", StringComparison.Ordinal));
        output.Messages.Should().Contain(message => message.Contains("Scenario: Duplicate property", StringComparison.Ordinal));
        output.Messages.Should().Contain(message => message.Contains("Left:", StringComparison.Ordinal));
    }

    [Fact]
    public void EmitLeftOutputForExtensionMembersInvalidInput()
    {
        var output = new RecordingOutputSink();
        var demo = new FpExtensionMembersTypeclassesDemo(output);

        _ = demo.Run(null, "1");

        output.Messages.Should().Contain(message =>
            message.Contains("Failed: input '1' -> Value 2 is not a multiple of four.", StringComparison.Ordinal));
        output.Messages.Should().Contain(message =>
            message.Contains("Failed: input 'abc' -> Could not parse 'abc' as an integer.", StringComparison.Ordinal));
    }
}
