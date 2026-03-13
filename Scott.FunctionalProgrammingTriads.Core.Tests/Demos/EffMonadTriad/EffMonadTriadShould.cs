using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.EffMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.EffMonadTriad;

public class EffMonadTriadShould
{
    [Fact]
    public void RunAllEffMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();
        IDemo[] demos = [new ImperativeEffMonadComparisonDemo(output), new CSharpEffMonadComparisonDemo(output), new LanguageExtEffMonadComparisonDemo()];
        foreach (var demo in demos) demo.Run("scott", "21").ShouldBeRight();
    }

    [Fact]
    public void ReturnLeftForUnknownProfileInLanguageExtVariant() =>
        new LanguageExtEffMonadComparisonDemo().Run("unknown", "21").ShouldBeLeft();

}
