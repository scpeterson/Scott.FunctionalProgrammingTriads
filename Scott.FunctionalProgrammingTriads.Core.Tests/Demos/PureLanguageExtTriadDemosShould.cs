using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Demos.NullOptionTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ParseValidateTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ValidationAccumulationTriad;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos;

public class PureLanguageExtTriadDemosShould
{
    [Fact]
    public void ReturnPureValuesForSuccessAndFailureCases()
    {
        // Option demo
        new LanguageExtOptionPipelineDemo().Run("Scott", "21").ShouldBeRight();
        new LanguageExtOptionPipelineDemo().Run("   ", "21").ShouldBeLeft();

        // Either demo
        new LanguageExtEitherParseValidateDemo().Run("Scott", "21").ShouldBeRight();
        new LanguageExtEitherParseValidateDemo().Run("Scott", "bad").ShouldBeLeft();

        // Validation demo
        new LanguageExtValidationAccumulateDemo().Run("Scott", "21").ShouldBeRight();
        new LanguageExtValidationAccumulateDemo().Run("", "bad").ShouldBeLeft();
    }
}
