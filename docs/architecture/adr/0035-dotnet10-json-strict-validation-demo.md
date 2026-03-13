# ADR 0035: .NET 10 Strict JSON Validation Demo

- Status: Accepted
- Date: 2026-03-10

## Context

The project includes .NET 10-specific functional demonstrations and needs explicit coverage for strict JSON validation.

## Decision

Keep `fp-json-strict-validation` as a dedicated .NET 10 feature demo.

## Consequences

### Positive

- Demonstrates modern platform features in functional workflows.

### Negative

- Requires ongoing alignment with .NET version behavior.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/DotNet10Features/FpJsonStrictValidationDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/DotNet10Features/FpJsonStrictValidationLogic.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/FpJsonStrictValidationLogicShould.cs`
