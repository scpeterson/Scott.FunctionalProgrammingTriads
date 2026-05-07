# ADR 0055: Resource Cleanup Boundaries Triad Comparison

- Status: Accepted
- Date: 2026-05-07

## Context

Resource usage often hides cleanup risk behind happy-path examples. Imperative code usually reaches for `try/finally` or `using`, while more functional styles try to centralize acquisition and release so the business path stays clearer.

The teaching path needs a triad that compares those trade-offs directly.

## Decision

Add a dedicated `ResourceCleanupTriad` with three demos:

1. `imperative-resource-cleanup-comparison`
2. `csharp-resource-cleanup-comparison`
3. `langext-resource-cleanup-comparison`

Add `ResourceCleanupRules` as the shared deterministic model:

- scenario resolution (`success`, `fail`)
- explicit acquire / write / release trace steps
- a success result and a failure result that both preserve cleanup trace visibility

The triad uses a fake audit resource so cleanup guarantees can be tested without file-system or database dependencies.

## Consequences

### Positive

- Makes cleanup guarantees visible on both success and failure.
- Gives a direct comparison between inline `try/finally`, a plain C# scoped helper, and a LanguageExt `Either` pipeline inside the same cleanup boundary.
- Strengthens the repository's effect-boundary teaching with a smaller, deterministic example than the database triads.

### Negative

- Adds another infrastructure-focused triad to maintain.
- Uses a simulated resource instead of a live file or database handle, so the example stays conceptual rather than operational.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/ResourceCleanupTriad/ResourceCleanupRules.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/ResourceCleanupTriad/ImperativeResourceCleanupComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/ResourceCleanupTriad/CSharpResourceCleanupComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/ResourceCleanupTriad/LanguageExtResourceCleanupComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/ResourceCleanupTriad/ResourceCleanupTriadShould.cs`
