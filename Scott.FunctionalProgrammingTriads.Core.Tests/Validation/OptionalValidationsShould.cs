using LanguageExt;
using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Validation;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Validation;

public class OptionalValidationsShould
{
    [Fact]
    public void PassThroughReturnSuccessForNoneAndSome()
    {
        Scott.FunctionalProgrammingTriads.Core.Validation.Optional.PassThrough(Option<string>.None).ShouldBeSuccess();
        Scott.FunctionalProgrammingTriads.Core.Validation.Optional.PassThrough(Some("x")).ShouldBeSuccess();
    }

    [Fact]
    public void RequireWhenReturnFailIfConditionIsTrueAndValueIsMissing()
    {
        var result = Scott.FunctionalProgrammingTriads.Core.Validation.Optional.RequireWhen(true, Option<int>.None, "Count");

        result.ShouldBeFail();
    }

    [Fact]
    public void RequireWhenReturnSuccessIfConditionIsFalse()
    {
        var result = Scott.FunctionalProgrammingTriads.Core.Validation.Optional.RequireWhen(false, Option<int>.None, "Count");

        result.ShouldBeSuccess();
    }
}
