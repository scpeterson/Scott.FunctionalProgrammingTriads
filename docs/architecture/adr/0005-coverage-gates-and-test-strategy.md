# ADR 0005: Coverage Gates and Test Strategy

- Status: Accepted
- Date: 2026-03-03

## Context

As demos expanded, accidental coverage regressions became more likely.

The repository needed enforceable quality gates without overfitting tests to implementation details.

## Decision

Adopt layered test strategy:

- Pure helper unit tests (high precision)
- Demo behavior tests with recording output
- Runner/CLI contract tests
- Smoke tests across registered demos

Add CI coverage thresholds per project using Coverlet MSBuild integration and scoped module includes.

## Alternatives Considered

- No coverage thresholds
  - Rejected due to regression risk.
- Single global threshold across all modules
  - Rejected because it can hide weak spots in specific assemblies.

## Consequences

### Positive

- Detects regressions early in pull requests.
- Encourages extraction of testable pure logic.
- Keeps architecture decisions enforceable over time.

### Negative

- Slightly longer CI time.
- Threshold tuning may need periodic adjustment as code evolves.

## References

- `.github/workflows/ci.yml`
- `Scott.FizzBuzz.Core.Tests/Scott.FizzBuzz.Core.Tests.csproj`
- `Scott.FizzBuzz.Console.Tests/Scott.FizzBuzz.Console.Tests.csproj`
