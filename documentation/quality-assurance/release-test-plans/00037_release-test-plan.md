# Release Test Plan: 2026.10.0

Release Date: 29/09/2026

Release Label: 2026.10.0

## Introduction

This document outlines the approach of testing release `2026.10.0` covering the necessary testing activities.

This is the BFR release. It focuses on the Budget Forecast Return (BFR) 2025-2026 data drop: the data pipeline configuration and dry run required to ingest and process the 2026 BFR data, together with the data-pipeline-related dependency updates that go out alongside it. For this release we are only releasing the data pipeline code and the associated dependency updates.

This release also covers some tweaks to the CFR transparency file generation.

Detailed validation of the BFR data content is covered by the BFR data-release test plan ([00006 - BFR Data Release 2025-2026](../data-release-test-plans/00006_BFR-2025-2026-data-release.md)); this plan covers the software and configuration release and the pipeline execution.

## Scope

**In-scope:**

- **Data pipeline (BFR 2026 data release)**
  - Data pipeline configuration and dry run with the 2026 BFR data, enabling ingestion and processing of the BFR 2025-2026 data drop across all supported years.
  - Bug fix for BFR metrics table.
  - Tweaks for the CFR transparency file generation.

- **Maintenance (partial dependency releases)**
  - September 2026 dependency updates.

## Test Strategy

Functional validation and regression testing were completed in the lower environment. For this release the focus is confirming the promotion behaves as expected in pre-production and production.

- **Sanity Testing (Pre-Prod):** Confirm the data pipeline run completes for the BFR 2026 data drop and that the service displays the 2026 BFR data.
- **Smoke Testing (Pre-Prod):** Verify core functionality behind login is accessible and the pre-production environment is stable.
- **Smoke Testing (Production):** Post-deployment checks to confirm system stability and availability.
- **User Acceptance Testing:** Coordinate with stakeholders to confirm the 2026 BFR data and its presentation meet business needs.

## Entry and Exit Criteria

- Entry Criteria:
  - All code and configuration changes for the release are completed and deployed to the pre-production environment.
  - The BFR parameter/configuration is updated so that the 2026 BFR data is reflected on the service.
  - A data pipeline run has been completed for the BFR 2026 data drop.
  - Functional and regression testing completed in the lower environment.

- Exit Criteria:
  - Pre-production sanity checks confirm the pipeline run completed and the service is showing 2026 BFR data.
  - All planned smoke, sanity and UAT activities are executed and pass.
  - No critical or high-severity defects remain open.
  - Stakeholders confirm readiness for release.

## Roles and Responsibilities

- **QA Lead:** Coordinate sanity, smoke and UAT activities and manage overall sign-off.
- **Engineer(s):** Execute the pipeline run, validation and defect investigation.
- **Data Analyst(s):** Confirm BFR and ancillary source files and data readiness.
- **Stakeholders:** Participate in UAT and provide acceptance sign-off.
- **Technical Lead:** Oversee the overall release and technical quality of the BFR ingestion pipeline.
- **Project Lead:** Own go/no-go decision.

## Test Deliverables

- Test plan document
- Pre-production sanity check results (pipeline run and service showing 2026 BFR data)
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

- [328999 - CFR Transparency file changes](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_boards/board/t/FBIT/Stories?workitem=328999)
- [329998 - BFR 2026 Schemas](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2074?workitem=329342)
- [329680 - BFR bugfix: Slope/Slope Flag metrics resolve as null](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_boards/board/t/FBIT/Stories?workitem=329680)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |
|-------------------------|:-----------:|:------:|:------:|:---------:|
| Sanity Tests - Pre Prod |      1      |   1    |   0    |   100%    |
| Smoke Tests - Pre Prod  |      1      |   1    |   0    |   100%    |
| Smoke Tests - Prod      |      1      |   1    |   0    |   100%    |
| Total                   |      3      |   3    |   0    |   100%    |

**User Acceptance Testing (UAT):** Conducted by clients/stakeholders and recorded as an acceptance sign-off rather than a test count. Outcome: passed.

\newpage
