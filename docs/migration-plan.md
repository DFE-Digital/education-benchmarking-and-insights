# Documentation Migration Plan (Diátaxis Framework)

This document outlines the detailed plan to migrate our existing documentation in the root `documentation/` folder to the new, structured `docs/content/` directory which is published via Eleventy (11ty).

The migration adopts the **Diátaxis framework**, which categorises documentation based on user needs: Tutorials (learning-oriented), How-to Guides (task-oriented), Reference (information-oriented), and Explanation (understanding-oriented).

## Target Folder Structure Overview

```text
docs/content/
├── tutorials/
│   └── developers/
├── how-to/
│   ├── developers/
│   ├── data/
│   ├── qa/
│   └── operational/
├── reference/
│   ├── architecture/
│   ├── api/
│   ├── data/
│   ├── features/
│   ├── design/
│   ├── developers/
│   ├── qa/
│   └── operational/
└── explanation/
    ├── architecture/
    ├── data/
    ├── design/
    └── developers/
```

## Step-by-Step Migration Process

Future migration efforts should execute the following steps for each target file or folder:

1. **Move and Clean File Names**
   - Copy the files from `documentation/` to their designated destination in `docs/content/`.
   - Remove numeric sequence prefixes (e.g., `01_Introduction.md` becomes `introduction.md`) since ordering in the static site is handled by 11ty frontmatter rather than filename prefixing.
   - Standardise to kebab-case (e.g., `Incident_Management.md` becomes `incident-management.md`).

2. **Add Eleventy Frontmatter**
   - Every migrated file must have an 11ty frontmatter block at the top.
   - Minimum configuration for content pages:

     ```yaml
     ---
     title: <Clean Page Title>
     layout: sub-navigation
     sectionKey: <Tutorials | How-to guides | Reference | Explanation>
     includeInBreadcrumbs: true
     eleventyNavigation:
       key: <Unique Navigation Key>
       parent: <Key of parent folder's index.md>
     ---
     ```

3. **Refactor Links and Images**
   - Locate and update all relative links (`[Link text](../architecture/01_Introduction.md)`) to reflect their new relative positions.
   - Copy referenced images from `documentation/<folder>/images/` to `docs/content/assets/images/` and update references to use the correct static path (e.g., `/assets/images/...` or correct relative links).

4. **Verify Render Output**
   - Build the site locally using `npm run start` and verify that the page renders correctly with appropriate breadcrumbs, side navigation, and table of contents.
   - Run linter checks (such as `markdownlint`) to ensure the files comply with styling guidelines.

5. **Deprecate Root Documentation Folder**
   - Once all files are fully migrated and verified, safely remove the root `documentation/` directory.
   - Update the root `Makefile` and any associated CI/CD workflows that reference compiling PDF/DOCX from the old `documentation/` folder.

## File Mapping Guide

The following tables specify exactly where each file from the old `documentation/` folder should be migrated.

### 1. Architecture Document Mapping (`documentation/architecture/`)

| Current File | Target Location | Rationale |
| :--- | :--- | :--- |
| `01_Introduction.md` | `docs/content/explanation/architecture/introduction.md` | Focuses on high-level understanding of the system context. |
| `02_Business-Context.md` | `docs/content/explanation/architecture/business-context.md` | Explains the "why" and strategic reasoning behind the platform. |
| `03_Constraints-and-Principals.md` | `docs/content/explanation/architecture/constraints-and-principles.md` | Explains the constraints and design principles that guide architecture. |
| `04_Non-Functional-Requirements.md` | `docs/content/reference/architecture/non-functional-requirements.md` | Structured details of system limits, requirements, and compliance. |
| `05_Volumetrics-Demand.md` | `docs/content/explanation/architecture/volumetrics-demand.md` | Concept analysis of usage load and demand models. |
| `06_Information-Architecture.md` | `docs/content/explanation/architecture/information-architecture.md` | High-level structural description of how information is stored. |
| `07_Application-Architecture.md` | `docs/content/explanation/architecture/application-architecture.md` | Deep dive explaining components and system interaction. |
| `08_Logical-Architecture.md` | `docs/content/explanation/architecture/logical-architecture.md` | Narrative structure of logical groupings and design. |
| `09_Deployment-Architecture.md` | `docs/content/explanation/architecture/deployment-architecture.md` | Detailed overview of infrastructure deployment strategies. |
| `10_Security-Architecture.md` | `docs/content/reference/architecture/security-architecture.md` | Concrete list of security controls, requirements, and configurations. |
| `11_Decisions.md` | `docs/content/reference/architecture/decisions/index.md` | Listing page for Architectural Decision Records. |
| `decisions/` (Folder) | `docs/content/reference/architecture/decisions/` | ADRs are pure reference material detailing what was decided. |

