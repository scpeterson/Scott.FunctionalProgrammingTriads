# ADR 0001: Demo Triad Structure

- Status: Accepted
- Date: 2026-03-03

## Context

The project goal is to help imperative programmers understand functional programming by comparing equivalent approaches.

Without a consistent structure, comparisons become ad hoc and difficult to teach.

## Decision

Organize demo topics as triads where practical:

1. Imperative
2. C# functional style
3. LanguageExt functional style

Use consistent metadata and runnable keys so each variant can be listed and executed independently.

Keep console presentation comparable across triad members, even when the LanguageExt variant internally computes through pure functions and only renders output at the outer demo boundary.

## Alternatives Considered

- Single "best" implementation per topic
  - Rejected because it removes side-by-side learning value.
- Separate repositories per paradigm
  - Rejected due to operational overhead and weaker discoverability.

## Consequences

### Positive

- Clear comparative learning path.
- Easier to add and review new topics.
- Better run-configuration support for side-by-side execution.
- Makes behavioral comparison easier because each variant reports success and failure in a similar user-facing shape.

### Negative

- More files/classes to maintain.
- Requires discipline to keep parity across triad variants.

## References

- `Scott.FizzBuzz.Core/Demos`
- `Scott.FizzBuzz.Console/DemoServiceRegistration.cs`
- `.run/Triad - *.run.xml`
