using LanguageExt;
using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Validation;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Tests.Validation;

public class OptionalValidationsShould
{
    [Fact]
    public void PassThroughReturnSuccessForNoneAndSome()
    {
        Scott.FizzBuzz.Core.Validation.Optional.PassThrough(Option<string>.None).ShouldBeSuccess();
        Scott.FizzBuzz.Core.Validation.Optional.PassThrough(Some("x")).ShouldBeSuccess();
    }

    [Fact]
    public void RequireWhenReturnFailIfConditionIsTrueAndValueIsMissing()
    {
        var result = Scott.FizzBuzz.Core.Validation.Optional.RequireWhen(true, Option<int>.None, "Count");

        result.ShouldBeFail();
    }

    [Fact]
    public void RequireWhenReturnSuccessIfConditionIsFalse()
    {
        var result = Scott.FizzBuzz.Core.Validation.Optional.RequireWhen(false, Option<int>.None, "Count");

        result.ShouldBeSuccess();
    }
}
