using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Collections
{
    public static Validation<Error, IEnumerable<T>> NonEmpty<T>(IEnumerable<T> input, string parameterName) =>
        input != null && input.Any()
            ? Success<Error, IEnumerable<T>>(input)
            : Fail<Error, IEnumerable<T>>(Error.New($"The list '{parameterName}' cannot be null or empty."));
}
