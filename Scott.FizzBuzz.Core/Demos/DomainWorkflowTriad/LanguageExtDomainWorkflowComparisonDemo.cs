using LanguageExt;
using Scott.FizzBuzz.Core.Demos.Shared;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.DomainWorkflowTriad;

public class LanguageExtDomainWorkflowComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public LanguageExtDomainWorkflowComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public LanguageExtDomainWorkflowComparisonDemo(IOutput output) => _output = output;

    public const string DemoKey = "langext-domain-workflow";

    public string Key => DemoKey;
    public string Category => "functional";
    public IReadOnlyCollection<string> Tags => ["fp", "languageext", "comparison", "domain-modeling", "triad"];

    public Either<string, Unit> Run(string? name, string? number) =>
        FunctionalDemoOutput.Render(
            _output,
            "LanguageExt Domain Workflow Comparison",
            ComputeResult(name, number),
            (output, state) => output.WriteLine($"Result: {DomainWorkflowRules.Render(state)}"));

    private static Either<string, DomainWorkflowRules.FulfillmentState> ComputeResult(string? name, string? number)
    {
        var env = new InMemoryFunctionalDemoEnvironment();

        return
            from amount in DomainWorkflowRules.ParseAmount(number)
            from draft in DomainWorkflowRules.CreateDraft(amount)
            from authorized in DomainWorkflowRules.Authorize(env, draft)
            from packed in DomainWorkflowRules.Pack(authorized)
            from shipped in DomainWorkflowRules.Ship(packed)
            select (DomainWorkflowRules.FulfillmentState)shipped;
    }
}
