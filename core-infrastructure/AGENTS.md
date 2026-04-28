# Core Infrastructure: Agent Mandates

This file defines AI-specific operational guidelines for agents working within the `core-infrastructure` module.
You **MUST** read the `README.md` in this directory before proposing changes or writing code for all human-readable architecture, tech stack, development standards, and anti-patterns.

## AI Operational Guidelines

- **IaC Strictness**: Do not suggest or attempt manual Azure Portal changes; all infrastructure MUST be defined in Terraform.
- **Sequential Migrations**: When asked to modify database schemas, you must create a new, sequentially numbered SQL file in `Scripts/` rather than modifying existing applied scripts.
