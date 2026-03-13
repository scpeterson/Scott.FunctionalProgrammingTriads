using FluentAssertions;

namespace Scott.FunctionalProgrammingTriads.Core.Tests;

public class FizzBuzzExamplesShould
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    public void ProduceExpectedImperativeValues(int input, string expected)
    {
        var actual = ImperativeExample.ImperativeFizzBuzz(input);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    public void ProduceExpectedNoDependencyValues(int input, string expected)
    {
        var actual = NoDependencyExample.NoDependencyFizzBuzz(input);
        actual.Should().Be(expected);
    }
}
