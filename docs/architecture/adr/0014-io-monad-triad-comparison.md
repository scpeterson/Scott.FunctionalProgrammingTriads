# ADR 0014: IO Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

The teaching path needs an IO-oriented comparison where side effects are explicit at the boundary.
In this codebase version (`LanguageExt.Core 4.4.9`), `Eff` is the practical IO-style abstraction.

## Decision

Adopt a dedicated IO-style triad:

1. `ImperativeIoComparisonDemo`
2. `CSharpIoComparisonDemo`
3. `LanguageExtIoMonadComparisonDemo` (implemented via `Eff`)

## Alternatives Considered

- Delay until direct IO type is available in project API surface
  - Rejected because `Eff` is sufficient for current teaching and package constraints.

## Consequences

### Positive

- Side-effect boundaries are explicit and testable.

### Negative

- IO naming requires version note (`Eff` equivalent in this repo).

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/IOMonadTriad/LanguageExtIoMonadComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/IOMonadTriad/IoMonadRules.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/IOMonadTriad/IoMonadTriadShould.cs`
