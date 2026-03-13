using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.SeqMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.SeqMonadTriad;

public class SeqMonadTriadShould
{
    [Fact]
    public void RunAllSeqMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();
        IDemo[] demos = [new ImperativeSeqMonadComparisonDemo(output), new CSharpSeqMonadComparisonDemo(output), new LanguageExtSeqMonadComparisonDemo()];
        foreach (var demo in demos) demo.Run("scott", "3").ShouldBeRight();
    }

    [Fact]
    public void ReturnLeftForBadThresholdInLanguageExtVariant() =>
        new LanguageExtSeqMonadComparisonDemo().Run("scott", "abc").ShouldBeLeft();

}
