# ADR 0058: Configuration Source Acquisition Triad Comparison

## Status

Accepted.

## Context

The configuration pipeline now has distinct slices for:

1. loading a normalized raw configuration shape from key/value settings
2. decoding typed environment, port, and log-level values
3. validating startup and business constraints

There was still one missing front-edge step: showing how those key/value settings are acquired from an external-style source before loading begins.

## Decision

Add a dedicated source-acquisition triad:

- `imperative-startup-config-source-comparison`
- `csharp-startup-config-source-comparison`
- `langext-startup-config-source-comparison`

This triad acquires a canonical `StartupSettingSource` from external-style keys such as:

- `TRIADS_APP_NAME`
- `TRIADS_ENVIRONMENT`
- `TRIADS_DATABASE_URL` / `TRIADS_DB_URL`
- `TRIADS_PORT` / `TRIADS_APP_PORT`
- `TRIADS_LOG_LEVEL`

It intentionally stops at a canonical in-memory source dictionary and leaves later loading, typed decoding, and startup validation to the next slices.

## Consequences

Positive:

- the configuration story now has a complete staged pipeline from external source to startup execution
- external key aliases and canonicalization are taught separately from raw loading and typed decoding
- later comparisons stay narrower because acquisition concerns are isolated here

Trade-offs:

- configuration concepts are now split across several adjacent triads
- readers need docs guidance to understand the boundaries between source acquisition, raw loading, and typed decoding

## Notes

This triad is about acquisition and canonical key mapping, not about parsing typed values or enforcing startup rules. Those concerns remain in the later configuration slices.
