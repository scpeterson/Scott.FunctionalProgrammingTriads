using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scott.FunctionalProgrammingTriads.Core;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Console;

public static class ConsoleApplication
{
    public static int Run(string[] args, IHost? host = null, TextWriter? errorWriter = null)
    {
        var resolvedHost = host ?? BuildHost(args);
        var ownsHost = host is null;
        var resolvedErrorWriter = errorWriter ?? System.Console.Error;

        try
        {
            var parser = new Parser(with => with.HelpWriter = resolvedErrorWriter);

            return parser.ParseArguments<Options>(args)
                .MapResult(
                    opts => ExecuteDemo(resolvedHost, opts, resolvedErrorWriter),
                    errors => HandleParseErrors(errors, resolvedErrorWriter));
        }
        finally
        {
            if (ownsHost)
            {
                resolvedHost.Dispose();
            }
        }
    }

    internal static IHost BuildHost(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<IOutput, ConsoleOutput>();
                services.AddFunctionalProgrammingTriadDemos();
                services.AddTransient<DemoRunner>();
            })
            .Build();

    private static int ExecuteDemo(IHost host, Options options, TextWriter errorWriter)
    {
        var runner = host.Services.GetRequiredService<DemoRunner>();
        var result = runner.Execute(options);
        if (result.IsSuccess)
        {
            return 0;
        }

        errorWriter.WriteLine(result.Error);
        return 1;
    }

    private static int HandleParseErrors(IEnumerable<Error> errors, TextWriter errorWriter)
    {
        errorWriter.WriteLine("Failed to parse command line arguments due to the following errors:");
        foreach (var error in errors)
        {
            errorWriter.WriteLine($"- {error.Tag}: {error}");
        }

        return 1;
    }
}
