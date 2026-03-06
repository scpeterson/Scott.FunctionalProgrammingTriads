using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Demos.DatabaseTextStoreTriad;

namespace Scott.FizzBuzz.Core.Tests.Demos.DatabaseTextStoreTriad;

public class DatabaseTextStoreTriadShould
{
    [Fact]
    public void ExecuteAllThreeDatabaseTriadDemosOnHappyPath()
    {
        new ImperativeTextStoreDatabaseDemo().Run("Scott", "42").ShouldBeRight();
        new CSharpFunctionalTextStoreDatabaseDemo().Run("Scott", "42").ShouldBeRight();
        new LanguageExtEffTextStoreDatabaseDemo().Run("Scott", "42").ShouldBeRight();
    }

    [Fact]
    public void SurfaceFailureDifferentlyAcrossStylesForInvalidAge()
    {
        Assert.Throws<FormatException>(() => new ImperativeTextStoreDatabaseDemo().Run("Scott", "bad"));
        new CSharpFunctionalTextStoreDatabaseDemo().Run("Scott", "bad").ShouldBeLeft();
        new LanguageExtEffTextStoreDatabaseDemo().Run("Scott", "bad").ShouldBeLeft();
    }
}
