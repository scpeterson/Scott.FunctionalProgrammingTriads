using FluentAssertions;
using Scott.FizzBuzz.Core.Interfaces;

namespace Scott.FizzBuzz.Core.Tests;

public class OutputUtilitiesShould
{
    [Fact]
    public void ExecuteWithSpacingUseStyledOutputWhenAvailable()
    {
        var output = new RecordingStyledOutput();
        var actionInvocations = 0;

        var result = OutputUtilities.ExecuteWithSpacing(
            output,
            () =>
            {
                actionInvocations++;
                output.WriteLine("action-body");
            },
            "DemoMethod");

        result.IsRight.Should().BeTrue();
        actionInvocations.Should().Be(1);
        output.Messages.Should().ContainInOrder(
            "************************************************************",
            "Executing method: DemoMethod",
            "************************************************************",
            "action-body",
            "************************************************************");
        output.Colors.Should().ContainInOrder(
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.Cyan,
            ConsoleColor.Green,
            ConsoleColor.Cyan);
    }

    [Fact]
    public void ExecuteWithSpacingFallbackToPlainOutputWhenNotStyled()
    {
        var output = new RecordingOutput();
        var actionInvocations = 0;

        var result = OutputUtilities.ExecuteWithSpacing(
            output,
            () =>
            {
                actionInvocations++;
                output.WriteLine("action-body");
            },
            "DemoMethod");

        result.IsRight.Should().BeTrue();
        actionInvocations.Should().Be(1);
        output.Messages.Should().ContainInOrder(
            "************************************************************",
            "Executing method: DemoMethod",
            "************************************************************",
            "action-body",
            "************************************************************");
    }

    private sealed class RecordingOutput : IOutput
    {
        public List<string> Messages { get; } = [];
        public void WriteLine(string message) => Messages.Add(message);
    }

    private sealed class RecordingStyledOutput : IStyledOutput
    {
        public List<string> Messages { get; } = [];
        public List<ConsoleColor> Colors { get; } = [];

        public void WriteLine(string message) => Messages.Add(message);

        public void WithColor(ConsoleColor color, Action action)
        {
            Colors.Add(color);
            action();
        }
    }
}
