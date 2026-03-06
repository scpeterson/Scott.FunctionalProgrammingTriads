using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Scott.FizzBuzz.Core.Interfaces;

namespace Scott.FizzBuzz.Console.Tests;

public class RegisteredDemosShould
{
    [Fact]
    public void HaveNonEmptyUniqueKeys()
    {
        // Arrange
        var demos = ResolveRegisteredDemos();

        // Assert
        demos.Should().NotBeEmpty();
        demos.Select(d => d.Key).Should().OnlyContain(k => !string.IsNullOrWhiteSpace(k));

        var duplicateKeys = demos
            .GroupBy(d => d.Key)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        duplicateKeys.Should().BeEmpty("duplicate demo keys cause runtime registration collisions.");
    }

    [Fact]
    public void FunctionalDemosIncludeFpTag()
    {
        // Arrange
        var demos = ResolveRegisteredDemos();

        // Assert
        var functionalWithoutFpTag = demos
            .Where(d => string.Equals(d.Category, "functional", StringComparison.OrdinalIgnoreCase))
            .Where(d => d.Tags.All(t => !string.Equals(t, "fp", StringComparison.OrdinalIgnoreCase)))
            .Select(d => d.Key)
            .ToList();

        functionalWithoutFpTag.Should().BeEmpty("functional demos should advertise the fp tag for discoverability.");
    }

    [Fact]
    public void DotNet10DemosHaveExpectedTags()
    {
        // Arrange
        var demos = ResolveRegisteredDemos().ToDictionary(d => d.Key, d => d);

        // Assert
        demos.Should().ContainKey("fp-json-strict-validation");
        demos["fp-json-strict-validation"].Tags.Should().Contain(["fp", "dotnet10"]);

        demos.Should().ContainKey("fp-extension-members-typeclasses");
        demos["fp-extension-members-typeclasses"].Tags.Should().Contain(["fp", "dotnet10", "csharp14"]);
    }

    private static List<IDemo> ResolveRegisteredDemos()
    {
        var services = new ServiceCollection();
        services.AddFizzBuzzDemos();
        using var provider = services.BuildServiceProvider();
        return provider.GetServices<IDemo>().ToList();
    }
}
