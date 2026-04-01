# Project: Education Benchmarking and Insights (FBIT)

## Project Overview

The Financial Benchmarking and Insights Tool (FBIT) is a comprehensive platform
for managing and analyzing UK school financial data. It enables schools,
trusts, and local authorities to compare resource usage, interrogate financial
performance, and support strategic planning to improve pupil outcomes.

## Monorepo Structure

This repository is organized as a monorepo with the following primary modules:

- **`web/`**: ASP.NET Core MVC portal. The primary entry point for users,
  utilizing Vue 3 for interactive client-side features.
- **`platform/`**: .NET Azure Functions REST APIs. Provides the backend data
  access layer for benchmarking and establishment insights.
- **`front-end-components/`**: React/TypeScript visualization library. Provides
  the charting and interactive tools used by the `web` module.
- **`data-pipeline/`**: Python/Pandas processing engine. Transforms raw DfE
  datasets into structured benchmarking and RAG (Red-Amber-Green) ratings.
- **`core-infrastructure/`**: Terraform IaC and .NET DbUp migrations. Manages
  Azure cloud resources and SQL database schemas.
- **`pipelines/`**: YAML-based CI/CD pipeline definitions for Azure DevOps.
- **`support-analytics/`**: Log Analytics queries and KQL for operational
  monitoring.
- **`documentation/`**: Central repository for architecture, design, and
  operational guides.

## Core Architecture & Data Flow

1. **Ingestion**: Raw CSV/Excel data from DfE sources is uploaded to Azure Blob
   Storage.
2. **Processing**: The **`data-pipeline`** (Python) cleans, joins, and
   calculates benchmarking metrics, persisting results to Azure SQL.
3. **Serving**: The **`platform`** (Azure Functions) exposes this data via
   RESTful APIs, using Dapper for high-performance SQL access.
4. **Presentation**: The **`web`** (ASP.NET MVC) portal consumes these APIs and
   renders interactive dashboards using **`front-end-components`** (React).

## Global Engineering Standards

- **GOV.UK Design System**: All user-facing UI must strictly adhere to GDS
  styles and accessible patterns.
- **Accessibility**: All features must meet **WCAG 2.2 AA** standards.
  Automated a11y testing is mandatory.
- **Surgical Updates**: Changes should be localized to the specific module
  being modified. Avoid cross-cutting refactors without clear justification.
- **Testing**: Every PR must include relevant tests (Unit, Integration, or
  E2E). A change is not complete until it is verified.
- **Type Safety**: Use C# Nullable Reference Types and strict TypeScript (no
  `any`).
- **Infrastructure as Code**: No manual resource creation in Azure.
  Infrastructure is managed via Terraform and is distributed across modules.
  Shared/foundational resources (VNet, SQL Server, Key Vault) are defined in
  `core-infrastructure/terraform`, while component-specific resources (App
  Services, Functions, Container Apps) are defined within their respective
  module's `terraform/` directory.

## Module-Specific Guidance

For detailed technical documentation, standards, and anti-patterns within each
module, refer to their respective `GEMINI.md` files:

- [Core Infrastructure](./core-infrastructure/GEMINI.md)
- [Data Pipeline](./data-pipeline/GEMINI.md)
- [Documentation](./documentation/GEMINI.md)
- [Front-end Components](./front-end-components/GEMINI.md)
- [Pipelines](./pipelines/GEMINI.md)
- [Platform (API)](./platform/GEMINI.md)
  - [Platform Chart Rendering API](./platform/src/apis/Platform.Api.ChartRendering/GEMINI.md)
- [Support Analytics](./support-analytics/GEMINI.md)
- [Web (Portal)](./web/GEMINI.md)

## Key Workflows

- **Local Development**: Refer to module-specific `README.md` files for setup
  instructions.
- **Database Changes**: Always use the DbUp migration project in
  `core-infrastructure/src/db`.
- **UI Changes**: When updating React components, ensure you run the build and
  copy scripts to sync assets with the `web` project.
