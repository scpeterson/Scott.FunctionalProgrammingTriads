using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Tests.TestUtilities;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConcurrencySafetyTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.ConcurrencySafetyTriad;

public class ConcurrencySafetyTriadShould
{
    [Fact]
    public void RunAllConcurrencySafetyTriadVariantsForHappyPath()
    {
        var output = new NullOutputSink();

        IDemo[] demos =
        [
            new ImperativeConcurrencySafetyComparisonDemo(output),
            new CSharpConcurrencySafetyComparisonDemo(output),
            new LanguageExtConcurrencySafetyComparisonDemo()
        ];

        foreach (var demo in demos)
        {
            demo.Run("scenario", "1000").ShouldBeRight();
        }
    }

    [Fact]
    public void ReturnLeftForInvalidIterationsInLanguageExtVariant() =>
        new LanguageExtConcurrencySafetyComparisonDemo().Run("scenario", "0").ShouldBeLeft();

    [Fact]
    public void DemonstrateLostUpdatesInImperativeUnsafeSimulation()
    {
        var result = ConcurrencySafetyRules.ExecuteImperativeUnsafe(iterations: 1000);

        Assert.Equal(2000, result.ExpectedBalance);
        Assert.Equal(1000, result.FinalBalance);
        Assert.Equal(1000, result.LostUpdates);
    }

    [Fact]
    public void PreserveAllUpdatesInCSharpAndLanguageExtSimulations()
    {
        var csharp = ConcurrencySafetyRules.ExecuteCSharpAtomic(iterations: 1000);
        var languageExt = LanguageExtConcurrencySafetyRules.ExecuteLanguageExtPure(iterations: 1000);

        Assert.Equal(csharp.ExpectedBalance, csharp.FinalBalance);
        Assert.Equal(languageExt.ExpectedBalance, languageExt.FinalBalance);
        Assert.Equal(0, csharp.LostUpdates);
        Assert.Equal(0, languageExt.LostUpdates);
    }
}
