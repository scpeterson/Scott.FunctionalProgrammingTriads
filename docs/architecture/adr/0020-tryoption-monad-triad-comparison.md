# ADR 0020: TryOption Monad Triad Comparison

- Status: Accepted
- Date: 2026-03-09

## Context

`TryOption<T>` models three outcomes in a single flow:

1. Success with value (`Some`)
2. Success without value (`None`)
3. Failure (`Fail`)

The demo set already included `Option` and `Try`, but not a side-by-side comparison that shows why combining both concerns is useful for imperative developers moving to functional composition.

## Decision

Add a dedicated TryOption triad under `Demos/TryOptionMonadTriad`:

1. `ImperativeTryOptionMonadComparisonDemo`
2. `CSharpTryOptionMonadComparisonDemo`
3. `LanguageExtTryOptionMonadComparisonDemo`

Use one shared rules class (`TryOptionMonadRules`) to keep scenario parity across all three variants.

## Alternatives Considered

- Cover TryOption only in existing Try/Option demos
  - Rejected because the three-state behavior is easy to miss without a focused comparison.

## Consequences

### Positive

- Clear demonstration of the `Some`/`None`/`Fail` model.
- Reduces conceptual jump from imperative exception/null handling to functional composition.

### Negative

- Additional demo/test/docs surface area to maintain.

## References

- `Scott.FizzBuzz.Core/Demos/TryOptionMonadTriad/ImperativeTryOptionMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/TryOptionMonadTriad/CSharpTryOptionMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/TryOptionMonadTriad/LanguageExtTryOptionMonadComparisonDemo.cs`
- `Scott.FizzBuzz.Core/Demos/TryOptionMonadTriad/TryOptionMonadRules.cs`
- `Scott.FizzBuzz.Core.Tests/Demos/TryOptionMonadTriad/TryOptionMonadTriadShould.cs`
- `Scott.FizzBuzz.Console/DemoServiceRegistration.cs`
