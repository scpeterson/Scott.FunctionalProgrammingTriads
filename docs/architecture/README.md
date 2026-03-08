# Architecture Overview

This repository is structured to teach imperative developers how equivalent problems can be expressed in:

1. Imperative C#
2. Functional style using core C#/.NET constructs
3. Functional style using LanguageExt

The architecture intentionally optimizes for comparison, repeatability, and testability.

## Goals

- Make tradeoffs visible between imperative and functional approaches.
- Keep functional logic pure where possible.
- Isolate side effects to explicit boundaries.
- Ensure demos are discoverable and runnable through a consistent CLI surface.

## Key Design Decisions

- Triad demo structure by topic (imperative/csharp/languageext).
- Output abstraction through `IOutput` and styled output support via `IStyledOutput`.
- Demo metadata contract (`Key`, `Category`, `Tags`, `Description`) for discoverability.
- Extraction of private demo logic into pure helper classes for direct unit testing.
- Persistence demos isolate text-store IO at explicit side-effect boundaries.
- LanguageExt async/sync effect demos standardize on `Aff`/`Eff` with pure transforms.
- PostgreSQL schema/data changes follow a SQL-first changelog workflow.
- Coverage thresholds in CI to prevent quality regressions.

## Primary Runtime Components

- `Scott.FizzBuzz.Console/Program.cs`
  - CLI entry point and DI setup.
- `Scott.FizzBuzz.Console/DemoRunner.cs`
  - Validates CLI contract, lists demos, dispatches execution by key.
- `Scott.FizzBuzz.Console/DemoServiceRegistration.cs`
  - Registers all demos in DI.
- `Scott.FizzBuzz.Core/Interfaces/IDemo.cs`
  - Runtime contract for all demos.
- `Scott.FizzBuzz.Core/Interfaces/IOutput.cs`
  - Side-effect boundary for text output.

## Decision Log

Architecture decisions are captured as ADRs under:

- `docs/architecture/adr`

Start with `0001` and read in order.

## Adding a New ADR

When introducing a significant architectural decision, add a new ADR file in:

- `docs/architecture/adr`

### Naming

- Use the next sequential number.
- Use kebab-case title.
- Format: `NNNN-short-decision-title.md`

Example:

- `0006-demo-description-style-guide.md`

### Required Sections

Each ADR should contain:

1. `Status`
2. `Date`
3. `Context`
4. `Decision`
5. `Alternatives Considered`
6. `Consequences`
7. `References`

### Status Values

Use one of:

- `Proposed`
- `Accepted`
- `Superseded`
- `Deprecated`

If superseded, include a pointer to the newer ADR in the old and new documents.

### Authoring Guidelines

- Keep it concise and specific to one decision.
- Capture tradeoffs, not just outcomes.
- Link concrete code paths and tests in `References`.
- Prefer updating docs in the same change that implements the decision.
