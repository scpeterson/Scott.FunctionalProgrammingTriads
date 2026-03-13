using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.ApplicativeValidationExample;

// This is the domain entity. It contains the business rules.
public class User
{
    public int Age { get; private set; }
    public string FirstName { get; private set; }

    private User(int age, string firstName) 
    {
        Age = age;
        FirstName = firstName;
    }

    public static Validation<Error, User> Create(int age, string firstName)
    {
        return (ValidateAge(age), ValidateFirstName(firstName))
            .Apply((validAge, validFirstName) => 
                new User(validAge, validFirstName));
    }

    private static Validation<Error, int> ValidateAge(int age) =>
        age > 0
            ? Success<Error, int>(age)
            : Fail<Error, int>(Error.New("Age must be greater than zero."));

    private static Validation<Error, string> ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Fail<Error, string>(Error.New("First name cannot be null, empty, or whitespace."));
        
        if (!Regex.IsMatch(firstName, "^[a-zA-Z]+$"))
            return Fail<Error, string>(Error.New("First name can only contain letters A-Z or a-z."));

        return Success<Error, string>(firstName);
    }

    // ... other domain logic and methods
}

