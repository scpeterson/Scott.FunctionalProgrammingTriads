# Versioned Docs with Mike

Use `mike` to publish versioned MkDocs documentation to `gh-pages`.

## Which Serve Command to Use

Preferred workflow in this repository:

1. Use `mkdocs serve` for normal authoring speed while editing a single docs version.
2. Use `mike serve` before publishing to validate versioned behavior (`latest`, numbered versions, and version selector links).
3. Use `mike deploy` to publish.

In short: `mkdocs serve` for writing, `mike serve` for versioning validation.

## Prerequisites

- Python 3.10+
- `requirements-docs.txt` installed
- Git remote configured for this repository

## First-Time Setup

```bash
mike deploy --push --update-aliases 0.1 latest
mike set-default --push latest
```

## Publish a New Version

```bash
mike deploy --push --update-aliases 0.2 latest
```

## Local Preview of Versioned Site

```bash
mike serve
```

## Local Preview for Day-to-Day Authoring

```bash
mkdocs serve
```

## Notes

- Keep version labels stable (`0.1`, `0.2`, `1.0`, etc.).
- Use `latest` alias for the most current docs set.
