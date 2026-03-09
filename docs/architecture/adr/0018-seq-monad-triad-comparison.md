# ADR 0018: Seq Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

Collection pipelines are central to functional programming; a dedicated Seq triad is needed for explicit comparison.

## Decision

Adopt a dedicated Seq monad triad:

1. `ImperativeSeqMonadComparisonDemo`
2. `CSharpSeqMonadComparisonDemo`
3. `LanguageExtSeqMonadComparisonDemo`

## Alternatives Considered

- Reuse aggregation demos only
  - Rejected because Seq-monad focus was not explicit.

## Consequences

### Positive

- Stronger sequence-composition teaching path.

### Negative

- Potential overlap with existing aggregation demos.

## References

- `Scott.FizzBuzz.Core/Demos/SeqMonadTriad/LanguageExtSeqMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/SeqMonadTriad/SeqMonadRules.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/SeqMonadTriad/SeqMonadTriadShould.cs`
