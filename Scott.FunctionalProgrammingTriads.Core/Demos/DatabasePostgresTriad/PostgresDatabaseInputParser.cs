using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.DatabasePostgresTriad;

public static class PostgresDatabaseInputParser
{
    public static Either<string, PostgresDatabaseInput> Parse(string? name, string? number)
    {
        var sanitizedName = string.IsNullOrWhiteSpace(name) ? "Guest" : name.Trim();

        if (!int.TryParse(number ?? "21", out var age))
            return Left<string, PostgresDatabaseInput>("Age must be an integer.");

        if (age < 0)
            return Left<string, PostgresDatabaseInput>("Age must be non-negative.");

        return Right<string, PostgresDatabaseInput>(new PostgresDatabaseInput(sanitizedName, age));
    }
}
