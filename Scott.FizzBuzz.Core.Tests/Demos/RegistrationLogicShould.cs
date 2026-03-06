using FluentAssertions;
using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Demos.EndToEndMiniFeatureTriad;

namespace Scott.FizzBuzz.Core.Tests.Demos;

public class RegistrationLogicShould
{
    [Fact]
    public void CSharpLogicReturnLeftWhenNameMissing()
    {
        CSharpFunctionalRegistrationLogic.Register("", "21")
            .ShouldBeLeft(error => error.Should().Be("Name is required."));
    }

    [Fact]
    public void CSharpLogicReturnLeftWhenAgeIsNotNumeric()
    {
        CSharpFunctionalRegistrationLogic.Register("Scott", "bad")
            .ShouldBeLeft(error => error.Should().Be("Age must be numeric."));
    }

    [Fact]
    public void CSharpLogicReturnRightForValidInput()
    {
        CSharpFunctionalRegistrationLogic.Register("Scott", "21")
            .ShouldBeRight(user =>
            {
                user.Name.Should().Be("Scott");
                user.Age.Should().Be(21);
                user.Id.Should().Be("scott-21");
            });
    }

    [Fact]
    public void LanguageExtLogicReturnLeftWhenUnderage()
    {
        LanguageExtFunctionalRegistrationLogic.Register("Scott", "17")
            .ShouldBeLeft(error => error.Should().Be("Must be at least 18."));
    }

    [Fact]
    public void LanguageExtLogicReturnRightForValidInput()
    {
        LanguageExtFunctionalRegistrationLogic.Register("Scott", "21")
            .ShouldBeRight(user =>
            {
                user.Name.Should().Be("Scott");
                user.Age.Should().Be(21);
                user.Id.Should().Be("scott-21");
            });
    }
}
