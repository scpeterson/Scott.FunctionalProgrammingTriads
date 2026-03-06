using FluentAssertions;
using Scott.FizzBuzz.Core.Demos;
using Scott.FizzBuzz.Core.Demos.AsyncEffTriad;
using Scott.FizzBuzz.Core.Interfaces;

namespace Scott.FizzBuzz.Core.Tests.Demos;

public class NegativeDemoPathsShould
{
    [Fact]
    public void ThrowFormatExceptionForImperativeAsyncWorkflowWhenNumberIsInvalid()
    {
        var demo = new ImperativeAsyncWorkflowDemo(new RecordingOutput());

        Action act = () => _ = demo.Run("Scott", "bad");

        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void EmitLeftOutputForStrictJsonFailureScenarios()
    {
        var output = new RecordingOutput();
        var demo = new FpJsonStrictValidationDemo(output);

        _ = demo.Run(null, null);

        output.Messages.Should().Contain(message => message.Contains("Scenario: Unknown field", StringComparison.Ordinal));
        output.Messages.Should().Contain(message => message.Contains("Scenario: Duplicate property", StringComparison.Ordinal));
        output.Messages.Should().Contain(message => message.Contains("Left:", StringComparison.Ordinal));
    }

    [Fact]
    public void EmitLeftOutputForExtensionMembersInvalidInput()
    {
        var output = new RecordingOutput();
        var demo = new FpExtensionMembersTypeclassesDemo(output);

        _ = demo.Run(null, "1");

        output.Messages.Should().Contain(message =>
            message.Contains("Input '1': Value 2 is not a multiple of four.", StringComparison.Ordinal));
        output.Messages.Should().Contain(message =>
            message.Contains("Input 'abc': Could not parse 'abc' as an integer.", StringComparison.Ordinal));
    }

    private sealed class RecordingOutput : IOutput
    {
        public List<string> Messages { get; } = [];
        public void WriteLine(string message) => Messages.Add(message);
    }
}
