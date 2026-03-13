using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Validation;

public static class Required
{
    public static Validation<Error, T> Value<T>(Option<T> value, string parameterName) =>
        value.Match(
            Some: val =>
            {
                if (val is string text)
                {
                    return Text(Some(text), parameterName).Map(_ => val);
                }

                return Success<Error, T>(val);
            },
            None: () => Fail<Error, T>(Error.New($"The value of '{parameterName}' is required.")));

    public static Validation<Error, string> Text(Option<string> value, string parameterName) =>
        value.Match(
            Some: text =>
                !string.IsNullOrWhiteSpace(text)
                    ? Success<Error, string>(text.Trim())
                    : Fail<Error, string>(Error.New($"The value of '{parameterName}' cannot be empty or whitespace.")),
            None: () => Fail<Error, string>(Error.New($"The value of '{parameterName}' is required.")));
}
