# ADR 0019: Writer Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

Writer monad allows accumulating logs alongside computation without interleaving logging side effects in orchestration.

## Decision

Adopt a dedicated Writer monad triad:

1. `ImperativeWriterMonadComparisonDemo`
2. `CSharpWriterMonadComparisonDemo`
3. `LanguageExtWriterMonadComparisonDemo`

## Alternatives Considered

- Keep Writer only in mixed monad demos
  - Rejected because comparison clarity and discoverability were weaker.

## Consequences

### Positive

- Clean separation of computed value and accumulated log context.

### Negative

- Adds additional monad-specific examples to maintain.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/WriterMonadTriad/LanguageExtWriterMonadComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/WriterMonadTriad/WriterMonadRules.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/WriterMonadTriad/WriterMonadTriadShould.cs`
