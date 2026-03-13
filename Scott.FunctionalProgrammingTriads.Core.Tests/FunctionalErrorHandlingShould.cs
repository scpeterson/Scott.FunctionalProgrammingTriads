using AutoFixture.Xunit2;
using FluentAssertions;
using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.CommonExampleCode;
using static Scott.FunctionalProgrammingTriads.Core.ErrorHandling.FunctionalErrorHandling;

namespace Scott.FunctionalProgrammingTriads.Core.Tests;

public class FunctionalErrorHandlingShould
{
    [Theory]
    [AutoData]
    public void ReturnExpectedErrors(IEnumerable<Error> errors)
    {
        //Arrange
        //$"- {error.Tag}: {error}"
        var expectedResult = errors.Select(error => $"{error.Message}").ToList();
        
        //Act
        var result = ShowParseErrors(errors);
        
        //Assert
        result.ShouldBeRight(x => x.Should().BeEquivalentTo(expectedResult));
    }
}