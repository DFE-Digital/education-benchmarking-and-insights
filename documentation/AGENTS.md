# Documentation: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `documentation` module.

## Development Standards

- **Pipeline Agnostic**: The `Makefile` is the universal entry point. Never bypass the `Makefile` to call build scripts directly unless debugging a specific Pandoc/Mermaid failure.
- **Numbered Sequencing**: Markdown files must use a numeric prefix (e.g., `00_Title.md`, `01_Intro.md`) to ensure correct ordering when combined into single documents.
- **Diagrams-as-Code**: Always use Mermaid.js for technical diagrams to ensure they are versionable and easy to maintain.
- **Strict Linting**: All documentation must pass `markdownlint-cli2` checks as defined in the root `.markdownlint-cli2.jsonc` file.
- **Template-Driven Styling**: Never use inline HTML or custom formatting for style; rely on the Pandoc templates.

## Anti-Patterns

- **Manual Artifact Editing**: Never edit PDF or DOCX files directly; all changes must be made to the Markdown source and recompiled.
- **Hardcoded Absolute Paths**: Scripts in `tools/` must use relative paths or environment-defined variables (`SOURCE_DIR`, `TARGET_DIR`) to ensure portability.
- **Binary Image Bloat**: Avoid storing large binary files in `images/` folders; prefer vector graphics, generated diagrams, or text representations.
- **Breaking Sequences**: Do not interrupt the numerical sequence of files in main documentation directories, as it breaks the unified document compilation.
