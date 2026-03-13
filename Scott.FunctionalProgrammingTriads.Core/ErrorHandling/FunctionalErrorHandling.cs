using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.ErrorHandling;

public static class FunctionalErrorHandling
{
    // Pure function to map LanguageExt errors into a list of messages.
    public static Either<string, List<string>> ShowParseErrors(IEnumerable<Error> errors)
    {
        var errorMessages = errors.Select(error => error.Message).ToList();
        return Right<string, List<string>>(errorMessages);
    }
}
