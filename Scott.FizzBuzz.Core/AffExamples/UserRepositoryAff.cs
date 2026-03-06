using LanguageExt;
using Scott.FizzBuzz.Core.CommonExampleCode;
using static LanguageExt.Prelude;
using static Scott.FizzBuzz.Core.CommonExampleCode.FakeDatabase;

namespace Scott.FizzBuzz.Core.AffExamples;

public class UserRepositoryAff
{
    // Use Aff to model an asynchronous function that could either return a User or throw an Exception
    public static Aff<Person> GetUserAsync(int id)
    {
        return Aff<Person>(
            async () =>
            {
                // Simulate async work, such as fetching data from a database
                await Task.Delay(1000);

                if (Persons.TryGetValue(id, out var user))
                {
                    return user;
                }

                throw new KeyNotFoundException($"No user found with id {id}");
            }
        );
    }
}