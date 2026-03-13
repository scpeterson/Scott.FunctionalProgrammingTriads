# Legacy Folder

This folder contains archival code that is intentionally kept outside the active demo catalog.

## Purpose

Use `Legacy` for code that is still useful as historical reference, but no longer matches the current triad teaching structure closely enough to keep in the main demo flow.

## What Belongs Here

- superseded examples that still have teaching value
- small exploratory samples worth retaining for comparison
- archival code that explains how the repository evolved

## What Does Not Belong Here

- active demos that should be runnable from the CLI
- shared runtime infrastructure used by active demos
- dead code that has no remaining teaching or archival value

## Expectations

- Files in this folder may be non-runnable or lightly maintained.
- New active work should prefer the triad structure under `Scott.FunctionalProgrammingTriads.Core/Demos`.
- If a legacy file is superseded by a newer demo, the newer demo or ADR should be the primary learning path.

## Current Contents

- `Demos/SchrodingerDemo.cs`: retained as an older nondeterminism-oriented sample.
- `MathExamples.cs`: retained as a small standalone historical example.
