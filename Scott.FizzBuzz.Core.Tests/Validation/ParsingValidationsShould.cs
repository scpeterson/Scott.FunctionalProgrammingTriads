using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Validation;

namespace Scott.FizzBuzz.Core.Tests.Validation;

public class ParsingValidationsShould
{
    [Fact]
    public void Int32ReturnSuccessForValidInteger()
    {
        var result = Parsing.Int32("42", "Age");

        result.ShouldBeSuccess();
    }

    [Fact]
    public void Int32ReturnFailForInvalidInteger()
    {
        var result = Parsing.Int32("abc", "Age");

        result.ShouldBeFail();
    }

    [Fact]
    public void DecimalReturnSuccessForValidDecimal()
    {
        var result = Parsing.Decimal("10.50", "Price");

        result.ShouldBeSuccess();
    }

    [Fact]
    public void DecimalReturnFailForInvalidDecimal()
    {
        var result = Parsing.Decimal("ten", "Price");

        result.ShouldBeFail();
    }
}
