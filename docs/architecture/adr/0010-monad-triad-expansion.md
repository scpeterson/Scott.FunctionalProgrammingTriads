# ADR 0010: Monad Triad Expansion (Option, Either, Validation, Reader, State, IO-style)

- Status: Accepted
- Date: 2026-03-09

## Context

The project’s teaching goal is to help imperative programmers compare equivalent solutions across:

1. imperative C#
2. functional C#/.NET
3. functional LanguageExt

Earlier demos covered individual monad concepts, but monad coverage was inconsistent and not uniformly represented in triad form.

## Decision

Standardize monad-focused teaching demos as dedicated triads for:

- Option
- Either
- Validation
- Reader
- State
- IO-style effects

Each monad topic must include:

- one imperative comparison demo
- one C#/.NET functional comparison demo
- one LanguageExt demo

All three variants must solve the same scenario so code-volume and orchestration differences are clear.

For current package constraints (`LanguageExt.Core 4.4.9`), IO-style modeling uses `Eff` as the practical equivalent in this codebase.

## Alternatives Considered

- Keep monad content in a single aggregate demo class
  - Rejected because side-by-side comparison by monad is harder to discover, run, and test.
- Add only LanguageExt versions
  - Rejected because comparative teaching value depends on imperative and C# baselines.
- Delay until a LanguageExt version with direct IO type usage in this repo
  - Rejected because `Eff` provides an immediate IO-style effect model without blocking on package changes.

## Consequences

### Positive

- Consistent teaching structure across major monad categories.
- Better CLI discoverability and run-config support for each monad.
- Stronger automated coverage for monad examples.

### Negative

- More demo classes and run configurations to maintain.
- Some conceptual overlap between related triads (e.g., validation variants).
- IO naming requires explicit note that `Eff` is used in this package/version.

## References

- `Scott.FizzBuzz.Core/Demos/OptionMonadTriad/LanguageExtOptionMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/EitherMonadTriad/LanguageExtEitherMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/ValidationMonadTriad/LanguageExtValidationMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/ReaderMonadTriad/LanguageExtReaderMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/StateMonadTriad/LanguageExtStateMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/IOMonadTriad/LanguageExtIoMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Console/DemoServiceRegistration.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/ScaffoldedTriadsShould.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/OptionMonadTriad/OptionMonadTriadShould.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/EitherMonadTriad/EitherMonadTriadShould.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/ValidationMonadTriad/ValidationMonadTriadShould.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/ReaderMonadTriad/ReaderMonadTriadShould.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/StateMonadTriad/StateMonadTriadShould.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/IOMonadTriad/IoMonadTriadShould.cs`