### 2. Data Document Mapping (`documentation/data/`)

| Current File | Target Location | Rationale |
| :--- | :--- | :--- |
| `01_Domain.md` | `docs/content/explanation/data/domain.md` | Explanation of the domain context and data philosophy. |
| `02_Sources.md` | `docs/content/reference/data/sources.md` | Structured data listing inputs and sources. |
| `03_Models.md` | `docs/content/reference/data/models.md` | Technical descriptions of data schemas and fields. |
| `04_Processing.md` | `docs/content/explanation/data/processing.md` | Explaining how and why data processing flows are designed. |
| `05_Releases.md` | `docs/content/reference/data/releases.md` | History, metadata, and schedule of data releases. |
| `06_Timeline.md` | `docs/content/reference/data/timeline.md` | Timeline schemas and timing rules. |
| `07_Cost-Categories.md` | `docs/content/reference/data/cost-categories.md` | Reference mapping and rules for cost classification. |
| `08_User-Generated-Calculations.md` | `docs/content/explanation/data/user-generated-calculations.md` | Explaining the background logic for calculations. |
| `09_Academy-Apportionments.md` | `docs/content/explanation/data/academy-apportionments.md` | Theoretical and financial context for apportionment rules. |
| `10_Databricks-Development.md` | `docs/content/how-to/data/databricks-development.md` | Concrete guide on how to perform Databricks development. |
| `11_Databricks-Data-Additions.md` | `docs/content/how-to/data/databricks-data-additions.md` | Steps for adding new data sources into Databricks. |
| `12_Databricks-Connectivity-To-s198.md` | `docs/content/how-to/data/databricks-connectivity.md` | Procedural network/connectivity steps. |

### 3. Developer Document Mapping (`documentation/developers/`)

