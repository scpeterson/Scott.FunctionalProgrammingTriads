# ADR 0012: Reader Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

Reader demonstrates environment/config dependency injection without threading context explicitly through each call.

## Decision

Adopt a dedicated Reader monad triad:

1. `ImperativeReaderComparisonDemo`
2. `CSharpReaderComparisonDemo`
3. `LanguageExtReaderMonadComparisonDemo`

Use a shared pricing-context scenario for parity.

## Alternatives Considered

- Keep Reader only in mixed monad demo files
  - Rejected due weaker discoverability and comparison clarity.

## Consequences

### Positive

- Makes context-threading tradeoff explicit.

### Negative

- Adds new demo surface area.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/ReaderMonadTriad/LanguageExtReaderMonadComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/ReaderMonadTriad/ReaderMonadRules.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/ReaderMonadTriad/ReaderMonadTriadShould.cs`
