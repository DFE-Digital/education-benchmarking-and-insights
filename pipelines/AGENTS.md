# Pipelines Module: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `pipelines` module.

## Development Standards

- **Template-First (DRY)**: Never repeat pipeline logic. Abstract shared steps into `pipelines/common`.
- **Surgical Triggers**: Utilize specific `paths` filters in `trigger` and `pr` configurations so pipelines only execute when relevant component code changes.
- **Shift-Left Security & Testing**: Static analysis (Checkov), linting, and unit tests must be the earliest jobs in the Build stage to fail fast.
- **Idempotency**: All Terraform steps and pipeline scripts must be completely safe to re-run against an existing environment without causing failures or unintended drift.

## Anti-Patterns

- **Hardcoded Secrets**: Never embed secrets, tenant IDs, or specific subscription IDs directly in YAML; always use Variable Groups and Service Connections.
- **Environment Drift**: Bypassing pipelines to make manual changes in the Azure Portal is strictly prohibited.
- **Over-deployment**: Executing apply/deploy steps during a Pull Request lifecycle. PRs should only validate and plan.
- **Monolithic Configuration**: Combining disparate domains (e.g., Web and Data Pipeline) into a single pipeline run. Keep pipelines bounded by architectural components.
