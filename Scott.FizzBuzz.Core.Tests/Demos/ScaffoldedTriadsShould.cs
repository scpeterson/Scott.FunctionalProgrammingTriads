using LanguageExt.UnitTesting;
using Scott.FizzBuzz.Core.Demos;
using Scott.FizzBuzz.Core.Demos.NullOptionTriad;
using Scott.FizzBuzz.Core.Demos.ParseValidateTriad;
using Scott.FizzBuzz.Core.Demos.ValidationAccumulationTriad;
using Scott.FizzBuzz.Core.Interfaces;

namespace Scott.FizzBuzz.Core.Tests.Demos;

public class ScaffoldedTriadsShould
{
    [Fact]
    public void ExecuteScaffoldedTriadDemos()
    {
        // Placeholder safety net for scaffolded demos: verifies wiring + executable shape.
        var demos = new IDemo[]
        {
            new ImperativeNullHandlingDemo(),
            new CSharpNullPatternsDemo(),
            new LanguageExtOptionPipelineDemo(),
            new ImperativeParseValidateDemo(),
            new CSharpParseValidatePipelineDemo(),
            new LanguageExtEitherParseValidateDemo(),
            new ImperativeValidationFirstErrorDemo(),
            new CSharpValidationErrorListDemo(),
            new LanguageExtValidationAccumulateDemo()
        };

        foreach (var demo in demos)
        {
            var result = demo.Run("Scott", "21");
            result.ShouldBeRight();
        }
    }
}
