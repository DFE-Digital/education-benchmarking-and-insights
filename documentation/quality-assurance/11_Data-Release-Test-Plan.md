# Test Plan: Data Releases

## Purpose

This document is the practical planning layer for the annual data releases. It sits beneath the data release strategy in [Test Strategy: Data Ingestion](./3_Test-strategy-data-ingestion.md) and the [Data Release Test Approach](./10_Data-Release-Test-Approach.md), and above the dated per-release plans in [data-release-test-plans/](./data-release-test-plans/). Its job is to make producing an individual release plan fast and consistent by providing a reusable template.

## How This Differs From Strategy and Approach

Following the same split as the service documents (see the service [Test Plan](./5_Test-Plan.md)):

- The [Data Ingestion Test Strategy](./3_Test-strategy-data-ingestion.md) defines why, what and where, including the Data Releases variation matrix.
- The [Data Release Test Approach](./10_Data-Release-Test-Approach.md) defines how QA works during a release.
- This Test Plan defines when each individual plan is produced and provides the template to generate it.

## How To Use This Plan

1. Copy the [template](#reusable-per-release-plan-template) below into [`data-release-test-plans/`](./data-release-test-plans/) as the next sequence number, named `0000N_<RELEASE>-<YYYY-YYYY>-data-release.md`.
2. Fill the placeholders using the Per-Release Variation Matrix in [Test Strategy: Data Ingestion](./3_Test-strategy-data-ingestion.md): choose the correct files, keep only the layers that apply, and add release specific business logic.
3. For a CFR release from 2025-2026 onward, keep the optional LAA Risk Indicator Validation block.
4. Tick the exit criteria only as they are genuinely met.

## When Each Plan Is Produced

Individual plans are produced ahead of each release, on the calendar documented in [`data/05_Releases.md`](../data/05_Releases.md).

## Reusable Per-Release Plan Template

Copy the block below and replace the `<...>` placeholders. It mirrors the structure of the existing plans in [`data-release-test-plans/`](./data-release-test-plans/); refer to the [Approach](./10_Data-Release-Test-Approach.md) for how each validation layer is performed rather than restating it in the plan.

---

```markdown
# Test Plan: <RELEASE> Data Release – <YYYY-YYYY>

## Purpose

This plan defines the QA strategy to validate the Data Release for the **<full release name> (<RELEASE>) covering the <YYYY-YYYY> period**. The primary focus is the integrity of the ingestion and transformation pipeline for <primary file(s)> into the FBIT platform, the accurate integration of ancillary datasets, and the verification of data availability and accuracy within the service.

## Scope

### In Scope

- **Schema & Structural Validation:** Contract checking for the primary <RELEASE> file(s) and all listed ancillary datasets.
- **End to End (E2E) Pipeline:** Monitoring the release from raw file landing to database persistence.
- **Data Reconciliation:** Database records, row counts and key joins match source totals.
- **Service/UI Validation:** Front end verification for the <YYYY> reporting year.
- **Regression Testing:** Historical <RELEASE> data (<prior year> and earlier) remains unchanged.
- <Transparency file integration, CFR and AAR only>
- <LAA risk indicator validation, CFR only>

### Out of Scope

- Accuracy of the raw source data (upstream responsibility of Data Analysts).

## Test Data Profile

| Category | Files / Sources |
| :--- | :--- |
| **Primary <RELEASE>** | <primary file(s)> |
| **Organisational** | <gias.csv, gias_links.csv, if applicable> |
| **Census / Ancillary** | <ancillary files, if applicable> |
| **Transparency** | <transparency file, CFR and AAR only> |

## Test Activities & Methodologies

### Schema & File Integrity Validation

**Goal:** Ensure structural integrity to prevent pipeline failures and schema level data loss.

- Constraint checking: headers, data types, mandatory or non-nullable fields.
- Identifier consistency: <URNs, Trust UIDs or LA codes> follow the standard format.
- Uniqueness: no duplicate primary keys.

### Ancillary Data & Completeness Reporting

Omit this activity where the release has no ancillary data (for example BFR).

**Goal:** Quantify data readiness and identify gaps before final processing.

- Cross reference ancillary data to the primary identifiers.
- Generate volume, orphan and completeness percentage reports (reuse the completeness script).

### Business Logic: <name>

Include only where the release has specific logic (for example AAR CS fund allocation, BFR three year forecast, S251 budget and outturn).

**Goal:** Verify the mathematical accuracy of the transformation.

- <release specific checks; ensure totals reconcile with zero variance>

### Database & Pipeline Validation

**Goal:** Ensure clean processing and relational integrity.

- Trigger ingestion; monitor logs for silent drops, duplicates or warnings.
- Reconcile row counts and key joins against source totals.
- Run regression guardrails or checksums on prior year data.

### Service/UI Validation

**Goal:** Final user acceptance of the data presentation.

- Verify the <YYYY> toggle loads the correct dataset.
- Spot check high value metrics against source files.
- Confirm graceful handling of missing data.

### Regression Testing

**Goal:** Prevent degradation of historical datasets.

- Confirm previous years' <RELEASE> data is unchanged via comparison queries.

### Transparency File Integration

Include for CFR and AAR only.

**Goal:** Ensure the transparency file is correctly produced and matches the source.

- Compare the generated transparency file against the inputs (1:1 mapping) and verify a sample via manual computation with zero variance.

### LAA Risk Indicator Validation

Include for CFR only, from the 2025-2026 cycle (see decision 0023).

**Goal:** Validate the LAA risk indicators refreshed alongside CFR.

- Confirm LAA processing runs on its custom trigger after the CFR refresh completes.
- Assure the six non-persisted non-financial datapoints via the input files, the parquet saved at calculation time, and the webpage data download (not the database).
- Confirm the headline and breakdown denormalised tables are populated and drive the two new webpages.
- Validate the three risk categories (Financial, Educational performance, School and Pupil) and confirm per year calculation versioning.
- Regression: confirm existing FBIT view latency (for example National Averages) is unaffected.

## Responsibilities & Environment

Responsibilities and environments follow the Data Ingestion Test Strategy and the Data Release Test Approach. Name the specific people for this release below.

| Role | Responsibility |
| --- | --- |
| **Data Analyst(s)** | Produce and review source and ancillary files before the release. |
| **Data Engineer** | Load files, execute pipeline runs, provide logs, support technical testing. |
| **QA Lead** | Prepare test plan and scripts; manage overall execution. |
| **Engineer(s)** | Assist with test execution, validation and regression scripts under QA guidance. |
| **Technical Lead** | Oversee technical quality and architectural integrity of the pipeline. |
| **Stakeholders** | Conduct UAT and provide formal sign-off. |
| **Project Lead** | Final Go/No-Go decision for the release. |

## Exit Criteria (Sign-off Requirements)

- [ ] All primary and ancillary schemas pass validation
- [ ] Completeness report generated and reviewed (where applicable)
- [ ] Release specific business logic verified with zero variance (where applicable)
- [ ] Pipeline completes E2E with no High or Critical errors
- [ ] Database reconciles to source totals with expected mappings
- [ ] Regression tests confirm historical data integrity
- [ ] UI displays <YYYY> data accurately across metrics and filters
- [ ] Transparency file verified (CFR and AAR only)
- [ ] LAA risk indicators verified via files, parquet and data download (CFR only)
- [ ] Stakeholder sign-off and Project Lead Go decision recorded
```

---

## Deliverables and Sign-off

Each release produces, and links from its individual plan, the completed plan with exit criteria met, the validation scripts and query outputs, the ancillary completeness report (where applicable), the transparency and LAA evidence (CFR, AAR), the UAT notes, and the recorded Go/No-Go decision. This follows the deliverables approach in the service [Test Plan](./5_Test-Plan.md).

## Living Plan

Like the service [Test Plan](./5_Test-Plan.md), this evolves each cycle. When a decision changes what a release must test, as [decision 0023](../architecture/decisions/0023-laa-risk-indicators-data-architecture.md) did for CFR and LAA, update the strategy, approach and template so the next generated plan inherits the change.

<!-- Leave the rest of this page blank -->
\newpage
