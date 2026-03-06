using Microsoft.Extensions.DependencyInjection;
using Scott.FizzBuzz.Core.Demos;
using Scott.FizzBuzz.Core.Demos.AsyncEffTriad;
using Scott.FizzBuzz.Core.Demos.CollectionsAggregationTriad;
using Scott.FizzBuzz.Core.Demos.DatabasePostgresTriad;
using Scott.FizzBuzz.Core.Demos.DatabaseTextStoreTriad;
using Scott.FizzBuzz.Core.Demos.EndToEndMiniFeatureTriad;
using Scott.FizzBuzz.Core.Demos.ExceptionBoundariesTriad;
using Scott.FizzBuzz.Core.Demos.NullOptionTriad;
using Scott.FizzBuzz.Core.Demos.ParseValidateTriad;
using Scott.FizzBuzz.Core.Demos.ValidationAccumulationTriad;
using Scott.FizzBuzz.Core.Interfaces;

namespace Scott.FizzBuzz.Console;

public static class DemoServiceRegistration
{
    public static IServiceCollection AddFizzBuzzDemos(this IServiceCollection services)
    {
        services.AddTransient<IDemo, ImperativeDemo>();
        services.AddTransient<IDemo, RangeIterationDemo>();
        services.AddTransient<IDemo, NoDependencyDemo>();
        services.AddTransient<IDemo, PatternMatchingDemo>();
        services.AddTransient<IDemo, TupleDemo>();
        services.AddTransient<IDemo, CurryingDemo>();
        services.AddTransient<IDemo, EitherDemo>();
        services.AddTransient<IDemo, LanguageExtDemo>();
        services.AddTransient<IDemo, EffDemo>();
        services.AddTransient<IDemo, SchrodingersCatDemo>();
        services.AddTransient<IDemo, MonadicFunctionsDemo>();
        services.AddTransient<IDemo, MonadBasicsCatDemo>();
        services.AddTransient<IDemo, OtherMonadsDemo>();
        services.AddTransient<IDemo, FpJsonStrictValidationDemo>();
        services.AddTransient<IDemo, FpExtensionMembersTypeclassesDemo>();
        services.AddTransient<IDemo, ImperativeNullHandlingDemo>();
        services.AddTransient<IDemo, CSharpNullPatternsDemo>();
        services.AddTransient<IDemo, LanguageExtOptionPipelineDemo>();
        services.AddTransient<IDemo, ImperativeParseValidateDemo>();
        services.AddTransient<IDemo, CSharpParseValidatePipelineDemo>();
        services.AddTransient<IDemo, LanguageExtEitherParseValidateDemo>();
        services.AddTransient<IDemo, ImperativeValidationFirstErrorDemo>();
        services.AddTransient<IDemo, CSharpValidationErrorListDemo>();
        services.AddTransient<IDemo, LanguageExtValidationAccumulateDemo>();
        services.AddTransient<IDemo, ImperativeCollectionsAggregationDemo>();
        services.AddTransient<IDemo, CSharpCollectionsAggregationDemo>();
        services.AddTransient<IDemo, LanguageExtSeqAggregationDemo>();
        services.AddTransient<IDemo, ImperativeExceptionBoundariesDemo>();
        services.AddTransient<IDemo, CSharpResultRecoveryDemo>();
        services.AddTransient<IDemo, LanguageExtTryEitherRecoveryDemo>();
        services.AddTransient<IDemo, ImperativeAsyncWorkflowDemo>();
        services.AddTransient<IDemo, CSharpAsyncCompositionDemo>();
        services.AddTransient<IDemo, LanguageExtAsyncEffWorkflowDemo>();
        services.AddTransient<IDemo, ImperativeUserRegistrationDemo>();
        services.AddTransient<IDemo, CSharpFunctionalRegistrationDemo>();
        services.AddTransient<IDemo, LanguageExtFunctionalRegistrationDemo>();
        services.AddTransient<IDemo, ImperativeTextStoreDatabaseDemo>();
        services.AddTransient<IDemo, CSharpFunctionalTextStoreDatabaseDemo>();
        services.AddTransient<IDemo, LanguageExtEffTextStoreDatabaseDemo>();
        services.AddTransient<IDemo, ImperativePostgresDatabaseDemo>();
        services.AddTransient<IDemo, CSharpFunctionalPostgresDatabaseDemo>();
        services.AddTransient<IDemo, LanguageExtEffPostgresDatabaseDemo>();

        return services;
    }
}
