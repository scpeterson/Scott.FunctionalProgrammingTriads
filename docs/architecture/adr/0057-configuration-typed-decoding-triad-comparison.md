# ADR 0057: Configuration Typed Decoding Triad Comparison

## Status

Accepted.

## Context

The configuration-loading triad now shows how stringly key/value settings become a normalized `RawStartupConfig`. The startup-validation triad and startup feature triad each assume the next step already has typed values available where needed.

There was still a missing teaching slice between those two stages:

1. load raw strings
2. decode strongly typed in-memory values
3. validate startup and business constraints

Without that middle step, the progression from raw settings to validated startup behavior looked more magical than it really is.

## Decision

Add a new triad dedicated to typed configuration decoding:

- `imperative-startup-config-decoding-comparison`
- `csharp-startup-config-decoding-comparison`
- `langext-startup-config-decoding-comparison`

This triad keeps the input shape as a loaded `RawStartupConfig`, then decodes:

- environment strings into a typed environment value
- port text into an integer port
- log-level text into a typed log-level value

The variants differ in how they surface failures:

- imperative C#: fail fast on the first decode error
- functional C#: accumulate all decode errors in one pass
- LanguageExt: use `Validation` to decode typed values applicatively

## Consequences

Positive:

- the configuration story now has a cleaner staged progression
- raw loading, typed decoding, and startup validation are separated explicitly
- the typed-decoding slice becomes a natural bridge to later startup rules and effectful startup features

Trade-offs:

- the repo now has another closely related configuration triad to navigate
- some concepts overlap intentionally with the startup-validation demos, so the docs need to explain the boundaries clearly

## Notes

The new triad is intentionally about type conversion and shape interpretation, not business validation. It should stay focused on decoding concerns and leave higher-level startup rules to the later startup-validation and startup-feature triads.
