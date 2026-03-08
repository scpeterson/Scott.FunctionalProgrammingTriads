using LanguageExt;
using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Validation;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Tests.Validation;

public class NumericValidationsShould
{
    [Fact]
    public void PositiveReturnSuccessWhenValueIsGreaterThanZero()
    {
        var result = Numeric.Positive(Some(10), "Age");

        result.ShouldBeSuccess();
    }

    [Fact]
    public void PositiveReturnFailWhenValueIsZeroOrLess()
    {
        Numeric.Positive(Some(0), "Age").ShouldBeFail();
        Numeric.Positive(Some(-1), "Age").ShouldBeFail();
    }

    [Fact]
    public void PositiveOrNoneReturnSuccessWhenNone()
    {
        var result = Numeric.PositiveOrNone(Option<int>.None, "Age");

        result.ShouldBeSuccess();
    }

    [Fact]
    public void PositiveOrNoneReturnFailWhenValueIsNonPositive()
    {
        var result = Numeric.PositiveOrNone(Some(-3), "Age");

        result.ShouldBeFail();
    }

    [Fact]
    public void NonNegativeReturnSuccessForZero()
    {
        var result = Numeric.NonNegative(Some(0), "Count");

        result.ShouldBeSuccess();
    }
}
