using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.WriterMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.WriterMonadTriad;

public class WriterMonadTriadShould
{
    [Fact]
    public void RunAllWriterMonadTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();
        IDemo[] demos = [new ImperativeWriterMonadComparisonDemo(output), new CSharpWriterMonadComparisonDemo(output), new LanguageExtWriterMonadComparisonDemo()];
        foreach (var demo in demos) demo.Run("scott", "10").ShouldBeRight();
    }

    [Fact]
    public void ReturnLeftForBadStartInLanguageExtVariant() =>
        new LanguageExtWriterMonadComparisonDemo().Run("scott", "abc").ShouldBeLeft();

}
