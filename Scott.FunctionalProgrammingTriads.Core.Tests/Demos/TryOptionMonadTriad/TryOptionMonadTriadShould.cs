using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.TryOptionMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.TryOptionMonadTriad;

public class TryOptionMonadTriadShould
{
    [Fact]
    public void RunAllTryOptionMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();
        IDemo[] demos =
        [
            new ImperativeTryOptionMonadComparisonDemo(output),
            new CSharpTryOptionMonadComparisonDemo(output),
            new LanguageExtTryOptionMonadComparisonDemo()
        ];

        foreach (var demo in demos)
        {
            demo.Run("scott", "21").ShouldBeRight();
        }
    }

    [Fact]
    public void ReturnLeftForNoneInLanguageExtVariant() =>
        new LanguageExtTryOptionMonadComparisonDemo().Run("scott", "999").ShouldBeLeft();

    [Fact]
    public void ReturnLeftForExceptionInLanguageExtVariant() =>
        new LanguageExtTryOptionMonadComparisonDemo().Run("scott", "13").ShouldBeLeft();
}
