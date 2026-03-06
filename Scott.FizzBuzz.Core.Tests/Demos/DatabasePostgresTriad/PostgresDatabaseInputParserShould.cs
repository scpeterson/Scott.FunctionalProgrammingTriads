using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Demos.DatabasePostgresTriad;

namespace Scott.FizzBuzz.Core.Tests.Demos.DatabasePostgresTriad;

public class PostgresDatabaseInputParserShould
{
    [Fact]
    public void ParseValidInput()
    {
        var result = PostgresDatabaseInputParser.Parse("Scott", "21");

        result.ShouldBeRight();
    }

    [Fact]
    public void ReturnLeftForInvalidAge()
    {
        var result = PostgresDatabaseInputParser.Parse("Scott", "not-a-number");

        result.ShouldBeLeft();
    }

    [Fact]
    public void ReturnLeftForNegativeAge()
    {
        var result = PostgresDatabaseInputParser.Parse("Scott", "-1");

        result.ShouldBeLeft();
    }
}
