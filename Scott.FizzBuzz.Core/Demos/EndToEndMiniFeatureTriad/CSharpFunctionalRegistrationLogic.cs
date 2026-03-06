using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.EndToEndMiniFeatureTriad;

public static class CSharpFunctionalRegistrationLogic
{
    public static Either<string, RegisteredUser> Register(string? name, string? ageRaw) =>
        ValidateName(name)
            .Bind(validName => ParseAge(ageRaw)
                .Bind(RequireAdult)
                .Map(age => new RegistrationRequest(validName, age)))
            .Bind(Save);

    public static Either<string, string> ValidateName(string? value) =>
        !string.IsNullOrWhiteSpace(value)
            ? Right<string, string>(value.Trim())
            : Left<string, string>("Name is required.");

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
