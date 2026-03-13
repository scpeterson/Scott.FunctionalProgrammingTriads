# ADR 0050: Streaming / Large Data Processing Triad Comparison

- Status: Accepted
- Date: 2026-03-10

## Context

Imperative systems often process large data sets by loading everything into memory, which obscures where streaming boundaries should exist.

The teaching path needs a triad that compares:

1. Imperative single-pass streaming with mutable accumulators
2. C# functional pipeline composition over chunked enumerable streams
3. LanguageExt pure fold-based stream policy

## Decision

Add a dedicated `StreamingLargeDataTriad` with three demos:

1. `imperative-streaming-large-data-comparison`
2. `csharp-streaming-large-data-comparison`
3. `langext-streaming-large-data-comparison`

Use a shared rule module (`StreamingLargeDataRules`) that provides:

- Input parsing and bounds checks
- Deterministic streamed data generation
- Equivalent aggregation contracts across all three variants

The LanguageExt variant remains pure and side-effect-free in its rule composition, while the demo boundary returns `DemoExecutionResult`.

## Consequences

### Positive

- Demonstrates memory-safe streaming style without full data materialization.
- Makes chunking and aggregation policy explicit and testable.
- Reinforces triad-style comparison for a practical production concern.

### Negative

- Introduces additional demos, run configs, and tests to maintain.
- Uses deterministic synthetic streams rather than external real-world data sources.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/StreamingLargeDataTriad/StreamingLargeDataRules.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/StreamingLargeDataTriad/ImperativeStreamingLargeDataComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/StreamingLargeDataTriad/CSharpStreamingLargeDataComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/StreamingLargeDataTriad/LanguageExtStreamingLargeDataComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Console/DemoServiceRegistration.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/StreamingLargeDataTriad/StreamingLargeDataTriadShould.cs`
- `.run/Triad - Streaming Large Data - 01 Imperative.run.xml`
- `.run/Triad - Streaming Large Data - 02 CSharp.run.xml`
- `.run/Triad - Streaming Large Data - 03 LanguageExt.run.xml`
