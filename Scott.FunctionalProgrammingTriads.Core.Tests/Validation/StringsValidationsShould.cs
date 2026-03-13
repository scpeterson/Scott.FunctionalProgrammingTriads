using FluentAssertions;
using LanguageExt;
using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Validation;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Validation;

public class StringsValidationsShould
{
    [Fact]
    public void RequiredWithMaxLengthReturnSuccessForValidInput()
    {
        var result = Strings.RequiredWithMaxLength(Some("Scott"), "FirstName", 10);

        result.ShouldBeSuccess();
    }

    [Fact]
    public void RequiredWithMaxLengthReturnFailForTooLongInput()
    {
        var result = Strings.RequiredWithMaxLength(Some("ThisNameIsFarTooLong"), "FirstName", 5);

        result.ShouldBeFail();
    }

    [Fact]
    public void AlphaOnlyReturnFailForNonAlphaInput()
    {
        var result = Strings.AlphaOnly("FirstName")("Scott1");

        result.ShouldBeFail();
    }

    [Fact]
    public void EmailReturnSuccessForValidEmail()
    {
        var result = Strings.Email("Email")("person@example.com");

        result.ShouldBeSuccess();
    }

    [Fact]
    public void EmailReturnFailForInvalidEmail()
    {
        var result = Strings.Email("Email")("invalid-email");

        result.ShouldBeFail();
    }
}
