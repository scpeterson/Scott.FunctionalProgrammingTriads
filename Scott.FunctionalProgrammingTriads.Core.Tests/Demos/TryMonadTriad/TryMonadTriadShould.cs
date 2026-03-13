using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.TryMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.TryMonadTriad;

public class TryMonadTriadShould
{
    [Fact]
    public void RunAllTryMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();
        IDemo[] demos = [new ImperativeTryMonadComparisonDemo(output), new CSharpTryMonadComparisonDemo(output), new LanguageExtTryMonadComparisonDemo()];
        foreach (var demo in demos) demo.Run("scott", "2").ShouldBeRight();
    }

    [Fact]
    public void ReturnLeftForZeroInLanguageExtVariant() =>
        new LanguageExtTryMonadComparisonDemo().Run("scott", "0").ShouldBeLeft();

}
