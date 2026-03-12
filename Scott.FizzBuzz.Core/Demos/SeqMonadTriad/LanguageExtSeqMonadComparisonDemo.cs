using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;

namespace Scott.FizzBuzz.Core.Demos.SeqMonadTriad;

public class LanguageExtSeqMonadComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtSeqMonadComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtSeqMonadComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-seq-monad-comparison";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "seq", "monad"];

    public Either<string, Unit> Run(string? name, string? number) =>
        FunctionalDemoOutput.Render(
            _output,
            "LanguageExt Seq Monad Comparison",
            ComputeResult(name, number),
            (output, result) => output.WriteLine($"Result: {result}"));

    private static Either<string, int> ComputeResult(string? name, string? number) =>
        from numbers in SeqMonadRules.ResolveNumbers(name)
        from threshold in SeqMonadRules.ParseThreshold(number)
        select SeqMonadRules.Compute(numbers, threshold);
}
