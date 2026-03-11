# ADR 0002: Output Abstraction via `IOutput`/`IStyledOutput`

- Status: Accepted
- Date: 2026-03-03

## Context

Demos historically wrote directly to `Console`, which made testing and side-effect control harder.

As the number of demos increased, output behavior needed to be standardized and testable.

## Decision

Introduce output interfaces:

- `IOutput` for basic line output
- `IStyledOutput` for color/style scopes

Refactor `OutputUtilities` and demos to route output through these interfaces.

Use `ConsoleOutput` as the production implementation.

Centralize test doubles for output contracts in a shared test utility module (`NullOutputSink`, `RecordingOutputSink`, `RecordingStyledOutputSink`) so output assertions remain consistent across demo test suites.

## Alternatives Considered

- Keep direct `Console` usage
  - Rejected due to poor testability and tighter coupling.
- Use logging framework as primary output abstraction
  - Rejected because demos are user-facing instructional output, not app telemetry.

## Consequences

### Positive

- Easier unit testing with recording/fake outputs.
- Clear side-effect boundary.
- Retains optional styling without forcing it on all outputs.

### Negative

- Slightly larger API surface.
- Requires constructor injection updates on demos.

## References

- `Scott.FizzBuzz.Core/Interfaces/IOutput.cs`
- `Scott.FizzBuzz.Core/Interfaces/IStyledOutput.cs`
- `Scott.FizzBuzz.Core/ConsoleOutput.cs`
- `Scott.FizzBuzz.Core/OutputUtilities.cs`
- `Scott.FizzBuzz.Core.Tests/TestUtilities/TestOutputSinks.cs`
