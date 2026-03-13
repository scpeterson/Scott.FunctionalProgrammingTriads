using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.CompositionRootTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.CompositionRootTriad;

public class CompositionRootTriadShould
{
    [Fact]
    public void RunAllCompositionRootVariantsForHappyPath()
    {
        var output = new NullOutputSink();
        IDemo[] demos =
        [
            new ImperativeCompositionRootComparisonDemo(output),
            new CSharpCompositionRootComparisonDemo(output),
            new LanguageExtCompositionRootComparisonDemo()
        ];

        foreach (var demo in demos)
        {
            demo.Run("vip", "100").ShouldBeRight();
        }
    }

    [Fact]
    public void ReturnLeftForInvalidAmountInLanguageExtVariant() =>
        new LanguageExtCompositionRootComparisonDemo().Run("vip", "abc").ShouldBeLeft();
}
