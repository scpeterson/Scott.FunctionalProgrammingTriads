using LanguageExt;
using LanguageExt.Common;
using System.Text.RegularExpressions;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Validation;

public static partial class Strings
{
    [GeneratedRegex("^[A-Za-z]+$")]
    private static partial Regex AlphaRegex();

    [GeneratedRegex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$")]
    private static partial Regex EmailRegex();

    public static Func<string, Validation<Error, string>> MinLength(int minLength, string parameterName) =>
        text => text.Length >= minLength
            ? Success<Error, string>(text)
            : Fail<Error, string>(Error.New($"The value of '{parameterName}' must be at least {minLength} characters."));

    public static Func<string, Validation<Error, string>> MaxLength(int maxLength, string parameterName) =>
        text => text.Length <= maxLength
            ? Success<Error, string>(text)
            : Fail<Error, string>(Error.New($"The value of '{parameterName}' must not exceed {maxLength} characters."));

    public static Func<string, Validation<Error, string>> AlphaOnly(string parameterName) =>
        text => AlphaRegex().IsMatch(text)
            ? Success<Error, string>(text)
            : Fail<Error, string>(Error.New($"The value of '{parameterName}' must contain letters A-Z only."));

    public static Func<string, Validation<Error, string>> Email(string parameterName) =>
        text => EmailRegex().IsMatch(text)
            ? Success<Error, string>(text)
            : Fail<Error, string>(Error.New($"The value of '{parameterName}' must be a valid email address."));

    public static Validation<Error, string> RequiredWithMaxLength(Option<string> value, string parameterName, int maxLength) =>
        Required.Text(value, parameterName)
            .Bind(MaxLength(maxLength, parameterName));
}