| Current File | Target Location | Rationale |
| :--- | :--- | :--- |
| `01_Getting-Started.md` | `docs/content/tutorials/developers/getting-started.md` | Interactive walkthrough for newcomers to build confidence. |
| `02_Code-Management.md` | `docs/content/how-to/developers/code-management.md` | Instructions on standard workflow tasks. |
| `03_Build-Configuration.md` | `docs/content/how-to/developers/build-configuration.md` | How to configure builds. |
| `04_Pre-commit-Hooks.md` | `docs/content/how-to/developers/pre-commit-hooks.md` | How to set up and manage local hooks. |
| `05_Linting-and-Formatting.md` | `docs/content/how-to/developers/linting-and-formatting.md` | Concrete guide to check style standards locally. |
| `06_Local-Environment-with-Docker.md` | `docs/content/how-to/developers/local-docker.md` | Guide on setting up and starting local Docker stacks. |
| `10_Task_Cancellation.md` | `docs/content/explanation/developers/task-cancellation.md` | Explains the complex "why" behind handling process cancellation. |
| `11_Platform-APIs.md` | `docs/content/reference/api/platform-apis.md` | List of endpoints, requests, and response schemas. |
| `12_Feature-Flags-Management-Guide.md`| `docs/content/how-to/developers/feature-flags.md` | Step-by-step tasks to toggle features. |
| `12_Secret-Management-Guide.md` | `docs/content/how-to/developers/secret-management.md` | Steps for provisioning secrets. |
| `13_Content-Management.md` | `docs/content/how-to/developers/content-management.md` | Steps for updating site content files. |
| `14_Authentication-Authorisation.md` | `docs/content/explanation/developers/auth-authz.md` | Underlying system design for authentication flow. |
| `15_Web-Assets.md` | `docs/content/how-to/developers/web-assets.md` | How to package and bundle CSS/JS assets. |
| `16_Azure-DevOps-Workflow.md` | `docs/content/how-to/developers/azure-devops-workflow.md` | Step-by-step CI/CD usage instructions. |
| `17_Azure-Front-Door-Review.md` | `docs/content/reference/architecture/azure-front-door.md` | Configuration reference for network entry point. |
| `18_Sortable-Html-Tables.md` | `docs/content/how-to/developers/sortable-tables.md` | Implementation guide for interactive elements. |
| `19_AI_Assisted_Engineering.md` | `docs/content/explanation/developers/ai-assisted-engineering.md` | Strategic context and rules for AI usage in development. |
| `20_Database-Migrations-and-Scripts.md`| `docs/content/how-to/developers/database-migrations.md` | Guide on executing and writing migrations. |
| `4_API_Management.md` | `docs/content/how-to/developers/api-management.md` | Routine tasks for endpoint configuration. |
| `5_Dependabot-Alerts-Management.md` | `docs/content/how-to/developers/dependabot.md` | Procedures to address alerts. |
| `6_Cookies.md` | `docs/content/reference/developers/cookies.md` | Exact cookies schema, lifetimes, and naming rules. |
| `7_Troubleshooting.md` | `docs/content/how-to/developers/troubleshooting.md` | Steps for identifying common dev system failures. |
| `8_Additional-Resources.md` | `docs/content/reference/developers/additional-resources.md`| List of helpful external documents. |
| `9_Rider-Configuration.md` | `docs/content/how-to/developers/rider-configuration.md` | IDE configuration recipe. |

### 4. Guides Mapping (`documentation/guides/`)

| Current Directory | Target Location | Rationale |
| :--- | :--- | :--- |
| `cfr-file-generation/` | `docs/content/how-to/data/cfr-file-generation.md` | Step-by-step instructions for data operators. |
| `chart-inconsistency-rationale/` | `docs/content/explanation/data/chart-inconsistency-rationale.md` | Theoretical context for discrepancies. |
| `chart-principles/` | `docs/content/reference/design/chart-principles.md` | Standards for layout and presentation. |
| `data-release-guide/` | `docs/content/how-to/data/data-release-guide.md` | Guide on releasing dataset versions. |
| `monthly-reporting/` | `docs/content/how-to/operational/monthly-reporting.md` | Standard operational report process. |
| `s251-file-generation/` | `docs/content/how-to/data/s251-file-generation.md` | Operator tasks. |

### 5. Operational Document Mapping (`documentation/operational/`)

| Current File | Target Location | Rationale |
| :--- | :--- | :--- |
| `2_Service-Conditions.md` | `docs/content/reference/operational/service-conditions.md` | Formal details of constraints and SLAs. |
| `3_Incident_Management.md` | `docs/content/how-to/operational/incident-management.md`| Tasks for incident response. |
| `4_Root-Cause-Analysis.md` | `docs/content/how-to/operational/root-cause-analysis.md` | Post-incident analysis procedure. |
| `5_Runbooks.md` (and subfiles) | `docs/content/how-to/operational/runbooks/` | System-operation recipes. |
| `6_Monitoring-Alerting.md` | `docs/content/how-to/operational/monitoring-alerting.md` | Steps for configuring alert levels. |
| `7_Metrics.md` | `docs/content/reference/operational/metrics.md` | Complete lists of KPIs, log sources, and system metrics. |

### 6. Quality Assurance Document Mapping (`documentation/quality-assurance/`)

