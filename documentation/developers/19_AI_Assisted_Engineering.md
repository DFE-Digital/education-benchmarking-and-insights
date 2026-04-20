# AI-Assisted Engineering

This repository supports AI-assisted engineering by incorporating guidelines and toolset configurations tailored for AI development agents (like Gemini CLI). This approach standardizes common engineering tasks and enforces project-specific mandates effectively.

## Gemini CLI Workspaces and `.gemini` Folders

The codebase is configured to be an intelligent workspace for Gemini CLI.
Local, user-specific, or session state for the agent is managed in `.gemini` folders. These folders have been added to the project's root `.gitignore` to prevent leaking temporary AI context, user preferences, or session logs into the repository.

You can save project-specific AI memories that will be respected by Gemini CLI locally without affecting the shared repository.

## Gemini Skills

This repository includes custom **Gemini CLI Skills** to automate and standardize common engineering tasks. These skills are located in the top-level `skills/` directory.

Skills act as expert procedural guides for the AI agent, dictating exactly how it should perform complex or domain-specific tasks in the context of this monorepo.

### Platform API Testing Skills

We have separated the API test creation process into two distinct skills for better planning, review, and execution of functional API tests.

#### 1. Platform API Test Planner (`platform-api-test-planner`)

This skill is responsible for researching, evaluating, and planning functional tests for Platform API features. It outputs a formal test plan into the `documentation/quality-assurance/api-test-plans/` directory.

#### 2. Platform API Test Implementer (`platform-api-test-implementer`)

This skill executes the test plan. It creates or updates Gherkin feature files, step bindings, and JSON data files using strict assertions and a fail-first approach, subsequently logging the changes back to the test plan.

#### Installation

For new developers or after a fresh clone, install the skills into your local workspace:

```bash
gemini skills install skills/platform-api-test-planner.skill --scope workspace
gemini skills install skills/platform-api-test-implementer.skill --scope workspace
```

After installation, you must reload your interactive Gemini session to enable the skills:

```bash
/skills reload
```

#### Usage

First, ask Gemini CLI to research and create a test plan:

- "Plan API tests for the Search feature in the School API"
- "Create a test plan for the LocalAuthority module API endpoints"

Once the test plan is generated and reviewed, trigger the implementer:

- "Implement the test plan for the Search feature"
- "Execute the test plan we just created for the LocalAuthority module"

## Context Control (GEMINI.md)

This project makes extensive use of `GEMINI.md` files located throughout the repository. These files are standard operating procedures (SOPs) and context references specifically for AI agents.

When interacting with the repository using Gemini CLI, the agent will automatically read the relevant `GEMINI.md` files for the module you are working in. This ensures the AI adheres to the specific architectural rules, tech stack constraints, and formatting guidelines of the local module.
