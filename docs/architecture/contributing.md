# Architecture Docs Contribution Guide

This guide defines how to update architecture documentation and ADRs in this repository.

## When to Add an ADR

Add a new ADR when a change:

- introduces a new cross-cutting pattern
- replaces an existing architectural approach
- changes quality/compliance policy (coverage gates, CI quality checks)
- impacts how demos are structured or discovered

## ADR Checklist

1. Create `docs/architecture/adr/NNNN-title.md` with the required template sections.
2. Add a short, explicit `References` section with concrete file paths.
3. Update `docs/architecture/adr/.nav.yml`.
4. Update `docs/index.md` if this is a major decision.
5. Run `mkdocs build --strict`.

## Writing Style

- Prefer concrete statements over theory.
- Capture tradeoffs and consequences, not just benefits.
- Keep each ADR focused on a single decision.
- Use absolute code paths only in references when possible.

## PR Review Expectations

- Decision rationale is explicit.
- Alternatives are documented and plausible.
- Consequences include at least one downside.
- References resolve to real files.
