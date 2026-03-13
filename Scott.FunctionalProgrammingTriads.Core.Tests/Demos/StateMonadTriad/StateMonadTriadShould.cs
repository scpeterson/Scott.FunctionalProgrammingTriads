using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.StateMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.StateMonadTriad;

public class StateMonadTriadShould
{
    [Fact]
    public void RunAllStateMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        var demos = new IDemo[]
        {
            new ImperativeStateComparisonDemo(output),
            new CSharpStateComparisonDemo(output),
            new LanguageExtStateMonadComparisonDemo()
        };

        foreach (var demo in demos)
        {
            var result = demo.Run("standard", "5");
            result.ShouldBeRight();
        }
    }

    [Fact]
    public void ReturnLeftForInvalidStepInLanguageExtVariant()
    {
        var demo = new LanguageExtStateMonadComparisonDemo();

        var result = demo.Run("standard", "abc");

        result.ShouldBeLeft();
    }
}
