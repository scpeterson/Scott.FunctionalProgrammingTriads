using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Strings
{
    public static Func<string, Validation<Error, string>> MaxLength(int maxLength) =>
        s => !string.IsNullOrWhiteSpace(s) && s.Length <= maxLength
            ? Success<Error, string>(s)
            : Fail<Error, string>(Error.New($"The string '{s}' must not exceed {maxLength} characters."));

    public static Validation<Error, string> RequiredWithMaxLength(Option<string> value, string parameterName, int maxLength) =>
        Required.Value(value, parameterName).Bind(MaxLength(maxLength));
}
