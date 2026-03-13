# ADR 0028: Applicative Validation Demo

- Status: Accepted
- Date: 2026-03-10

## Context

Applicative validation is a core teaching point for error accumulation in functional pipelines.

## Decision

Keep a dedicated `applicative-validation` demo focused on reusable validation combinators and accumulated failures.

## Consequences

### Positive

- Highlights validation composition independent of larger triads.

### Negative

- Requires synchronizing with validation utility evolution.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/ValidationFoundations/ApplicativeValidationDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/ApplicativeValidationExample/ApplicativeValidationDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/ApplicativeValidationDemoEntryShould.cs`
