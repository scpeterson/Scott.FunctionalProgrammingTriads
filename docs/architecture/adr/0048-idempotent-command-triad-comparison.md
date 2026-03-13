# ADR 0048: Idempotent Command Triad Comparison

- Status: Accepted
- Date: 2026-03-10

## Context

Idempotency is a common backend requirement and a good example of side-effect control and state transition boundaries.

## Decision

Maintain a dedicated Idempotent Command triad:

1. `imperative-idempotent-command`
2. `csharp-idempotent-command`
3. `langext-idempotent-command`

Use a shared seeded set of processed command IDs in the environment for comparable behavior.

## Consequences

### Positive

- Demonstrates duplicate detection and command handling in three styles.
- Reinforces pure decision logic with explicit state updates.

### Negative

- Requires careful consistency between environment seed and demo expectations.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/Shared/IFunctionalDemoEnvironment.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/Shared/InMemoryFunctionalDemoEnvironment.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/IdempotentCommandTriad/IdempotentCommandRules.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/IdempotentCommandTriad/ImperativeIdempotentCommandComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/IdempotentCommandTriad/CSharpIdempotentCommandComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/IdempotentCommandTriad/LanguageExtIdempotentCommandComparisonDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/IdempotentCommandTriad/IdempotentCommandTriadShould.cs`
