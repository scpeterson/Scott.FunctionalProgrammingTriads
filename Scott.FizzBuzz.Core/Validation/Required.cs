using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Required
{
    public static Validation<Error, T> Value<T>(Option<T> value, string parameterName) =>
        value.Match(
            Some: val =>
                !string.IsNullOrWhiteSpace(val?.ToString())
                    ? Success<Error, T>(val)
                    : Fail<Error, T>(Error.New($"The value of '{parameterName}' cannot be empty or whitespace.")),
            None: () => Fail<Error, T>(Error.New($"The value of '{parameterName}' is required.")));
}
