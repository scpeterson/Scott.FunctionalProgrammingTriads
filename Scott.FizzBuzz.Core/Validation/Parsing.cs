using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Parsing
{
    public static Validation<Error, T> FromString<T>(string input) =>
        convert<T>(input).Match(
            Some: Success<Error, T>,
            None: () => Fail<Error, T>(Error.New($"Cannot parse '{input}' to {typeof(T).Name}.")));
}
