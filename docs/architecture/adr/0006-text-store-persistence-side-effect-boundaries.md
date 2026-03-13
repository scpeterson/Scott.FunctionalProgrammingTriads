# ADR 0006: Text-Store Persistence Side-Effect Boundaries

- Status: Accepted
- Date: 2026-03-05

## Context

The project now includes a database-focused triad to show imperative developers how persistence can be handled across:

1. Imperative C#
2. Functional C# without LanguageExt-heavy effects
3. Functional LanguageExt with explicit effect boundaries

Persistence is inherently side-effecting. Without a clear decision, demos can blur pure transformation logic with file/database IO, reducing teaching value.

## Decision

Model the first persistence demos using a text-based store and separate concerns by style:

- Imperative variant may keep inline mutation and IO to demonstrate baseline practice.
- C# functional variant must keep parsing/validation/transforms pure and isolate IO in explicit boundary methods.
- LanguageExt variant must express IO at the `Eff` boundary while keeping domain transforms pure (`Either`/`Option`/pure functions).

Use a shared pure codec for parsing and serialization so all variants can compare orchestration style without duplicating domain rules.

## Alternatives Considered

- Start directly with an RDBMS
  - Rejected for this stage due to setup overhead and noise for FP-first learning goals.
- Keep one persistence implementation only
  - Rejected because it removes side-by-side comparison value.
- Put all persistence logic inside `Eff` across all variants
  - Rejected because the C# functional variant intentionally demonstrates a non-LanguageExt-heavy path.

## Consequences

### Positive

- Teaches side-effect boundaries explicitly in a common "database work" scenario.
- Reuses pure parsing/upsert logic across variants.
- Prepares a smooth path to a future RDBMS-backed triad without changing the conceptual model.

### Negative

- Adds more demo classes and supporting files.
- Text store is intentionally simplified and not production persistence.
- Requires keeping behavior parity across three implementations.

## References

- `Scott.FunctionalProgrammingTriads.Core/Demos/DatabaseTextStoreTriad/ImperativeTextStoreDatabaseDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/DatabaseTextStoreTriad/CSharpFunctionalTextStoreDatabaseDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/DatabaseTextStoreTriad/LanguageExtEffTextStoreDatabaseDemo.cs`
- `Scott.FunctionalProgrammingTriads.Core/Demos/DatabaseTextStoreTriad/TextStoreRecordCodec.cs`
- `Scott.FunctionalProgrammingTriads.Console/DemoServiceRegistration.cs`
- `Scott.FunctionalProgrammingTriads.Core.Tests/Demos/DatabaseTextStoreTriad/DatabaseTextStoreTriadShould.cs`
