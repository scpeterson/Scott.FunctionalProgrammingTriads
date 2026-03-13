# Repo Health Notes

This document captures maintenance conventions that help keep the teaching structure coherent as the repository grows.

## Active vs Archival Code

- Active demos belong under `Scott.FunctionalProgrammingTriads.Core/Demos`.
- Archival or superseded examples belong under `Scott.FunctionalProgrammingTriads.Core/Legacy`.
- Dead code should be removed instead of being moved to `Legacy`.

## Triad Boundaries

When a topic is taught as a triad, keep the contrast honest:

- imperative demos should use plain imperative techniques
- plain C# demos should use core language and .NET techniques
- LanguageExt demos may use LanguageExt-specific abstractions

Shared kernels that serve imperative and plain C# demos should prefer plain .NET types.
LanguageExt-specific lifting should stay in LanguageExt-only adapter or rule files.

## Side-Effect Boundaries

- Pure rule functions should stay free of presentation and console output.
- Demo classes are the presentation boundary and return `DemoExecutionResult`.
- File system, database, and async effects should be isolated to explicit boundaries rather than scattered through rule logic.

## Intentional Duplication

Some duplication is kept on purpose when it improves the teaching comparison between imperative, plain C#, and LanguageExt approaches.
Avoid deduplicating demo code if doing so would blur the differences the repository is trying to teach.

## Generated Artifacts

Generated coverage output and test results should not be tracked.
Use `.gitignore` for coverage and `TestResults` output, and prefer CI checks that catch stale path or naming drift after renames.

## Legacy Review Rule

When reviewing code in `Legacy`, ask two questions:

1. Does it still provide historical or teaching value?
2. Is it clearly archival rather than part of the active learning path?

If the answer to both is no, it should probably be deleted.
