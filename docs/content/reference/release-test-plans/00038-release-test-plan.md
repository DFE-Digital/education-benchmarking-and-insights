---
title: "Release 00038: 2026.10.1"
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Release 00038: 2026.10.1"
  parent: Release Test Plans
  order: 38
---

_Release version incremented to 2026.10.1 after 2026.10.0 was discarded due to a pupil absence display defect spotted in UAT._

Release Date: TBC

Release Label: 2026.10.1

## Introduction

This document outlines the approach to testing release `2026.10.1`, covering the necessary testing activities.

This is the LAA release. It integrates the Local Authority (LAA) school risk tool into the FBIT service. This involves ingesting the 2025 and 2026 LAA data, processing it to calculate the school risk scores, and displaying the resulting risk pages and downloadable data. It also includes the October 2026 dependency updates.

## Scope

**In-scope:**

- **LAA tool integration into FBIT.** Ingesting the new 2025 and 2026 LAA source files, processing them to calculate the school risk scores, and displaying the relevant pages and data:
  - Data pipeline: ingest and process the 2025 and 2026 LAA data. ([326594](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/326594), [329924](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/329924), [331641](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331641))
  - Service/UI: the individual school risk score page, the trend in risk scores page, the risk score methodology page, and the downloadable risk dataset. ([317737](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/317737), [318456](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/318456), [318457](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/318457), [320418](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/320418))

**Out-of-Scope:**

- **Infrastructure:** Terraform and data-pipeline changes for the LA sitemap. Deployed with the release but not part of pre-production functional or UAT testing. ([327660](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/327660))
- **Maintenance:** October 2026 dependency updates. Included in the release but not functionally tested in pre-production. ([330373](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/330373))
- Changes to the other data releases (CFR, BFR, AAR, S251), which are unchanged by this release.
- Further LAA enhancements planned for subsequent releases.

## Test Strategy

Functional validation and regression testing were completed in the lower environment. For this release the focus is confirming the LAA pages, the downloadable dataset and the 2025 and 2026 risk-score data behave as expected through promotion to pre-production and production, and that the data content is accepted by the client.

- **Sanity Testing (Pre-Prod):** Confirm the LAA risk-score pipeline run completes for the 2025 and 2026 data drop and that the LAA pages (individual school risk score, trend, methodology) and the downloadable CSV display the expected 2025 and 2026 risk data.
- **Smoke Testing (Pre-Prod):** Verify core functionality behind login is accessible, the LAA pages render, and the pre-production environment is stable.
- **Smoke Testing (Production):** Post-deployment checks to confirm system stability and availability, and that the LAA tool is reachable.
- **User Acceptance Testing (Pre-Prod):** Coordinate with stakeholders to confirm the 2025 and 2026 LAA risk scores, the risk pages and the downloadable dataset meet business needs. UAT is carried out in pre-production and signed off by the client.

## Entry and Exit Criteria

- Entry Criteria:
  - All code and configuration changes for the release are completed and deployed to the pre-production environment.
  - The LAA configuration is updated so that the 2025 and 2026 LAA risk-score data is reflected on the service.
  - A data pipeline run has been completed for the 2025 and 2026 LAA risk-score data drop.
  - Functional and regression testing completed in the lower environment.

- Exit Criteria:
  - Pre-production sanity checks confirm the pipeline run completed and the LAA pages and download show the 2025 and 2026 risk data.
  - All planned smoke, sanity and UAT activities are executed and pass.
  - No critical or high-severity defects remain open.
  - Client sign-off of UAT in pre-production is obtained, and stakeholders confirm readiness for release.

## Risks and Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| The sitemap changes require manual steps to be performed in the pre-production and production environments before the release begins. If these manual steps are not completed, the deployment may fail. | Deployment failure and a blocked release. | Carry out the required manual steps proactively in each environment before starting the release. |

## Roles and Responsibilities

- **QA Lead:** Coordinate sanity, smoke and UAT activities and manage overall sign-off.
- **Engineer(s):** Execute the pipeline run, validation and defect investigation.
- **Data Analyst(s):** Confirm the LAA source and ancillary files and data readiness.
- **Stakeholders:** Participate in UAT and provide acceptance sign-off.
- **Technical Lead:** Oversee the overall release and technical quality of the LAA integration and risk-score pipeline.
- **Project Lead:** Own go/no-go decision.

## Test Deliverables

- Test plan document
- Pre-production sanity check results (pipeline run and LAA pages/download showing 2025 and 2026 risk data)
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

During UAT a defect was found in the pupil absence indicator. The "Current % total pupil absence" figure was displayed 100 times too high (for example 571% instead of 5.71%), because the data pipeline held the value in percentage points while the service formats it as a proportion. The risk scores, flags and grades were not affected, but a fix was required in the data pipeline to output the value on the correct scale.

As a result, release 2026.10.0 was discarded and the fix for the absence display issue is included in this release.

- **New Release Version:** 2026.10.1

**Azure DevOps tickets included in this release:**

- [317737 - Individual school risk score page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/317737)
- [318456 - Trend in risk scores page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/318456)
- [318457 - Risk score methodology page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/318457)
- [320418 - Downloadable CSV on the schools risk overview page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/320418)
- [326594 - Update LAA risk tool data for 2025 and 2026](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/326594)
- [327660 - Terraform and pipeline changes for the LA sitemap](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/327660)
- [329924 - Rename risk indicators, add previous-year balance indicator, fix risk groups](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/329924)
- [330373 - Review and merge Oct '26 dependency updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/330373)
- [331641 - LAA data changes round 3 (phase-exclusion and scoring corrections)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/331641)

## Appendix

### Test Summary Report

**Summary of results:** Pre-production testing complete and passed. Production smoke testing pending.

| Test Category           | Total Tests | Passed | Failed | Pass Rate |
|-------------------------|:-----------:|:------:|:------:|:---------:|
| Sanity Tests - Pre Prod |      1      |   1    |   0    |   100%    |
| Smoke Tests - Pre Prod  |      1      |   1    |   0    |   100%    |
| Smoke Tests - Prod      |      1      |  TBC   |  TBC   |    TBC    |
| Total                   |      3      |  TBC   |  TBC   |    TBC    |

**User Acceptance Testing (UAT):** Conducted by clients/stakeholders in pre-production and recorded as an acceptance sign-off rather than a test count. Outcome: TBC.
