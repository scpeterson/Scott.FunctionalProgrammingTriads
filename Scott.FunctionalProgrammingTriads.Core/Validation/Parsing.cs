using LanguageExt;
using LanguageExt.Common;
using System.Globalization;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Validation;

public static class Parsing
{
    public static Validation<Error, T> FromString<T>(string input) =>
        convert<T>(input).Match(
            Some: Success<Error, T>,
            None: () => Fail<Error, T>(Error.New($"Cannot parse '{input}' to {typeof(T).Name}.")));

    public static Validation<Error, int> Int32(string input, string parameterName) =>
        int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)
            ? Success<Error, int>(value)
            : Fail<Error, int>(Error.New($"The value of '{parameterName}' must be a valid integer."));

    public static Validation<Error, decimal> Decimal(string input, string parameterName) =>
        decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var value)
            ? Success<Error, decimal>(value)
            : Fail<Error, decimal>(Error.New($"The value of '{parameterName}' must be a valid decimal number."));
}
