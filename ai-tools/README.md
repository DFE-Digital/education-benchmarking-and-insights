# Gemini AI Tools

This directory contains specialized commands, instructions, and templates for AI agents to assist with common tasks in the FBIT codebase.

## Directory Structure

- `commands/`: Lightweight `.toml` command definitions.
- `instructions/`: Procedural guides and shared rules.
- `templates/`: Standardized output formats (e.g., test plans).
- `scripts/`: Installation scripts to copy tools to your local `.gemini/` folder.

## Available Tools

### Platform API Metadata Enrichment (`/api-metadata-enrich`)

Systematically enhances Platform APIs with XML documentation, descriptive OpenAPI attributes, and explicit FluentValidation error messages. Use this tool to prepare an API for high-quality test plan generation or to improve Swagger documentation without altering core business logic.

### Platform API Testing Tools

Separated into two distinct commands for better planning, review, and execution of functional API tests:

1. **Platform API Test Planner (`/api-test-plan`)**: Researches, evaluates, and plans functional tests for Platform API features. Outputs a formal test plan into the `documentation/quality-assurance/api-test-plans/` directory using a standardized template.
2. **Platform API Test Implementer (`/api-test-implement`)**: Executes the test plan. It creates or updates Gherkin feature files, step bindings, and JSON data files using strict assertions and a fail-first approach, subsequently logging the changes back to the test plan.

### Git Tools

#### Staged Commit Message Generator (`/staged-commit`)

Proposes a concise, one-line commit message based on your currently staged changes. It follows the [Conventional Commits](https://www.conventionalcommits.org) standard and looks at the last 3 commits in your history to ensure consistency with the project's style.

## Setup & Installation

For new developers or after a fresh clone, run the installation script from the root of the repository. This will copy the necessary commands, instructions, and templates into your local `.gemini/` folder.

**Windows (PowerShell):**

```powershell
.\ai-tools\scripts\install-tools.ps1
```

**macOS/Linux (Bash):**

```bash
./ai-tools/scripts/install-tools.sh
```

After installation, the commands are immediately available in your interactive Gemini session.

## Usage

The tools follow a consistent pattern: `/[command] [API Name] [Feature Name]`.

1. **Enrich API Metadata:**
   Prepare the API for testing by ensuring it has descriptive metadata.
   - `/api-metadata-enrich School Search`
   - `/api-metadata-enrich Trust Insight`

2. **Plan API Tests:**
   Research and generate a formal test plan.
   - `/api-test-plan School Search`
   - `/api-test-plan LocalAuthority Finances`

3. **Implement Tests:**
   Once the test plan is reviewed, execute it to generate the code.
   - `/api-test-implement School Search`
   - `/api-test-implement LocalAuthority Finances`

4. **Generate Commit Message:**
   Propose a message for your currently staged changes.
   - `/staged-commit`

## How it Works

The commands in `commands/` are designed to be copied to your local `.gemini/commands` folder. They are configured to read their procedural logic from the `.gemini/instructions/` folder. This decoupling allows us to maintain complex logic in standard Markdown files while keeping the command definitions simple and easy to version control.
