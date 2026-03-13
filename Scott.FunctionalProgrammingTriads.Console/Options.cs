using CommandLine;

namespace Scott.FunctionalProgrammingTriads.Console;

public class Options
{
    [Option('l', "list", Required = false, HelpText = "List available demos. Can be combined with --tag or --first-hour.")]
    public bool List { get; init; }

    [Option("tag", Required = false, HelpText = "Filter listed demos by tag (repeat --tag to add filters). Requires --list.")]
    public IEnumerable<string>? Tags { get; init; }

    [Option("first-hour", Required = false, HelpText = "Show the curated first-hour learning path. Requires --list.")]
    public bool FirstHour { get; init; }
    
    [Option('m', "method", Required = false, HelpText = "Demo key to execute. Cannot be combined with --list.")]
    public string? Method { get; init; }
    
    [Option('n', "name", Required = false, HelpText = "Name of person to search for in the database")]
    public string? Name { get; init; }
    
    [Option('b', "number", Required = false, HelpText = "Number to validate")]
    public string? Number { get; init; }
}
