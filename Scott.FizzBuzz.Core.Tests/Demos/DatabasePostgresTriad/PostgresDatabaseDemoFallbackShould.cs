using FluentAssertions;
using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Demos.DatabasePostgresTriad;

namespace Scott.FizzBuzz.Core.Tests.Demos.DatabasePostgresTriad;

public class PostgresDatabaseDemoFallbackShould
{
    [Fact]
    public void ImperativeDemoShouldNotThrowWhenConnectionStringIsMissing()
    {
        var oldValue = Environment.GetEnvironmentVariable(PostgresDemoConfiguration.ConnectionEnvVar);
        Environment.SetEnvironmentVariable(PostgresDemoConfiguration.ConnectionEnvVar, null);

        try
        {
            var demo = new ImperativePostgresDatabaseDemo();
            Action act = () => _ = demo.Run("Scott", "21");
            act.Should().NotThrow();
        }
        finally
        {
            Environment.SetEnvironmentVariable(PostgresDemoConfiguration.ConnectionEnvVar, oldValue);
        }
    }

    [Fact]
    public void CSharpAndLanguageExtDemoShouldReturnLeftWhenConnectionStringIsMissing()
    {
        var oldValue = Environment.GetEnvironmentVariable(PostgresDemoConfiguration.ConnectionEnvVar);
        Environment.SetEnvironmentVariable(PostgresDemoConfiguration.ConnectionEnvVar, null);

        try
        {
            new CSharpFunctionalPostgresDatabaseDemo().Run("Scott", "21").ShouldBeLeft();
            new LanguageExtEffPostgresDatabaseDemo().Run("Scott", "21").ShouldBeLeft();
        }
        finally
        {
            Environment.SetEnvironmentVariable(PostgresDemoConfiguration.ConnectionEnvVar, oldValue);
        }
    }
}
