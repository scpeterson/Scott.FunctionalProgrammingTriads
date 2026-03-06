# PDF Export

The project includes an optional workflow to build a PDF-oriented docs artifact.

## Manual Local Build (HTML)

```bash
mkdocs build --strict
```

## GitHub Workflow PDF Artifact

Use the `Docs PDF` workflow (manual dispatch) to generate and upload a docs artifact suitable for PDF conversion workflows.

Workflow file:

- `.github/workflows/docs-pdf.yml`

## Notes

- The default docs workflow remains HTML-first.
- PDF generation is intentionally isolated from the main CI gate.
