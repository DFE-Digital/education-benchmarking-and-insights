---
title: "Release 00039: 2026.11.0"
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Release 00039: 2026.11.0"
  parent: Release Test Plans
  order: 39
---

Release Date: 26/11/2026

Release Label: 2026.11.1

_Release version incremented to 2026.11.1 after 2026.11.0 was discarded SEN year display bug spotted in UAT._

## Introduction

This document outlines the approach of testing release `2026.11.0` covering the necessary testing activities.

This is the S251 release. It focuses on the Section 251 (S251) 2025-2026 data drop: the data pipeline configuration and run required to ingest and process the 2026 S251 data with the updated source and ancillary files, together with a small high-needs benchmarking UI bug fix.

The release also includes the Attainment 8 work for secondary schools. This ships behind a feature flag that remains OFF for this release, so the Attainment 8 pages, data sources changes and benchmarking additions are not surfaced to users and are not functionally tested here.

Detailed validation of the S251 data content is covered by the S251 data-release test plan ([00007 - Section 251 (S251) Data Release 2025-2026](/reference/data-release-test-plans/00007-s251-2025-2026-data-release/)); this plan covers the software and configuration release and the pipeline execution. The data validation checks performed as part of that data release (source-to-database lineage, service/front-end validation of the high-needs spending and history pages, and completeness with year-on-year comparison) are the basis for the sanity checks below.

## Scope

**In-scope:**

- **Data pipeline (S251 2026 data release)**
  - Data pipeline configuration and run with the updated 2026 S251 source and ancillary files, enabling ingestion and processing of the S251 2025-2026 data drop across all supported years, and persistence to the database.
  - Service/UI display of the 2026 S251 high-needs data (high-needs spending and history pages).

- **High needs benchmarking bug fix.** The filter on the high needs benchmarking page listed "Home to school transport (16-18)" twice; the duplicate entry is removed so the filter lists each category once. ([334184](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/334184))

**Out-of-Scope:**

- **Attainment 8 for secondary schools.** The Attainment 8 feature is behind a feature flag that is OFF for this release. The code and schema changes ship with the release but the functionality is not enabled or functionally tested in pre-production or UAT. ([329689](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/329689), [329776](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/329776), [331153](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331153), [331340](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331340), [331535](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331535))
- Detailed validation of the S251 data content, which is covered by the S251 data-release test plan ([00007](/reference/data-release-test-plans/00007-s251-2025-2026-data-release/)).
- Changes to the other data releases (CFR, BFR, AAR), which are unchanged by this release.

## Test Strategy

Functional validation and regression testing were completed in the lower environment. For this release the focus is confirming the promotion behaves as expected in pre-production and production, and that the 2026 S251 data content is accepted by the client.

- **Sanity Testing (Pre-Prod):** Confirm the data pipeline run completed for the S251 2026 data drop and that the service displays the 2026 S251 high-needs data; confirm the high needs benchmarking filter no longer lists "Home to school transport (16-18)" twice; confirm the Attainment 8 feature flag is OFF and no Attainment 8 pages or data are surfaced.
- **Smoke Testing (Pre-Prod):** Verify core functionality behind login is accessible and the pre-production environment is stable.
- **Smoke Testing (Production):** Post-deployment checks to confirm system stability and availability.
- **User Acceptance Testing (Pre-Prod):** Coordinate with stakeholders to confirm the 2026 S251 data and its presentation meet business needs. UAT is carried out in pre-production and signed off by the client.

## Entry and Exit Criteria

- Entry Criteria:
  - All code and configuration changes for the release are completed and deployed to the pre-production environment.
  - The S251 configuration is updated so that the 2026 S251 data is reflected on the service.
  - A data pipeline run has been completed for the S251 2026 data drop with the updated data files.
  - The S251 data content has been validated per the data-release test plan ([00007](/reference/data-release-test-plans/00007-s251-2025-2026-data-release/)): source-to-database lineage, service/front-end validation of the high-needs spending and history pages, and completeness with year-on-year comparison.
  - The Attainment 8 feature flag is confirmed OFF.
  - Functional and regression testing completed in the lower environment.

- Exit Criteria:
  - Pre-production sanity checks confirm the pipeline run completed and the service is showing 2026 S251 data, the duplicate transport filter is resolved, and no Attainment 8 functionality is surfaced.
  - All planned smoke, sanity and UAT activities are executed and pass.
  - No critical or high-severity defects remain open.
  - Client sign-off of UAT in pre-production is obtained, and stakeholders confirm readiness for release.

## Risks and Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| Attainment 8 code and schema changes ship behind a feature flag. If the flag is inadvertently enabled, incomplete Attainment 8 functionality could surface to users. | Unverified Attainment 8 data and UI visible in pre-production or production. | Confirm the Attainment 8 feature flag is OFF in pre-production and production as part of the sanity and smoke checks. |

## Roles and Responsibilities

- **QA Lead:** Coordinate sanity, smoke and UAT activities and manage overall sign-off.
- **Engineer(s):** Execute the pipeline run, validation and defect investigation.
- **Data Analyst(s):** Confirm the S251 and ancillary source files and data readiness.
- **Stakeholders:** Participate in UAT and provide acceptance sign-off.
- **Technical Lead:** Oversee the overall release and technical quality of the S251 ingestion pipeline.
- **Project Lead:** Own go/no-go decision.

## Test Deliverables

- Test plan document
- Pre-production sanity check results (pipeline run and service showing 2026 S251 data; duplicate filter resolved; Attainment 8 flag off)
- Smoke test results (pre-production and production)
- UAT results and client sign-off
- Test summary report

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

During UAT a FE bug was discovered that caused incorrect year being displayed for SEN data. For the reason the release was discarded and a new release was created with the fix.

**Azure DevOps tickets included in this release:**

- [329689 - Data set up for Attainment 8 for secondary schools](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/329689)
- [329776 - API changes required for Attainment 8](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/329776)
- [331153 - Create new columns in nonfinancial and LA-nonfinancial tables](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331153)
- [331340 - Add Attainment 8 scores on school homepage and change to data sources page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331340)
- [331535 - Add Attainment 8 to benchmark spending page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331535)
- [334184 - High needs benchmarking: filter shows "Home to school transport (16-18)" twice](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/334184)
- [333315 - S251: 2026 release prep](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/333315)

## Appendix

### Test Summary Report

**Summary of results:** To be completed post-release.

| Test Category           | Total Tests | Passed | Failed | Pass Rate |
|-------------------------|:-----------:|:------:|:------:|:---------:|
| Sanity Tests - Pre Prod |      1      |   1    |   0    |   100%    |
| Smoke Tests - Pre Prod  |      1      |   1    |   0    |   100%    |
| Smoke Tests - Prod      |      1      |   1    |   0    |   100%    |
| Total                   |      3      |   3    |   0%   |   100%    |

**User Acceptance Testing (UAT):** Conducted by clients/stakeholders in pre-production and recorded as an acceptance sign-off rather than a test count. UAT was successful and the readiness of release was confirmed.
