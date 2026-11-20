---
title: How-to guides
layout: sub-navigation
sectionKey: How-to guides
includeInBreadcrumbs: true
eleventyNavigation:
  key: How-to guides
---

How-to guides are **task-oriented** recipes that guide an experienced practitioner through the steps required to solve a specific, real-world problem across the Financial Benchmarking and Insights Tool (FBIT).

## [Data & Pipeline](/how-to/data-and-pipeline/)

Orchestrate annual financial data releases, run the Python ingestion pipeline, manage Databricks, and run database migrations:

* **[CFR File Generation](/how-to/data-and-pipeline/cfr-file-generation/):** Data extraction, SQL preparation, and dimension table generation for maintained school returns.
* **[Data Release Guide](/how-to/data-and-pipeline/data-release-guide/):** End-to-end procedures for executing the four annual financial data releases (AAR, BFR, CFR, S251).
* **[Database Migrations](/how-to/data-and-pipeline/database-migrations/):** Author idempotent SQL migration scripts with DbUp and execute schema migrations safely.
* **[Databricks Connectivity](/how-to/data-and-pipeline/databricks-connectivity/):** Connect local tooling and pipeline workers to Azure Databricks workspaces.
* **[Databricks Data Additions](/how-to/data-and-pipeline/databricks-data-additions/):** Define and register new tables, views, and schemas in Databricks.
* **[Databricks Development](/how-to/data-and-pipeline/databricks-development/):** Local notebook workflows, cluster execution, and integration with the data pipeline.

## [Design](/how-to/design/)

Prototype design changes for the service:

* **[Add a design concept to the prototype kit](/how-to/design/add-a-design-concept/):** Run the FBIT prototype kit locally, add a design concept to a work item, and share it with the team.

## [Development & Workflow](/how-to/development-and-workflow/)

Configure your local engineering environment, adhere to repository code standards, and use automated quality pipelines:

* **[Azure DevOps Workflow](/how-to/development-and-workflow/azure-devops-workflow/):** Manage delivery boards, branch policies, work item linking, and release traceability.
* **[Build Configuration](/how-to/development-and-workflow/build-configuration/):** Manage MSBuild shared properties, Central Package Management, and .NET target frameworks.
* **[Code Management](/how-to/development-and-workflow/code-management/):** Trunk-based branching strategies, commit conventions, and pull request review checklists.
* **[Dependabot Alerts](/how-to/development-and-workflow/dependabot/):** Triage, review, test, and merge automated dependency security updates.
* **[Linting and Formatting](/how-to/development-and-workflow/linting-and-formatting/):** Run and configure Markdown, C# (.NET format / CSharpier), and Python linters across the repository.
* **[Local Environment with Docker](/how-to/development-and-workflow/local-docker/):** Spin up local dependencies (SQL Server, Azurite emulator, Redis) using Docker Compose.
* **[Pre-commit Hooks](/how-to/development-and-workflow/pre-commit-hooks/):** Install and configure git pre-commit hooks to automate formatting, linting, and secret checks.

## [Infrastructure & Operations](/how-to/infrastructure-and-operations/)

Respond to incidents, monitor telemetry, maintain operational runbooks, and protect secrets:

* **[Incident Management](/how-to/infrastructure-and-operations/incident-management/):** Triage incident severity levels, coordinate stakeholder communications, and track resolution targets.
* **[Monitoring and Alerting](/how-to/infrastructure-and-operations/monitoring-alerting/):** Monitor application health, alert thresholds, and Log Analytics queries in Application Insights.
* **[Monthly Reporting](/how-to/infrastructure-and-operations/monthly-reporting/):** Produce monthly school engagement and service usage reports with Power Query and Azure SQL.
* **[Release Communications](/how-to/infrastructure-and-operations/release-communications/):** Draft and send planned release announcements and feature notes to stakeholders.
* **[Root Cause Analysis](/how-to/infrastructure-and-operations/root-cause-analysis/):** Post-incident review methodology and standardised root-cause analysis report template.
* **[Runbooks](/how-to/infrastructure-and-operations/runbooks/):** Operational runbooks covering backup/restore, disaster recovery, deployments, and service shuttering.
* **[Secret Management](/how-to/infrastructure-and-operations/secret-management/):** Protect sensitive credentials, encrypt local settings, and configure .NET User Secrets.

## [Platform & Web](/how-to/platform-and-web/)

Build web user interfaces, publish server-rendered visualisations, manage dynamic content, and maintain platform APIs:

* **[API Management](/how-to/platform-and-web/api-management/):** Guidelines for RESTful design, authentication keys, rate limiting, and lifecycle management across APIs.
* **[Chart Development Workflow](/how-to/platform-and-web/chart-development-workflow/):** Multi-disciplinary workflow between designers, engineers, and analysts for developing server-rendered SVG charts.
* **[Content Management](/how-to/platform-and-web/content-management/):** Update dynamic database-driven banners, advisory messages, and financial return year parameters.
* **[Feature Flags](/how-to/platform-and-web/feature-flags/):** Define, consume, and test runtime feature flags via Azure App Configuration.
* **[Web Assets](/how-to/platform-and-web/web-assets/):** Compile SASS styles, bundle TypeScript/JavaScript, and manage static front-end assets.

## [Quality & Assurance](/how-to/quality-and-assurance/)

Plan, coordinate, and execute testing activities across development sprints and production data releases:

* **[Create Test Plan in DevOps](/how-to/quality-and-assurance/create-test-plan-for-release-in-devops/):** Step-by-step instructions for structuring and executing release test suites in Azure DevOps.
* **[Data Release Test Plan](/how-to/quality-and-assurance/data-release-test-plan/):** Reusable QA test plan template and validation gates for annual data ingestion drops.
* **[QA Test Plan](/how-to/quality-and-assurance/test-plan/):** Practical delivery framework for testing activities, roles, and regression sign-offs.
