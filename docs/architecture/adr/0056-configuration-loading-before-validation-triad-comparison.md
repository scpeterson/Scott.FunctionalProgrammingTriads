# ADR 0056: Configuration Loading Before Validation Triad Comparison

## Status

Accepted

## Context

The repository already compares how different styles validate startup configuration.

That leaves one upstream step unexplained: taking stringly runtime inputs such as environment variables or settings dictionaries and turning them into a raw configuration shape before validation begins.

This is a distinct teaching problem because it introduces:

- required-setting detection
- alias handling such as `DB_URL` vs `DATABASE_URL`
- normalization such as trimming and lower-casing
- the choice between fail-fast and error accumulation while loading

## Decision

Add a new triad that compares three approaches to loading raw startup configuration from key/value settings:

- imperative fail-fast loading
- composed C# loading with accumulated missing-setting errors
- LanguageExt validation-based loading with alias support and normalized values

The loaded output remains intentionally simple so it can feed conceptually into the existing startup configuration validation triad.

## Consequences

Positive:

- makes the startup story more complete by covering the step before validation
- gives a realistic example of stringly input handling without adding infrastructure dependencies
- creates a clean bridge to the Haskell companion project, where pure loading before validation is especially natural

Tradeoffs:

- introduces another configuration-focused triad, so the docs must explain how it differs from validation-at-startup
- keeps the example small and deterministic, which means it does not model secret managers or real host environments directly
