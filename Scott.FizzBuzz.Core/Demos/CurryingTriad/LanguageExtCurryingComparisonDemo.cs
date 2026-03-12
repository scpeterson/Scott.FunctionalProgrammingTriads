using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.CurryingTriad;

public class LanguageExtCurryingComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtCurryingComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtCurryingComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-currying-comparison";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "currying", "triad"];

    public Either<string, Unit> Run(string? name, string? number) =>
        FunctionalDemoOutput.Render(
            _output,
            "LanguageExt Currying Comparison",
            ComputeResult(name, number),
            (output, total) => output.WriteLine($"Result: total = {total:0.00}"));

    private static Either<string, decimal> ComputeResult(string? name, string? number) =>
        from amount in CurryingTriadRules.ParseBaseAmount(number)
        from rates in CurryingTriadRules.ResolveRates(name)
        select CurryingTriadRules.CurriedTotal(amount)(rates.DiscountRate)(rates.TaxRate);
}
