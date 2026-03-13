using AutoFixture.Xunit2;
using FluentAssertions;
using LanguageExt;
using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Validation;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Validation;

public class RequiredValidationsShould
{
    [Theory, AutoData]
    public void Required_Value_Should_Return_Success_For_NonEmpty_Value(string value)
    {
        var result = Required.Value(Some(value), "Name");
        result.IsSuccess.Should().BeTrue("value should be considered valid");
        result.Match(
            Succ: val => val.Should().Be(value),
            Fail: _ => throw new Xunit.Sdk.XunitException("Expected success but got failure")
        );
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Required_Value_Should_Return_Failure_For_Empty_Or_Null(string? input)
    {
        var option = input == null ? Option<string>.None : Some(input);
        var result = Required.Value(option, "Name");
        result.ShouldBeFail();
    }

    [Fact]
    public void Required_Value_Should_Return_Success_For_NonString_Value()
    {
        var result = Required.Value(Some(0), "Count");

        result.ShouldBeSuccess();
    }
}
