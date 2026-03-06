# ADR 0003: Demo Discovery Metadata in `IDemo`

- Status: Accepted
- Date: 2026-03-03

## Context

As demo count grew, key-only listing became insufficient for discovery and learning flow.

Users needed richer filtering and context in `--list` output.

## Decision

Standardize demo metadata on `IDemo`:

- `Key`
- `Category`
- `Tags`
- `Description`

Update `DemoRunner` to include these fields in listing output.

## Alternatives Considered

- Keep metadata in external registry file
  - Rejected because it duplicates source of truth and drifts over time.
- Hardcode descriptions in runner
  - Rejected due to tight coupling and poor extensibility.

## Consequences

### Positive

- Better discoverability through CLI listing and tag filtering.
- Self-describing demos.
- Simplifies future automation/document generation from code metadata.

### Negative

- Requires consistent metadata maintenance in each demo.

## References

- `Scott.FizzBuzz.Core/Interfaces/IDemo.cs`
- `Scott.FizzBuzz.Console/DemoRunner.cs`
