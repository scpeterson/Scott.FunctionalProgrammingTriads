using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.ReaderMonadTriad;

public class LanguageExtReaderMonadComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtReaderMonadComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtReaderMonadComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-reader-monad-comparison";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "reader", "monad"];
    public string Description => "Reader monad composes environment-dependent functions without passing context explicitly.";

    public Either<string, Unit> Run(string? name, string? number) =>
        FunctionalDemoOutput.Render(
            _output,
            "LanguageExt Reader Monad Comparison",
            ComputeResult(name, number),
            (output, result) => output.WriteLine($"Result: {result}"));

    private static Either<string, string> ComputeResult(string? name, string? number) =>
        ReaderMonadRules.ResolveContext(name)
            .Bind(context => ReaderMonadRules.ParseSubtotal(number).Map(subtotal => (context, subtotal)))
            .Bind(args =>
            {
                var program = BuildProgram(args.subtotal);
                var fin = program.Run(args.context);

                return fin.Match(
                    Succ: result => Right<string, string>(result),
                    Fail: error => Left<string, string>($"Reader failure: {error.Message}"));
            });

    private static Reader<ReaderPricingContext, string> BuildProgram(decimal subtotal) =>
        from taxRate in Reader<ReaderPricingContext, decimal>(ctx => ctx.TaxRate)
        from fee in Reader<ReaderPricingContext, decimal>(ctx => ctx.ServiceFee)
        from profile in Reader<ReaderPricingContext, string>(ctx => ctx.ProfileName)
        from currency in Reader<ReaderPricingContext, string>(ctx => ctx.Currency)
        let total = (subtotal * (1m + taxRate)) + fee
        select $"{profile}: total = {total:0.00} {currency}";
}
