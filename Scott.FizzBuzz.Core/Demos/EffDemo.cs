using LanguageExt;
using Scott.FizzBuzz.Core.AffExamples;
using Scott.FizzBuzz.Core;
using Scott.FizzBuzz.Core.CommonExampleCode;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;
using static Scott.FizzBuzz.Core.OutputUtilities;

namespace Scott.FizzBuzz.Core.Demos;

public class EffDemo : IDemo
{
    private readonly IOutput _output;

    public EffDemo() : this(new ConsoleOutput())
    {
    }

    public EffDemo(IOutput output)
    {
        _output = output;
    }

    public string Key => "demo-eff";
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "eff", "aff", "effects"];
    public Either<string, Unit> Run(string? idInput, string? _)
    {
        return ExecuteWithSpacing(_output, () =>
        {
            // 1) Parse into Either<string,int>
            var parsed = ParseId(idInput);
            parsed.Match(
                Left: err =>
                    _output.WriteLine($"[Eff] Invalid input: {err}"),
                Right: id =>
                {
                    // 2) Synchronous Eff<Person> → Try(...) → ToEither(...)
                    Either<string, Person> userEither =
                        Try(() =>
                            {
                                if (FakeDatabase.Persons.TryGetValue(id, out var p))
                                    return p;
                                throw new KeyNotFoundException($"No user with id {id}");
                            })
                            .ToEither(ex => ex.Message);

                    // Now `userEither` has its own Match overload returning Unit
                    userEither.Match(
                        Left:  err2 => _output.WriteLine($"[Eff] Error: {err2}"),
                        Right: p    => _output.WriteLine($"[Eff] Found: {p.Name}, {p.Age}")
                    );

                    // 3) Async Aff<Person> example
                    var aff = UserRepositoryAff
                        .GetUserAsync(id)                           // Aff<Person>
                        .Map(p => $"[Aff] Found async: {p.Name}, {p.Age}")
                        .IfFail(ex => $"[Aff] Error: {ex.Message}");

                    var affResult = aff.Run().Result;             // sync-unwrap for demo
                    _output.WriteLine(affResult.Match(
                        Succ: value => value,
                        Fail: error => $"[Aff] Error: {error.Message}"));
                });
        }, "LanguageExt Eff/Aff Demo");
    }
    
    // Parse the string ID into an Either<string,int>
    private static Either<string, int> ParseId(string? s) =>
        !string.IsNullOrWhiteSpace(s) && int.TryParse(s, out var i)
            ? Right(i)
            : Left("Please specify a valid integer ID.");
}
