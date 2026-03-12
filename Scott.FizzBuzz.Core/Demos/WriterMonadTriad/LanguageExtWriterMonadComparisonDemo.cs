using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.WriterMonadTriad;

public class LanguageExtWriterMonadComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtWriterMonadComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtWriterMonadComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-writer-monad-comparison";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "writer", "monad"];

    public Either<string, Unit> Run(string? name, string? number) =>
        FunctionalDemoOutput.Render(
            _output,
            "LanguageExt Writer Monad Comparison",
            ComputeResult(name, number),
            (output, result) =>
            {
                output.WriteLine($"Final state: {result.FinalState}");
                foreach (var entry in result.Logs)
                {
                    output.WriteLine(entry);
                }
            });

    private static Either<string, (int FinalState, Seq<string> Logs)> ComputeResult(string? name, string? number) =>
        from start in WriterMonadRules.ParseStart(number)
        from ops in WriterMonadRules.ResolveOps(name)
        let run = WriterMonadRules.RunProgram(start, ops).Run()
        select (ifNoneOrFail(run.Value, () => start, _ => start), run.Output);
}
