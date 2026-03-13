using LanguageExt;
using LanguageExt.Common;
using System.Numerics;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Validation;

public static class Numeric
{
    public static Validation<Error, T> Positive<T>(Option<T> value, string parameterName)
        where T : struct, INumber<T> =>
        value.Match(
            Some: val => val > T.Zero
                ? Success<Error, T>(val)
                : Fail<Error, T>(Error.New($"The value '{val}' must be greater than zero for '{parameterName}'.")),
            None: () => Fail<Error, T>(Error.New($"The value of '{parameterName}' is required.")));

    public static Validation<Error, T> NonNegative<T>(Option<T> value, string parameterName)
        where T : struct, INumber<T> =>
        value.Match(
            Some: val => val >= T.Zero
                ? Success<Error, T>(val)
                : Fail<Error, T>(Error.New($"The value '{val}' must be zero or greater for '{parameterName}'.")),
            None: () => Fail<Error, T>(Error.New($"The value of '{parameterName}' is required.")));

    public static Validation<Error, Option<T>> PositiveOrNone<T>(Option<T> value, string parameterName)
        where T : struct, INumber<T> =>
        value.Match(
            Some: val => val > T.Zero
                ? Success<Error, Option<T>>(Some(val))
                : Fail<Error, Option<T>>(Error.New($"The value '{val}' must be greater than zero for '{parameterName}'.")),
            None: () => Success<Error, Option<T>>(Option<T>.None));
}
