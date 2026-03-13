using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.ReaderMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.ReaderMonadTriad;

public class ReaderMonadTriadShould
{
    [Fact]
    public void RunAllReaderMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        var demos = new IDemo[]
        {
            new ImperativeReaderComparisonDemo(output),
            new CSharpReaderComparisonDemo(output),
            new LanguageExtReaderMonadComparisonDemo()
        };

        foreach (var demo in demos)
        {
            var result = demo.Run("vip", "50");
            result.ShouldBeRight();
        }
    }

    [Fact]
    public void ReturnLeftForUnknownProfileInLanguageExtVariant()
    {
        var demo = new LanguageExtReaderMonadComparisonDemo();

        var result = demo.Run("unknown-profile", "50");

        result.ShouldBeLeft();
    }
}
