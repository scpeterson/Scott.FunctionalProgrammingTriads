# Versioned Docs with Mike

Use `mike` to publish versioned MkDocs documentation to `gh-pages`.

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

## Notes

- Keep version labels stable (`0.1`, `0.2`, `1.0`, etc.).
- Use `latest` alias for the most current docs set.
