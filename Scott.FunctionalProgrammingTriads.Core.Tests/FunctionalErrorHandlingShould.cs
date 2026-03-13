using FluentAssertions;
using LanguageExt.Common;
using static Scott.FunctionalProgrammingTriads.Core.ErrorHandling.FunctionalErrorHandling;

namespace Scott.FunctionalProgrammingTriads.Core.Tests;

public class FunctionalErrorHandlingShould
{
    [Theory]
    [MemberData(nameof(GetErrors))]
    public void ReturnExpectedErrors(IEnumerable<Error> errors)
    {
        var expectedErrors = errors.Select(error => error.Message).ToList();

        var result = ShowParseErrors(errors);

        result.IsRight.Should().BeTrue();
        result.RightToSeq().Head().Should().BeEquivalentTo(expectedErrors);
    }

    public static TheoryData<IEnumerable<Error>> GetErrors() => new()
    {
        new[] { Error.New("Error 1"), Error.New("Error 2") },
        new[] { Error.New("Error 1") },
        Array.Empty<Error>(),
    };
}
