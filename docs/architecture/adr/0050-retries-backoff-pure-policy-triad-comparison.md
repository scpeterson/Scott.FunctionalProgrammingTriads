# ADR 0050: Retries + Backoff as Pure Policy Triad Comparison

- Status: Accepted
- Date: 2026-03-10

## Context

Retry logic is often implemented with `try/catch`, sleeping threads, and mutable counters mixed directly with side effects. That makes policy behavior hard to test and reason about.

The teaching path needs a triad that demonstrates how retry and backoff can be modeled as pure policy:

1. Imperative orchestration with mutable state
2. C# functional pipeline with explicit composition
3. LanguageExt functional pipeline with pure transformations

## Decision

Add a dedicated `RetryBackoffTriad` with three demos:

1. `imperative-retry-backoff-comparison`
2. `csharp-retry-backoff-comparison`
3. `langext-retry-backoff-comparison`

Add `RetryBackoffRules` as the shared pure policy module:

- Policy resolution (`exponential`, `linear`)
- Input parsing for failures-before-success
- Delay calculation per retry attempt
- Deterministic execution simulation with no waiting/sleeping side effects

The LanguageExt variant remains pure and returns success/failure as `Either<string, Unit>` without output side effects.

## Consequences

### Positive

- Demonstrates resilient retry behavior without coupling to timers or I/O.
- Makes backoff schedules deterministic and unit-testable.
- Highlights how imperative and functional styles differ while using the same policy contract.

### Negative

- Adds another triad and run-configuration surface area to maintain.
- Keeps simulated retry semantics separate from real runtime scheduling concerns.

## References

- `Scott.FizzBuzz.Core/Demos/RetryBackoffTriad/RetryBackoffRules.cs`
- `Scott.FizzBuzz.Core/Demos/RetryBackoffTriad/ImperativeRetryBackoffComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/RetryBackoffTriad/CSharpRetryBackoffComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/RetryBackoffTriad/LanguageExtRetryBackoffComparisonDemo.cs`
- `Scott.FizzBuzz.Console/DemoServiceRegistration.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/RetryBackoffTriad/RetryBackoffTriadShould.cs`
- `.run/Triad - Retries Backoff - 01 Imperative.run.xml`
- `.run/Triad - Retries Backoff - 02 CSharp.run.xml`
- `.run/Triad - Retries Backoff - 03 LanguageExt.run.xml`
