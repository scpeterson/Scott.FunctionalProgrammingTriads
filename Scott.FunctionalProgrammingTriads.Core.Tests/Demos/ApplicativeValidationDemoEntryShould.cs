using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos;

public class ApplicativeValidationDemoEntryShould
{
    [Fact]
    public void ReturnRightForValidInput()
    {
        var demo = new ApplicativeValidationDemo(new RecordingOutputSink());

        var result = demo.Run("Scott", "21");

        result.ShouldBeRight();
    }

    [Fact]
    public void ReturnLeftForInvalidInput()
    {
        var demo = new ApplicativeValidationDemo(new RecordingOutputSink());

        var result = demo.Run("Scott1", "-2");

        result.ShouldBeLeft();
    }
}
