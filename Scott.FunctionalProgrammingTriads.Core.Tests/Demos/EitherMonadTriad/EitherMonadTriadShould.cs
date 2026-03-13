using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.EitherMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.EitherMonadTriad;

public class EitherMonadTriadShould
{
    [Fact]
    public void RunAllEitherMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        var demos = new IDemo[]
        {
            new ImperativeEitherComparisonDemo(output),
            new CSharpEitherComparisonDemo(output),
            new LanguageExtEitherMonadComparisonDemo()
        };

        foreach (var demo in demos)
        {
            var result = demo.Run("vip", "50");
            result.ShouldBeRight();
        }
    }

    [Fact]
    public void ReturnLeftForInvalidDiscountCodeInLanguageExtVariant()
    {
        var demo = new LanguageExtEitherMonadComparisonDemo();

        var result = demo.Run("unknown-code", "50");

        result.ShouldBeLeft();
    }
}
