# ADR 0013: State Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

State transitions are commonly implemented with mutation or explicit state threading.
State monad comparison clarifies how transition composition can be expressed functionally.

## Decision

Adopt a dedicated State monad triad:

1. `ImperativeStateComparisonDemo`
2. `CSharpStateComparisonDemo`
3. `LanguageExtStateMonadComparisonDemo`

Use one shared transition model and operation plan.

## Alternatives Considered

- Rely on Schrödinger/state snippets only
  - Rejected as too indirect for imperative-vs-functional comparison.

## Consequences

### Positive

- Clear state-threading contrast.

### Negative

- Extra demos and configuration entries.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/StateMonadTriad/LanguageExtStateMonadComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/StateMonadTriad/StateMonadRules.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/StateMonadTriad/StateMonadTriadShould.cs`
