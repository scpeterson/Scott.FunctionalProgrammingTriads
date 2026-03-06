using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.EndToEndMiniFeatureTriad;

public static class LanguageExtFunctionalRegistrationLogic
{
    public static Either<string, RegisteredUser> Register(string? name, string? ageRaw) =>
        ValidateName(name)
            .ToEither()
            .MapLeft(errors => string.Join(" | ", errors.Map(error => error.Message)))
            .Bind(validName => ParseAge(ageRaw)
                .Bind(RequireAdult)
                .Map(age => new RegistrationRequest(validName, age)))
            .Bind(Save);

    public static Validation<Error, string> ValidateName(string? value) =>
        !string.IsNullOrWhiteSpace(value)
            ? Success<Error, string>(value.Trim())
            : Fail<Error, string>(Error.New("Name is required."));

    public static Either<string, int> ParseAge(string? value) =>
        int.TryParse(value, out var parsed)
            ? Right<string, int>(parsed)
            : Left<string, int>("Age must be numeric.");

    public static Either<string, int> RequireAdult(int age) =>
        age >= 18
            ? Right<string, int>(age)
            : Left<string, int>("Must be at least 18.");

    public static Either<string, RegisteredUser> Save(RegistrationRequest request) =>
        Right<string, RegisteredUser>(new RegisteredUser(
            $"{request.Name.ToLowerInvariant()}-{request.Age}",
            request.Name,
            request.Age));
}
