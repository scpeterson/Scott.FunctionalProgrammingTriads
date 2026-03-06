using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Optional
{
    public static Validation<Error, Option<T>> PassThrough<T>(Option<T> value) =>
        Success<Error, Option<T>>(value);
}
