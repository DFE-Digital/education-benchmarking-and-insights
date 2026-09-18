# Release Test Plan: 2026.09.1

Release Date: 18/09/2026

Release Label: 2026.09.1

## Introduction

This document outlines the approach of testing release `2026.09.1` covering the necessary testing activities.

This is the CFR release. It focuses on the Consistent Financial Reporting (CFR) 2025-2026 data drop: the data pipeline configuration and dry run required to ingest and process the 2026 CFR data, together with the data-pipeline-related dependency updates that go out alongside it. For this release we are only releasing the data pipeline code and the associated dependency updates.

Detailed validation of the CFR data content is covered by the CFR data-release test plan ([00005 - CFR Data Release 2025-2026](../data-release-test-plans/00005_CFR-2025-2026-data-release.md)); this plan covers the software and configuration release and the pipeline execution.

## Scope

**In-scope:**

- **Data pipeline (CFR 2026 data release)**
  - Data pipeline configuration and dry run with the 2026 CFR data, enabling ingestion and processing of the CFR 2025-2026 data drop across all supported years. ([326615](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/326615))

- **Maintenance (partial dependency releases)**
  - August 2026 dependency updates. Partial release: only the dependency updates relating to the data-pipeline project are included. ([326253](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/326253))
  - September 2026 dependency updates. Partial release: only the dependency updates relating to the data-pipeline project (those supporting the CFR 2026 data drop) are included. ([327284](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/327284))

**Out-of-Scope:**

- **Non-data-pipeline dependency updates** from the August and September 2026 batches. Deferred; only data-pipeline-related dependencies are released here.
- **LAA risk score page** the work has been done for it but the feature is turned off in production.

## Test Strategy

Functional validation and regression testing were completed in the lower environment. For this release the focus is confirming the promotion behaves as expected in pre-production and production.

- **Sanity Testing (Pre-Prod):** Confirm the data pipeline run completes for the CFR 2026 data drop and that the service displays the 2026 CFR data for maintained schools.
- **Smoke Testing (Pre-Prod):** Verify core functionality behind login is accessible and the pre-production environment is stable.
- **Smoke Testing (Production):** Post-deployment checks to confirm system stability and availability.
- **User Acceptance Testing:** Coordinate with stakeholders to confirm the 2026 CFR data and its presentation meet business needs.

## Entry and Exit Criteria

- Entry Criteria:
  - All code and configuration changes for the release are completed and deployed to the pre-production environment.
  - The CFR parameter/configuration is updated so that the 2026 CFR data is reflected on the service.
  - A data pipeline run has been completed for the CFR 2026 data drop.
  - Functional and regression testing completed in the lower environment.

- Exit Criteria:
  - Pre-production sanity checks confirm the pipeline run completed and the service is showing 2026 CFR data.
  - All planned smoke, sanity and UAT activities are executed and pass.
  - No critical or high-severity defects remain open.
  - Stakeholders confirm readiness for release.

## Roles and Responsibilities

- **QA Lead:** Coordinate sanity, smoke and UAT activities and manage overall sign-off.
- **Engineer(s):** Execute the pipeline run, validation and defect investigation.
- **Data Analyst(s):** Confirm CFR and ancillary source files and data readiness.
- **Stakeholders:** Participate in UAT and provide acceptance sign-off.
- **Technical Lead:** Oversee the overall release and technical quality of the CFR ingestion pipeline.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** Partial dependency releases (August and September 2026) could introduce regressions or dependency conflicts in the data-pipeline project.
  - **Mitigation:** Confirm only the intended data-pipeline packages are bumped, and run pipeline smoke and sanity checks after the update.

## Test Deliverables

- Test plan document
- Pre-production sanity check results (pipeline run and service showing 2026 CFR data)
- Smoke test results (pre-production and production)
- UAT results
- Test summary report

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

To be completed post-release.

**Azure DevOps tickets included in this release:**

- [326615 - Data pipeline config and dry run with 2026 CFR data (CFR data release main ticket)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/326615)
- [326253 - Review and merge Aug '26 dependency updates (partial: data-pipeline dependencies only)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/326253)
- [327284 - Review and merge Sep '26 dependency updates (partial: data-pipeline dependencies only)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/327284)
- [314478 - Surface consent flag in log files](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/314478)
- [317736 - LAA - Create Schools risk score overview page](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/317736)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |
|-------------------------|:-----------:|:------:|:------:|:---------:|
| Sanity Tests - Pre Prod |      1      |   1    |   0    |   100%    |
| Smoke Tests - Pre Prod  |      1      |   1    |   0    |   100%    |
| Smoke Tests - Prod      |      1      |   1    |   1    |   100%    |
| Total                   |      3      |   3    |   3    |   100%    |

**User Acceptance Testing (UAT):** Conducted by clients/stakeholders and recorded as an acceptance sign-off rather than a test count. Outcome: passed.

\newpage
