# ADR 0010: Either Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

The project teaches imperative developers by comparing imperative, C# functional, and LanguageExt approaches for the same workflow.
A dedicated Either triad is needed to show explicit error-channel modeling versus exceptions/manual branching.

## Decision

Adopt a dedicated Either monad triad:

1. `ImperativeEitherComparisonDemo`
2. `CSharpEitherComparisonDemo`
3. `LanguageExtEitherMonadComparisonDemo`

Use one shared scenario and rules so differences are in orchestration, not business logic.

## Alternatives Considered

- Keep Either only inside generic demos
  - Rejected because it weakens direct side-by-side comparison.

## Consequences

### Positive

- Clear fail-fast comparison between paradigms.
- Better discoverability and testability.

### Negative

- Additional classes/tests/run configs to maintain.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/EitherMonadTriad/LanguageExtEitherMonadComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/EitherMonadTriad/EitherMonadRules.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/EitherMonadTriad/EitherMonadTriadShould.cs`
