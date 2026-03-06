using AutoFixture.Xunit2;
using FluentAssertions;
using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.CommonExampleCode;
using static Scott.FizzBuzz.Core.ErrorHandling.FunctionalErrorHandling;

namespace Scott.FizzBuzz.Core.Tests;

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