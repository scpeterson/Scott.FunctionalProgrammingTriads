# ADR 0009: Option Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-08

## Context

The project teaches imperative developers through side-by-side triads:

1. Imperative C#
2. Functional C#/.NET without LanguageExt-heavy abstractions
3. Functional LanguageExt

We already had a null-handling triad, but we needed a dedicated Option-monad comparison that makes the cost of manual null orchestration explicit in imperative/C# variants, then contrasts it with LanguageExt `Option` composition.

## Decision

Add a dedicated Option Monad triad under `Demos/OptionMonadTriad` with one class per file:

- `ImperativeOptionComparisonDemo`
- `CSharpOptionComparisonDemo`
- `LanguageExtOptionMonadComparisonDemo`

Use a shared optional-domain scenario (customer/profile/email/domain/segment) so each variant solves the same problem.

Design constraints:

- Imperative variant uses explicit branching and null checks.
- C# functional variant uses nullable-reference composition and helper plumbing to emulate bind behavior.
- LanguageExt variant uses `Option` pipeline composition (`Optional`, `Bind`, `Map`, `Filter`, `ToEither`) with minimal orchestration code.

## Alternatives Considered

- Extend existing null/option triad only
  - Rejected because it did not sufficiently demonstrate Option-monad-specific composition costs and benefits.
- Add only a LanguageExt demo
  - Rejected because teaching goal requires side-by-side contrast against imperative and C#-only approaches.

## Consequences

### Positive

- Clear teaching artifact for why `Option` composition reduces orchestration complexity.
- Reusable scenario data for consistent comparison across paradigms.
- Better coverage for monad-focused learning path and run configurations.

### Negative

- More demo surface area to maintain.
- Some conceptual overlap with null-handling triad requires clear demo naming and tags.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/OptionMonadTriad/ImperativeOptionComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/OptionMonadTriad/CSharpOptionComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/OptionMonadTriad/LanguageExtOptionMonadComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/OptionMonadTriad/CSharpNullableBindExtensions.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/OptionMonadTriad/OptionMonadSampleData.cs`
- `Scott.FunctionalProgrammingTriads.Console/DemoServiceRegistration.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/OptionMonadTriad/OptionMonadTriadShould.cs`
