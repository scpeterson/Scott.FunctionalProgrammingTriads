# Demo Author Checklist

Use this checklist when adding or reshaping a demo so the repository stays consistent and easy to navigate.

## Before You Start

- Confirm the topic actually teaches a meaningful contrast for imperative developers.
- Decide whether the topic belongs in the active demo catalog or should remain archival.
- Prefer extending the existing triad structure when the topic fits naturally.

## Demo Structure

- Add one class per file.
- Put multi-file demos in a dedicated folder under `Scott.FunctionalProgrammingTriads.Core/Demos`.
- Keep imperative, plain C#, and LanguageExt variants separate when the topic is taught as a triad.
- Keep shared kernels plain .NET where imperative and plain C# demos depend on them.
- Keep LanguageExt-specific lifting in LanguageExt-only rule or adapter files.

## Output and Execution

- Keep rule functions pure where practical.
- Render user-facing output at the demo boundary through `IOutput`.
- Return `DemoExecutionResult` from `IDemo.Run`.
- Keep success and failure messaging aligned with the rest of the catalog where possible.

## Metadata

- Add a stable `Key`.
- Set `Category`, `Tags`, and `Description`.
- Make the description useful in `--list`, not just technically correct.

## Tests

- Add unit tests for the new demo logic.
- Add negative-path tests where failure is part of the teaching story.
- If the demo is triad-based, test all three members enough to preserve the intended contrast.
- If output matters, prefer the shared recording output sinks in test utilities.

## Documentation

- Add or update an ADR when the demo introduces a meaningful new architectural or teaching decision.
- Add the ADR to docs navigation and index pages.
- Update the learning-path docs if the new demo should be part of the recommended progression.
- Update the README if the new demo changes the best starting path for readers.

## Tooling and Visibility

- Add a run configuration if the demo should be easy to launch directly from Rider.
- Make sure the file layout is visible in the solution where that improves discoverability.
- Run `dotnet build`, relevant `dotnet test` commands, and `mkdocs build --strict` before finishing.

## Final Review

Ask these questions before considering the change done:

1. Does the demo make the comparison clearer for an imperative programmer?
2. Does the plain C# variant remain plain C# rather than LanguageExt in disguise?
3. Are side effects isolated to the right boundary?
4. Is the demo easy to discover from the CLI and docs?