| Current File/Folder | Target Location | Rationale |
| :--- | :--- | :--- |
| `1_Test-strategy-service.md` | `docs/content/reference/qa/test-strategy-service.md` | System-wide QA definitions and standards. |
| `2_Test-strategy-dynamic-conent.md` | `docs/content/reference/qa/test-strategy-dynamic-content.md`| QA reference for content elements. |
| `3_Test-strategy-data-ingestion.md` | `docs/content/reference/qa/test-strategy-data-ingestion.md`| Reference for validating the pipeline. |
| `4_Test-Approach.md` | `docs/content/reference/qa/test-approach.md` | Baseline definitions of automated scopes. |
| `5_Test-Plan.md` | `docs/content/how-to/qa/test-plan.md` | Practical roadmap to execute regression tasks. |
| `6_Security-Testing.md` | `docs/content/reference/qa/security-testing.md` | Compliance scan rules and schedules. |
| `7_Performance-Testing.md` | `docs/content/reference/qa/performance-testing.md` | Performance budgets, load limits, and setups. |
| `8_Release-Comms.md` | `docs/content/how-to/qa/release-communications.md` | Tasks to execute when distributing notes. |
| `9_DSI-Test-Organisations.md` | `docs/content/reference/qa/dsi-test-organisations.md` | Reference dataset of test account credentials. |
| `10_Data-Release-Test-Approach.md`| `docs/content/reference/qa/data-release-test-approach.md`| QA structure specific to database schema updates. |
| `11_Data-Release-Test-Plan.md` | `docs/content/how-to/qa/data-release-test-plan.md` | Tasks for validating a release candidate dataset. |
| `api-test-plans/` (Folder) | `docs/content/reference/qa/api-test-plans/` | Pure reference structures of API test models. |
| `data-release-test-plans/` | `docs/content/reference/qa/data-release-test-plans/` | Reference checklists for release validation. |
| `performance-test-plans/` | `docs/content/reference/qa/performance-test-plans/` | Load scripts and performance target configurations. |
| `release-test-plans/` | `docs/content/reference/qa/release-test-plans/` | Standard checklist before release approval. |

### 7. Design Document Mapping (`documentation/design/`)

| Current File | Target Location | Rationale |
| :--- | :--- | :--- |
| `01_Chart-Development-Workflow.md` | `docs/content/explanation/design/chart-development-workflow.md`| Conceptual reasoning behind layout iterations. |
| `02_User-Testing.md` | `docs/content/explanation/design/user-testing.md` | Historical user insight synthesis. |
| `03_Progressive-Enhancement.md` | `docs/content/explanation/design/progressive-enhancement.md`| Architectural philosophy behind lightweight clients. |
| `04_Branding.md` | `docs/content/reference/design/branding.md` | Layout, colors, and typography specifications. |

### 8. Features Document Mapping (`documentation/features/`)

All files within `documentation/features/` describe specific system features in terms of operational parameters, business logic, and specification details. Thus, they should be located under:

`docs/content/reference/features/`

| Current File | Target Location |
| :--- | :--- |
| `01_Integration-with-DfE-Signin.md` | `docs/content/reference/features/integration-with-dfe-signin.md` |
| `02_Session-State.md` | `docs/content/reference/features/session-state.md` |
| `03_Incomplete-Data.md` | `docs/content/reference/features/incomplete-data.md` |
| `04_Spending-and-Costs.md` | `docs/content/reference/features/spending-and-costs.md` |
| `05_Find-Organisation.md` | `docs/content/reference/features/find-organisation.md` |
| `06_Orchestrator.md` | `docs/content/reference/features/orchestrator.md` |
| `07_Validation.md` | `docs/content/reference/features/validation.md` |
| `08_Progressive_Enhancements.md` | `docs/content/reference/features/progressive-enhancements.md` |
| `09_Commercial-Resources.md` | `docs/content/reference/features/commercial-resources.md` |
| `10_Service-Banners.md` | `docs/content/reference/features/service-banners.md` |
| `11_Progress-Bandings.md` | `docs/content/reference/features/progress-bandings.md` |
| `13_Chart-Rendering-Api.md` | `docs/content/reference/features/chart-rendering-api.md` |
| `14_Rounding-off-rules-in-service.md`| `docs/content/reference/features/rounding-off-rules-in-service.md`|
