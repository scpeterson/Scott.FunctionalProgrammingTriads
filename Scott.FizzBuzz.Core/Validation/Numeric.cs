using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

using System.Numerics;

public static class Numeric
{
    public static Validation<Error, T> Positive<T>(Option<T> value, string parameterName)
        where T : struct, INumber<T> =>
        value.Match(
            Some: val => val > T.Zero
                ? Success<Error, T>(val)
                : Fail<Error, T>(Error.New($"The value '{val}' must be greater than zero for '{parameterName}'.")),
            None: () => Fail<Error, T>(Error.New($"The value of '{parameterName}' is required.")));

    public static Validation<Error, T?> PositiveOrNone<T>(Option<T?> value, string parameterName)
        where T : struct, INumber<T> =>
        value.Match(
            Some: val => !val.HasValue || val > T.Zero
                ? Success<Error, T?>(val)
                : Fail<Error, T?>(Error.New($"The value '{val}' must be greater than zero for '{parameterName}'.")),
            None: () => Fail<Error, T?>(Error.New($"The value of '{parameterName}' is required.")));
}
