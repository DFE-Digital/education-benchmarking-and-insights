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
