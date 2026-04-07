# Module Context: Documentation

## Module Purpose

The `documentation` module implements a **Docs-as-Code Generation Pipeline**.
It serves as the central source of truth for all technical, business, and
operational information, providing a structured system to manage and generate
multi-format documentation (PDF, DOCX, JIRA) from a single Markdown-based
repository.

## Tech Stack

- **Content:** Markdown (enhanced with Mermaid.js for diagrams).
- **Processing:** Pandoc (universal document converter) and Mermaid CLI
  (`mmdc`) for diagram rendering.
- **Automation:** GNU Make (`Makefile`) and Bash scripts for build
  orchestration.
- **Styling:** LaTeX (for PDF templates) and Microsoft Word (for DOCX reference
  templates).
- **Quality:** Markdownlint-cli2 for consistent formatting and linting.
- **Utilities:** Python for generating Markdown tables from data schemas.
- **Environment Quirks:** Mermaid CLI (`mmdc`) relies on Puppeteer. On Ubuntu
  24.04+ (or restricted CI environments), AppArmor breaks the Chromium sandbox.
  Always pass `puppeteer-config.json` to disable the sandbox during automated
  builds.

## Core Logic & Data Flow

Documentation is authored as individual Markdown files, logically grouped and
numbered (e.g., `1_Introduction.md`) to maintain a deterministic sequence. The
build process aggregates these files, renders diagrams, and transforms them
into final artifacts.

## Key Definitions

- **`build-docs.sh <SOURCE_DIR> <PROJECT_NAME> <TARGET_DIR>`**: The core bash
  orchestrator. It resolves inputs, triggers Mermaid CLI for diagram
  generation, and passes the aggregate to Pandoc.
- **`DictMarkdown`**: A Python utility class (in `tools/input_schemas.py`) used
  to dynamically generate standardized Markdown tables from Python dictionary
  schemas.
- **Pandoc Templates (`tools/templates/`)**: The `.latex` and `.docx` files
  that define the absolute visual styling (headers, footers, typography) for
  the compiled artifacts.
- **`architecture/`, `developers/`, `quality-assurance/`**: The primary source
  directories containing categorized Markdown files and domain-specific
  knowledge.

## Integration Points

- **Cross-Project Documentation**: Provides the definitive technical and
  business context for all other modules (`Core`, `Platform`, `Front-End`,
  etc.).
- **CI/CD Pipelines**: The `Makefile` targets (e.g., `make architecture-pdf`)
  act as the primary interface for automated pipelines (like GitHub Actions) to
  generate and publish release artifacts.
- **External Tooling**: Depends on `pandoc` and `@mermaid-js/mermaid-cli` being
  available in the environment.

## Development Standards

- **Pipeline Agnostic**: The `Makefile` is the universal entry point. Never
  bypass the `Makefile` to call build scripts directly unless debugging a
  specific Pandoc/Mermaid failure.
- **Numbered Sequencing**: Markdown files must use a numeric prefix (e.g.,
  `00_Title.md`, `01_Intro.md`) to ensure correct ordering when combined into
  single documents.
- **Diagrams-as-Code**: Always use Mermaid.js for technical diagrams to ensure
  they are versionable and easy to maintain.
- **Strict Linting**: All documentation must pass `markdownlint-cli2` checks as
  defined in the root `.markdownlint-cli2.jsonc` file.
- **Template-Driven Styling**: Never use inline HTML or custom formatting for
  style; rely on the Pandoc templates.

## Anti-Patterns

- **Manual Artifact Editing**: Never edit PDF or DOCX files directly; all
  changes must be made to the Markdown source and recompiled.
- **Hardcoded Absolute Paths**: Scripts in `tools/` must use relative paths or
  environment-defined variables (`SOURCE_DIR`, `TARGET_DIR`) to ensure
  portability.
- **Binary Image Bloat**: Avoid storing large binary files in `images/`
  folders; prefer vector graphics, generated diagrams, or text representations.
- **Breaking Sequences**: Do not interrupt the numerical sequence of files in
  main documentation directories, as it breaks the unified document
  compilation.
