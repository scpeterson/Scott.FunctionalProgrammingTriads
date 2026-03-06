using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.EndToEndMiniFeatureTriad;

public class LanguageExtFunctionalRegistrationDemo : IDemo
{
    public string Key => "langext-functional-registration";
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "end-to-end"];
    public string Description => "LanguageExt registration flow with Validation + Either boundaries.";

    public Either<string, Unit> Run(string? name, string? number) =>
        LanguageExtFunctionalRegistrationLogic.Register(name, number).Map(_ => unit);
}
