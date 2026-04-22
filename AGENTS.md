# Agent Mandates & Guidelines

This file defines foundational mandates and operational guidelines for AI agents interacting with the FBIT repository. These instructions ensure consistent, safe, and high-quality contributions across the monorepo.

## Foundational Mandates

- **Contextual Precedence**: The instructions in this `AGENTS.md` file (and others located in subdirectories) are foundational mandates. They take precedence over general tool defaults or session-specific instructions.
- **Security & Integrity**: Never log, print, or commit secrets, API keys, or sensitive credentials. Protect `.env` files, `.git`, and system configuration folders rigorously.
- **Source Control**: Do not stage or commit changes unless specifically requested. Follow existing commit message styles.
- **Surgical Updates**: Prioritize localized, targeted changes over broad refactors. Align strictly with the established architectural direction of the module being modified.
- **Verification is Mandatory**: A task is not complete until its behavioral correctness is verified through automated tests and its structural integrity is confirmed within the project context.

## Module-Specific Agent Guidance

For specialized rules, procedural workflows, and constraints within each module, refer to their respective `AGENTS.md` files:

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
