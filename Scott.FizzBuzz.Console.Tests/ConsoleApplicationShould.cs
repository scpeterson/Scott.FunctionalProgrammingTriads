using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scott.FizzBuzz.Core.Interfaces;

namespace Scott.FizzBuzz.Console.Tests;

public class ConsoleApplicationShould
{
    [Fact]
    public void ReturnZeroWhenResolvedDemoSucceeds()
    {
        using var host = BuildHost(new StubDemo("stub-success", (_, _) => DemoExecutionResult.Success()));
        using var errorWriter = new StringWriter();

        var exitCode = ConsoleApplication.Run(["--method", "stub-success"], host, errorWriter);

        exitCode.Should().Be(0);
        errorWriter.ToString().Should().BeEmpty();
    }

    [Fact]
    public void ReturnOneAndWriteErrorWhenResolvedDemoFails()
    {
        using var host = BuildHost(new StubDemo("stub-failure", (_, _) => DemoExecutionResult.Failure("boom")));
        using var errorWriter = new StringWriter();

        var exitCode = ConsoleApplication.Run(["--method", "stub-failure"], host, errorWriter);

        exitCode.Should().Be(1);
        errorWriter.ToString().Should().Contain("boom");
    }

    [Fact]
    public void ReturnOneAndWriteParseErrorsWhenArgumentsAreInvalid()
    {
        using var errorWriter = new StringWriter();

        var exitCode = ConsoleApplication.Run(["--does-not-exist"], errorWriter: errorWriter);

        exitCode.Should().Be(1);
        errorWriter.ToString().Should().Contain("Failed to parse command line arguments");
        errorWriter.ToString().Should().Contain("UnknownOptionError");
    }

    private static IHost BuildHost(IDemo demo) =>
        new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<IDemo>(_ => demo);
                services.AddTransient<DemoRunner>();
            })
            .Build();
}
