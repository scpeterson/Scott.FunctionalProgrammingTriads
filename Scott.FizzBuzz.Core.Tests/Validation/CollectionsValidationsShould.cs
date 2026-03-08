using FluentAssertions;
using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Validation;

namespace Scott.FizzBuzz.Core.Tests.Validation;

public class CollectionsValidationsShould
{
    [Fact]
    public void NonEmptyReturnSuccessForPopulatedCollection()
    {
        var result = Collections.NonEmpty([1, 2, 3], "Numbers");

        result.ShouldBeSuccess();
        result.Match(
            Succ: values => values.Should().HaveCount(3),
            Fail: _ => throw new Xunit.Sdk.XunitException("Expected success for populated collection."));
    }

    [Fact]
    public void NonEmptyReturnFailForEmptyOrNullCollection()
    {
        Collections.NonEmpty(Array.Empty<int>(), "Numbers").ShouldBeFail();
        Collections.NonEmpty<int>(null, "Numbers").ShouldBeFail();
    }
}
