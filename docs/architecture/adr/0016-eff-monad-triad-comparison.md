# ADR 0016: Eff Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

`Eff` is the primary sync-effect abstraction used in this codebase for LanguageExt effect boundaries.

## Decision

Adopt a dedicated Eff monad triad:

1. `ImperativeEffMonadComparisonDemo`
2. `CSharpEffMonadComparisonDemo`
3. `LanguageExtEffMonadComparisonDemo`

## Alternatives Considered

- Rely only on existing Eff-heavy demos (database/async)
  - Rejected because they mix concerns beyond a monad-focused comparison.

## Consequences

### Positive

- Clear sync-effect comparison by paradigm.

### Negative

- More demos to keep behavior-aligned.

## References

- `Scott.FizzBuzz.Core/Demos/EffMonadTriad/LanguageExtEffMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/EffMonadTriad/EffMonadRules.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/EffMonadTriad/EffMonadTriadShould.cs`
