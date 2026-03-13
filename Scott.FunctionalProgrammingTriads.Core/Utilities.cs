using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core;

public static class Utilities
{
    public static TU Pipe<T, TU>(this T input, Func<T, TU> func)
    {
        var result = func(input);
        return result;
    }

    public static Either<Error, T> TryEither<T>(Func<T> thunk, Func<Exception, Error> mapError) =>
        Try(thunk).ToEither(mapError);

    public static string RenderMessages(this Seq<Error> errors, string separator = " | ") =>
        string.Join(separator, errors.Map(error => error.Message));
}
