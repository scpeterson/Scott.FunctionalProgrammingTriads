# ADR 0046: Composition Root Triad Comparison

- Status: Accepted
- Date: 2026-03-10

## Context

Imperative teams often resolve dependencies ad hoc at call sites. The teaching path needs a side-by-side comparison of dependency wiring styles.

## Decision

Maintain a dedicated Composition Root triad:

1. `imperative-composition-root`
2. `csharp-composition-root`
3. `langext-composition-root`

Use shared environment contracts for consistent dependency lookup behavior.

## Consequences

### Positive

- Demonstrates explicit dependency flow from imperative to functional styles.
- Makes composition-root concerns concrete without adding infrastructure complexity.

### Negative

- Adds additional demo surface area to maintain.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/Shared/IFunctionalDemoEnvironment.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/Shared/InMemoryFunctionalDemoEnvironment.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/CompositionRootTriad/CompositionRootRules.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/CompositionRootTriad/ImperativeCompositionRootComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/CompositionRootTriad/CSharpCompositionRootComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/CompositionRootTriad/LanguageExtCompositionRootComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/CompositionRootTriad/CompositionRootTriadShould.cs`
