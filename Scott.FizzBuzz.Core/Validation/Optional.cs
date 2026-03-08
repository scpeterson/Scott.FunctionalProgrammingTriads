using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Optional
{
    public static Validation<Error, Option<T>> PassThrough<T>(Option<T> value) =>
        Success<Error, Option<T>>(value);

    public static Validation<Error, Option<T>> RequireWhen<T>(bool condition, Option<T> value, string parameterName) =>
        !condition
            ? Success<Error, Option<T>>(value)
            : value.Match(
                Some: _ => Success<Error, Option<T>>(value),
                None: () => Fail<Error, Option<T>>(Error.New($"The value of '{parameterName}' is required.")));
}
