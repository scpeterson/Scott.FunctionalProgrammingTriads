using LanguageExt;
using LanguageExt.Common;
using Scott.FizzBuzz.Core.Validation;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.ApplicativeValidationExample;

public static class ApplicativeValidationDemo
{
    public static Validation<Error, UserEntity> ValidateToEntity(string? firstName, string? ageText) =>
        ValidateToEntity(Optional(firstName), Optional(ageText));

    public static Validation<Error, UserEntity> ValidateToEntity(Option<string> firstName, Option<string> ageText)
    {
        var validFirstName =
            Strings.RequiredWithMaxLength(firstName, "FirstName", 50)
                .Bind(Strings.AlphaOnly("FirstName"));

        var validAge =
            Required.Text(ageText, "Age")
                .Bind(text => Parsing.Int32(text, "Age"))
                .Bind(age => Numeric.Positive(Some(age), "Age"));

        var validatedUser =
            (validAge, validFirstName)
                .Apply((age, fn) => (Age: age, FirstName: fn))
                .Bind(values => User.Create(values.Age, values.FirstName));

        return validatedUser.Map(user => new UserEntity
        {
            Age = user.Age,
            FirstName = user.FirstName
        });
    }
}
