# ADR 0052: Concurrency Safety Triad Comparison

- Status: Accepted
- Date: 2026-03-11

## Context

Imperative code that mutates shared state can lose updates when multiple writers interleave non-atomic read-modify-write operations.

To teach imperative developers functional migration, this concern should be shown side-by-side in triad form:

1. Imperative mutable update flow
2. C#/.NET atomic update flow
3. LanguageExt pure immutable update model

## Decision

Add a dedicated `ConcurrencySafetyTriad` with three demos:

1. `imperative-concurrency-safety-comparison`
2. `csharp-concurrency-safety-comparison`
3. `langext-concurrency-safety-comparison`

Add shared rules in `ConcurrencySafetyRules` to provide:

- Iteration input validation
- Deterministic unsafe interleaving simulation (lost updates)
- Atomic C# update simulation using `Interlocked`
- Pure LanguageExt fold-based update simulation

The LanguageExt variant remains side-effect free and validates that no updates are lost.

## Consequences

### Positive

- Demonstrates a practical concurrency failure mode in a deterministic way.
- Makes safety tradeoffs explicit between mutable shared state and pure immutable transformations.
- Adds testable, repeatable policy logic rather than thread-scheduling-dependent demos.

### Negative

- Simulation illustrates concurrency semantics without running real multi-threaded workloads.
- Adds more demo and configuration surface area to maintain.

## References

- `Scott.FizzBuzz.Core/Demos/ConcurrencySafetyTriad/ConcurrencySafetyRules.cs`
- `Scott.FizzBuzz.Core/Demos/ConcurrencySafetyTriad/ImperativeConcurrencySafetyComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/ConcurrencySafetyTriad/CSharpConcurrencySafetyComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/ConcurrencySafetyTriad/LanguageExtConcurrencySafetyComparisonDemo.cs`
- `Scott.FizzBuzz.Console/DemoServiceRegistration.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/ConcurrencySafetyTriad/ConcurrencySafetyTriadShould.cs`
- `.run/Triad - Concurrency Safety - 01 Imperative.run.xml`
- `.run/Triad - Concurrency Safety - 02 CSharp.run.xml`
- `.run/Triad - Concurrency Safety - 03 LanguageExt.run.xml`
