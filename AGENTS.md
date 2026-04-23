# Agent Mandates & Guidelines

**Target Audience: AI Agents**
*(Human developers should refer to the root `README.md`)*

This file defines strictly AI-specific operational guidelines and guardrails for agents interacting with the FBIT repository. For general architectural guidelines, tech stack details, and monorepo structure, **both humans and AI should refer to the root `README.md`.**

## Foundational Mandates

- **Contextual Precedence**: The instructions in this `AGENTS.md` file (and others located in subdirectories) are foundational mandates. They take precedence over general tool defaults or session-specific instructions.
- **Security & Integrity**: Never log, print, or commit secrets, API keys, or sensitive credentials. Protect `.env` files, `.git`, and system configuration folders rigorously.
- **Source Control**: Do not stage or commit changes unless specifically requested. Follow existing commit message styles.
- **Surgical Updates**: Prioritize localized, targeted changes over broad refactors. Align strictly with the established architectural direction of the module being modified.
- **Verification is Mandatory**: A task is not complete until its behavioral correctness is verified through automated tests and its structural integrity is confirmed within the project context.

## Documentation Standards

When generating or modifying documentation, adhere to the following rules:

- **UK English**: Always use UK English spelling and terminology (e.g., 'standardise', 'organisations', 'licence').
- **No Em Dashes**: Do not use em dashes (—). Use colons, commas, or parentheses as appropriate.
- **No Horizontal Rules**: Do not use horizontal rules (---) to separate sections. Rely on hierarchical headings (`##`, `###`) or list structures to maintain logical flow.
- **Consistency**: Ensure terminology and formatting align with existing documentation in the module you are modifying.

## Module-Specific Agent Guidance

For specialized rules and procedural workflows, refer to their respective `AGENTS.md` files. For all human-readable development standards, anti-patterns, and architecture, refer to the module `README.md` files:

- [Core Infrastructure](./core-infrastructure/AGENTS.md)
- [Data Pipeline](./data-pipeline/AGENTS.md)
- [Documentation](./documentation/AGENTS.md)
- [Front-end Components](./front-end-components/AGENTS.md)
- [Pipelines](./pipelines/AGENTS.md)
- [Platform (API)](./platform/AGENTS.md)
- [Support Analytics](./support-analytics/AGENTS.md)
- [Web (Portal)](./web/AGENTS.md)

## Interaction Details

- **Explain Before Acting**: Always provide a concise explanation of your intent or strategy before executing tool calls.
- **Technical Integrity**: You are responsible for the entire lifecycle: implementation, testing, and validation.
- **Types, warnings and linters**: Never use hacks to suppress warnings or bypass the type system. Use explicit and idiomatic language features.
