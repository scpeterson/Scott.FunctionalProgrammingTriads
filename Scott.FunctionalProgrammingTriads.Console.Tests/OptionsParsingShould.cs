using CommandLine;
using FluentAssertions;

namespace Scott.FunctionalProgrammingTriads.Console.Tests;

public class OptionsParsingShould
{
    [Fact]
    public void ParseShortListAlias()
    {
        var parser = new Parser();
        var parsedOptions = default(Options);

        var result = parser.ParseArguments<Options>(["-l"])
            .WithParsed(options => parsedOptions = options);

        result.Tag.Should().Be(ParserResultType.Parsed);
        parsedOptions.Should().NotBeNull();
        parsedOptions!.List.Should().BeTrue();
    }
}
