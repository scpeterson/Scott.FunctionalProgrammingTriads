using FluentAssertions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Tests;

public class UtilitiesShould
{
    [Fact]
    public void ConvertEitherErrorToValidationFailure()
    {
        // Arrange
        var either = Left<Error, int>(Error.New("boom"));

        // Act
        var validation = either.ToValidation();

        // Assert
        validation.Match(
            Succ: _ => throw new Xunit.Sdk.XunitException("Expected validation failure."),
            Fail: (Seq<Error> errors) => errors.RenderMessages().Should().Contain("boom"));
    }

    [Fact]
    public void ConvertEitherRightToValidationSuccess()
    {
        // Arrange
        var either = Right<Error, int>(42);

        // Act
        var validation = either.ToValidation();

        // Assert
        validation.Match(
            Succ: value => value.Should().Be(42),
            Fail: (Seq<Error> _) => throw new Xunit.Sdk.XunitException("Expected validation success."));
    }
}
