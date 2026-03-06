using LanguageExt;
using Scott.FizzBuzz.Core.CommonExampleCode;
using static LanguageExt.Prelude;
using static Scott.FizzBuzz.Core.CommonExampleCode.FakeDatabase;

namespace Scott.FizzBuzz.Core.EffExamples;

public static class UserRepositoryEff
{
    // Use Option to model a function that could either return a User or None
    public static Option<Person> GetUser(int id)
    {
        // Use TryGetValue to more efficiently check for the value
        return Persons.TryGetValue(id, out var user) 
            ? Some(user) 
            : None;
    }
}