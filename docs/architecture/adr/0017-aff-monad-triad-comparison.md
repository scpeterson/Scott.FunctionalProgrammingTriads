# ADR 0017: Aff Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

`Aff` is used for async effect composition in LanguageExt. A dedicated triad clarifies async orchestration differences.

## Decision

Adopt a dedicated Aff monad triad:

1. `ImperativeAffMonadComparisonDemo`
2. `CSharpAffMonadComparisonDemo`
3. `LanguageExtAffMonadComparisonDemo`

Use safe sync-bridge execution where necessary in synchronous demo entry points.

## Alternatives Considered

- Use only async workflow demos for Aff
  - Rejected because monad-focused comparison was not isolated.

## Consequences

### Positive

- Explicit async effect comparison with minimal noise.

### Negative

- Requires careful sync-bridge handling in `IDemo.Run`.

## References

- `Scott.FizzBuzz.Core/Demos/AffMonadTriad/LanguageExtAffMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/AffMonadTriad/AffMonadRules.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/AffMonadTriad/AffMonadTriadShould.cs`
