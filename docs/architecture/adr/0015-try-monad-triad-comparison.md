# ADR 0015: Try Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

`Try` demonstrates exception capture as values, enabling compositional error handling without exception-driven orchestration.

## Decision

Adopt a dedicated Try monad triad:

1. `ImperativeTryMonadComparisonDemo`
2. `CSharpTryMonadComparisonDemo`
3. `LanguageExtTryMonadComparisonDemo`

## Alternatives Considered

- Keep Try coverage only in exception-boundary demos
  - Rejected because Try-specific composition was not explicit.

## Consequences

### Positive

- Better demonstration of exception-to-value flow.

### Negative

- Additional overlapping examples with exception demos.

## References

- `Scott.FizzBuzz.Core/Demos/TryMonadTriad/LanguageExtTryMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/TryMonadTriad/TryMonadRules.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/TryMonadTriad/TryMonadTriadShould.cs`
