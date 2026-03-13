using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Demos.CurryingTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConcurrencySafetyTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.CompositionRootTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.DomainWorkflowTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.EventSourcingLiteTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationValidationStartupTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.EitherMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.EffMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.AffMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.IdempotentCommandTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.IOMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.NullOptionTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.OptionMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ParseValidateTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ReaderMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.StreamingLargeDataTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.RetryBackoffTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.SeqMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.StateMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.TryMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.TryOptionMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ValidationMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.ValidationAccumulationTriad;
using Scott.FunctionalProgrammingTriads.Core.Demos.WriterMonadTriad;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos;

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
            new ImperativeCurryingComparisonDemo(),
            new CSharpCurryingComparisonDemo(),
            new LanguageExtCurryingComparisonDemo(),
            new ImperativeCompositionRootComparisonDemo(),
            new CSharpCompositionRootComparisonDemo(),
            new LanguageExtCompositionRootComparisonDemo(),
            new ImperativeDomainWorkflowComparisonDemo(),
            new CSharpDomainWorkflowComparisonDemo(),
            new LanguageExtDomainWorkflowComparisonDemo(),
            new ImperativeEventSourcingLiteComparisonDemo(),
            new CSharpEventSourcingLiteComparisonDemo(),
            new LanguageExtEventSourcingLiteComparisonDemo(),
            new ImperativeConfigurationValidationStartupComparisonDemo(),
            new CSharpConfigurationValidationStartupComparisonDemo(),
            new LanguageExtConfigurationValidationStartupComparisonDemo(),
            new ImperativeConcurrencySafetyComparisonDemo(),
            new CSharpConcurrencySafetyComparisonDemo(),
            new LanguageExtConcurrencySafetyComparisonDemo(),
            new ImperativeStreamingLargeDataComparisonDemo(),
            new CSharpStreamingLargeDataComparisonDemo(),
            new LanguageExtStreamingLargeDataComparisonDemo(),
            new ImperativeRetryBackoffComparisonDemo(),
            new CSharpRetryBackoffComparisonDemo(),
            new LanguageExtRetryBackoffComparisonDemo(),
            new ImperativeIdempotentCommandComparisonDemo(),
            new CSharpIdempotentCommandComparisonDemo(),
            new LanguageExtIdempotentCommandComparisonDemo(),
            new ImperativeOptionComparisonDemo(),
            new CSharpOptionComparisonDemo(),
            new LanguageExtOptionMonadComparisonDemo(),
            new ImperativeEitherComparisonDemo(),
            new CSharpEitherComparisonDemo(),
            new LanguageExtEitherMonadComparisonDemo(),
            new ImperativeValidationMonadComparisonDemo(),
            new CSharpValidationMonadComparisonDemo(),
            new LanguageExtValidationMonadComparisonDemo(),
            new ImperativeReaderComparisonDemo(),
            new CSharpReaderComparisonDemo(),
            new LanguageExtReaderMonadComparisonDemo(),
            new ImperativeStateComparisonDemo(),
            new CSharpStateComparisonDemo(),
            new LanguageExtStateMonadComparisonDemo(),
            new ImperativeIoComparisonDemo(),
            new CSharpIoComparisonDemo(),
            new LanguageExtIoMonadComparisonDemo(),
            new ImperativeTryMonadComparisonDemo(),
            new CSharpTryMonadComparisonDemo(),
            new LanguageExtTryMonadComparisonDemo(),
            new ImperativeTryOptionMonadComparisonDemo(),
            new CSharpTryOptionMonadComparisonDemo(),
            new LanguageExtTryOptionMonadComparisonDemo(),
            new ImperativeEffMonadComparisonDemo(),
            new CSharpEffMonadComparisonDemo(),
            new LanguageExtEffMonadComparisonDemo(),
            new ImperativeAffMonadComparisonDemo(),
            new CSharpAffMonadComparisonDemo(),
            new LanguageExtAffMonadComparisonDemo(),
            new ImperativeSeqMonadComparisonDemo(),
            new CSharpSeqMonadComparisonDemo(),
            new LanguageExtSeqMonadComparisonDemo(),
            new ImperativeWriterMonadComparisonDemo(),
            new CSharpWriterMonadComparisonDemo(),
            new LanguageExtWriterMonadComparisonDemo(),
            new ImperativeParseValidateDemo(),
            new CSharpParseValidatePipelineDemo(),
            new LanguageExtEitherParseValidateDemo(),
            new ImperativeValidationFirstErrorDemo(),
            new CSharpValidationErrorListDemo(),
            new LanguageExtValidationAccumulateDemo()
        };

        foreach (var demo in demos)
        {
            var (name, number) = demo.Key.Contains("currying-comparison", StringComparison.Ordinal)
                ? ("vip", "100")
                : demo.Key.Contains("concurrency-safety-comparison", StringComparison.Ordinal)
                    ? ("scenario", "1000")
                : demo.Key.Contains("streaming-large-data-comparison", StringComparison.Ordinal)
                    ? ("1000", "128")
                : demo.Key.Contains("retry-backoff-comparison", StringComparison.Ordinal)
                    ? ("exp", "2")
                : demo.Key.Contains("startup-config-validation-comparison", StringComparison.Ordinal)
                    ? ("prod", "20")
                : ("Scott", "21");

            var result = demo.Run(name, number);
            result.ShouldBeRight();
        }
    }
}
