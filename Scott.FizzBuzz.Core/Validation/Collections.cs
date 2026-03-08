using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Collections
{
    public static Validation<Error, IReadOnlyCollection<T>> NonEmpty<T>(IEnumerable<T>? input, string parameterName)
    {
        var materialized = input?.ToList();

        return materialized is { Count: > 0 }
            ? Success<Error, IReadOnlyCollection<T>>(materialized)
            : Fail<Error, IReadOnlyCollection<T>>(Error.New($"The list '{parameterName}' cannot be null or empty."));
    }
}
