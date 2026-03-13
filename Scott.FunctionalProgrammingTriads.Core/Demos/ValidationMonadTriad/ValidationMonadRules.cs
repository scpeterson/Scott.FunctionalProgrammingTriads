using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ValidationMonadTriad;

public static class ValidationMonadRules
{
    private static readonly Regex AlphaOnly = new("^[A-Za-z]+$", RegexOptions.Compiled);

    public static Validation<Error, ValidationMonadCandidate> ValidateCandidate(string? name, string? age) =>
        (ValidateName(name), ValidateAge(age))
            .Apply((n, a) => new ValidationMonadCandidate
            {
                Name = n,
                Age = a
            });

    public static Validation<Error, string> ValidateName(string? name)
    {
        var normalized = name?.Trim() ?? string.Empty;

        var required =
            normalized.Length > 0
                ? Success<Error, string>(normalized)
                : Fail<Error, string>(Error.New("Name is required."));

        var minLength =
            normalized.Length >= 3
                ? Success<Error, string>(normalized)
                : Fail<Error, string>(Error.New("Name must be at least 3 characters."));

        var alphaOnly =
            AlphaOnly.IsMatch(normalized)
                ? Success<Error, string>(normalized)
                : Fail<Error, string>(Error.New("Name must contain letters only."));

        return (required, minLength, alphaOnly)
            .Apply((_, _, _) => normalized);
    }

    public static Validation<Error, int> ValidateAge(string? age)
    {
        if (!int.TryParse(age, out var parsed))
        {
            return Fail<Error, int>(Error.New("Age must be numeric."));
        }

        return parsed is >= 18 and <= 120
            ? Success<Error, int>(parsed)
            : Fail<Error, int>(Error.New("Age must be between 18 and 120."));
    }
}
