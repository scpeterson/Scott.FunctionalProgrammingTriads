# ADR 0011: Validation Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

Validation needs a dedicated comparison to illustrate accumulating errors versus first-error imperative flow.

## Decision

Adopt a dedicated Validation monad triad:

1. `ImperativeValidationMonadComparisonDemo`
2. `CSharpValidationMonadComparisonDemo`
3. `LanguageExtValidationMonadComparisonDemo`

LanguageExt variant uses applicative validation composition with shared validation rules.

## Alternatives Considered

- Reuse only existing validation demos
  - Rejected because monad-focused comparison was not explicit enough.

## Consequences

### Positive

- Stronger teaching value for accumulated validation errors.
- Shared rule set keeps behavior parity.

### Negative

- Adds overlap with older validation examples.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/ValidationMonadTriad/LanguageExtValidationMonadComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/ValidationMonadTriad/ValidationMonadRules.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/ValidationMonadTriad/ValidationMonadTriadShould.cs`
