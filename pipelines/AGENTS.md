# Pipelines Module: Agent Mandates

This file defines AI-specific operational guidelines for agents working within the `pipelines` module.
For all human-readable architecture, tech stack, development standards, and anti-patterns, refer to the `README.md` in this directory.

## AI Operational Guidelines

- **Template Reusability**: When creating new pipelines, default to using shared templates in `pipelines/common` rather than duplicating step logic.
- **No Hardcoded Secrets**: Ensure no secrets, tenant IDs, or subscription IDs are embedded directly in YAML.
