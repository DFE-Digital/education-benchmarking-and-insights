# AI-Assisted Engineering

This repository supports AI-assisted engineering by incorporating guidelines and toolset configurations tailored for AI development agents (like Gemini CLI). This approach standardizes common engineering tasks and enforces project-specific mandates effectively.

## Gemini CLI Workspaces and `.gemini` Folders

The codebase is configured to be an intelligent workspace for Gemini CLI.
Local, user-specific, or session state for the agent is managed in `.gemini` folders. These folders have been added to the project's root `.gitignore` to prevent leaking temporary AI context, user preferences, or session logs into the repository.

You can save project-specific AI memories that will be respected by Gemini CLI locally without affecting the shared repository.

## Gemini Skills

This repository includes custom **Gemini CLI Skills** to automate and standardize common engineering tasks. These skills are located in the top-level `skills/` directory.

Skills act as expert procedural guides for the AI agent, dictating exactly how it should perform complex or domain-specific tasks in the context of this monorepo.

### Platform API Test Creator Skill

The `platform-api-test-creator` skill orchestrates the creation and update of functional API tests, ensuring 100% validation coverage and strict JSON assertions.

#### Installation

For new developers or after a fresh clone, install the skill into your local workspace:

```bash
gemini skills install skills/platform-api-test-creator.skill --scope workspace
```

After installation, you must reload your interactive Gemini session to enable the skill:

```bash
/skills reload
```

#### Usage

Trigger the skill by asking Gemini CLI to work on API tests or coverage. For example:

- "Add functional tests for the Search feature in the School API"
- "Update API test coverage for the LocalAuthority module"
- "Improve functional tests for [Feature Name]"

The agent will automatically follow the project-specific workflow (Research -> Realignment -> JSON Assertion -> Run & Capture).

## Context Control (GEMINI.md)

This project makes extensive use of `GEMINI.md` files located throughout the repository. These files are standard operating procedures (SOPs) and context references specifically for AI agents.

When interacting with the repository using Gemini CLI, the agent will automatically read the relevant `GEMINI.md` files for the module you are working in. This ensures the AI adheres to the specific architectural rules, tech stack constraints, and formatting guidelines of the local module.
