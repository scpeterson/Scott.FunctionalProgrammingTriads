// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommandLine;
using LanguageExt;
using Scott.FizzBuzz.Console;
using Scott.FizzBuzz.Core;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IOutput, ConsoleOutput>();
        services.AddFizzBuzzDemos();
        
        services.AddTransient<DemoRunner>();
    })
    .Build();

var parser = new Parser(with => with.HelpWriter = Console.Error);

parser.ParseArguments<Options>(args)
    .WithParsed<Options>(opts =>
    {
        var runner = host.Services.GetRequiredService<DemoRunner>();
        runner.Execute(opts)
            .IfLeft(err => Environment.Exit(1));
    })
    .WithNotParsed(errors => ShowParseErrors(errors));

return;


void ShowParseErrors(IEnumerable<Error> errors)
{
    Console.WriteLine("Failed to parse command line arguments due to the following errors:");
    foreach (var error in errors)
    {
        Console.WriteLine($"- {error.Tag}: {error}");
    }
}
