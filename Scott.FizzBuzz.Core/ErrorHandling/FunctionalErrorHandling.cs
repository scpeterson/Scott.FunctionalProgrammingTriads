
using LanguageExt;
using Scott.FizzBuzz.Core.CommonExampleCode;
using static LanguageExt.Prelude;
using static System.Console;

namespace Scott.FizzBuzz.Core.ErrorHandling;

public static class FunctionalErrorHandling
{
    // Pure function to map errors into a List of strings
    public static Either<string, List<string>> ShowParseErrors(IEnumerable<Error> errors)
    {
        var errorMessages = errors.Select(error => $"{error.Message}").ToList();
        return Right<string, List<string>>(errorMessages);
    }

    // Side effect function that prints errors to the console
    public static void PrintErrors(Either<string, List<string>> eitherErrors)
    {
        eitherErrors.Match(
            Right: errors =>
            {
                WriteLine("Failed to parse command line arguments due to the following errors:");
                errors.ForEach(WriteLine);
            },
            Left: errorMessage =>
            {
                WriteLine($"Error: {errorMessage}");
            }
        );
    }
}
