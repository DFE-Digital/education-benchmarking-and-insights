# Pipelines Module

## Module Purpose

Automated CI/CD pipelines managed via Azure DevOps to provide a robust, repeatable lifecycle for all project components (Web, Platform, Data Pipeline, Core Infrastructure, Documentation, and Performance Tests). It enforces code quality, orchestrates secure multi-environment deployments via Terraform, executes advanced quality gates (E2E, A11y, DAST), and handles safe infrastructure teardowns.

## Tech Stack

- **Orchestration:** Azure DevOps multi-stage YAML pipelines
- **Infrastructure as Code:** Terraform (v1.9.8)
- **Languages/Runtimes:** .NET (C#), Node.js (NPM/TypeScript), Bash, PowerShell
- **Quality & Testing:** Markdownlint, ESLint, Dotnet Format, Playwright/Cypress (E2E & A11y), API Tests
- **Security:** Checkov (IaC Static Analysis), ZAProxy (Dynamic Application Security Testing)
- **Cloud:** Azure (Resource Groups, Storage Accounts, Key Vault, App Services)

## Core Logic & Data Flow

1. **Trigger:** Pipelines trigger via PR validation or merges to the `main` branch.
2. **Build Stage:**
   - Lints code (.NET, TS) and formats validation.
   - Note: Markdownlint is part of the central `pr-compliance-checks` workflow and reports its status back to the PR checklist.
   - Runs unit tests and Terraform static analysis (Checkov).
   - Packages build artifacts and publishes them to the pipeline workspace.
3. **Deployment Stages (CI/CD):**
   - **Development:** Automatic deployment on merge to `main`.
   - **Automated Test:** Automatic deployment followed by rigorous, parallel quality gates (E2E, API, A11y, Security scans).
   - **Test:** Final automated gate before manual release workflows.
4. **Release Orchestration:** `production/release.yaml` coordinates specific artifact versions into `pre-production` and `production` via targeted parameters.

## Key Definitions

- **`vars.yaml`**: The central source of truth for pipeline versioning and conditional logic (e.g., `ShouldDeploy`).
- **`common/`**: Contains reusable YAML templates (`run-terraform.yaml`, `fmt-validate-terraform.yaml`) to enforce DRY principles across the architecture.
- **`ShouldDeploy`**: A critical condition variable ensuring infrastructure and application deployments *only* occur from the `main` branch, not during PR validation.
- **`deployment.yaml` & `destroy.yaml`**: Standardized templates defining the lifecycle mapping for environment-specific creation and teardown.
- **`release.yaml`**: The high-level orchestrator that downloads specific tagged versions of artifacts from component pipeline runs for coordinated production releases.

## Integration Points

- **Source Modules**: Consumes artifacts and IaC from `web/`, `platform/`, `data-pipeline/`, `core-infrastructure/`, and `support-analytics/`.
- **Azure DevOps**: Relies heavily on Variable Groups (e.g., `dsi pre-prod`, `automated test app settings`) for environment-specific secrets and configuration injection.
- **Azure Cloud**: Provisions and configures Azure resources via Service Connections referencing specific subscription IDs.

## Development Standards

### Architecture & Engineering Guidelines

- **Template-First (DRY):** Never repeat pipeline logic. Abstract shared steps into `pipelines/common`.
- **Surgical Triggers:** Utilize specific `paths` filters in `trigger` and `pr` configurations so pipelines only execute when relevant component code changes.
- **Shift-Left Security & Testing:** Static analysis (Checkov), linting, and unit tests must be the earliest jobs in the Build stage to fail fast.
- **Idempotency:** All Terraform steps and pipeline scripts must be completely safe to re-run against an existing environment without causing failures or unintended drift.

### Anti-Patterns

- **Hardcoded Secrets:** Never embed secrets, tenant IDs, or specific subscription IDs directly in YAML; always use Variable Groups and Service Connections.
- **Environment Drift:** Bypassing pipelines to make manual changes in the Azure Portal is strictly prohibited.
- **Over-deployment:** Executing apply/deploy steps during a Pull Request lifecycle. PRs should only validate and plan.
- **Monolithic Configuration:** Combining disparate domains (e.g., Web and Data Pipeline) into a single pipeline run. Keep pipelines bounded by architectural components.
