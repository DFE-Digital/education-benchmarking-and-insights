# Release Test Plan: 2025.12.2

_*Release version updated to 2025.12.2 following resolution of TotalPupils count for local authorities which have federations which was incorrectly computed previously._*

**Release Date:** TBC  
**Release Label:** 2025.12.2

## Introduction

This plan defines the approach for testing release `2025.12.2`, covering smoke, sanity, and UAT testing activities.  
This release delivers a combination of dependency updates, branding alignment with the GOV.UK and DfE Design Systems, S251 data refresh, and a change to normalisation methodology for Pupil count in LAs.

## Scope

**In-scope:**

- **Data Updates**

  - Ingested S251 data into the service for 2025.
  - CFR 2024/2025 transparency file added to the service.

- **Enhancements**

  - Added a total pupil count for each Local Authority. This figure represents the combined number of pupils across all schools in that area, providing a more complete and accurate picture of the population. This new measure now replaces the previous population figures used in the service.
  - Updated FBIT branding to align with the GOV.UK Design System.
  - Integrated DfE Design System branding elements into the service.

**Out-of-Scope:**

- Any feature development unrelated to branding, normalisation, or S251 ingestion.
- Reviewed and merge December ’25 dependency updates to ensure platform compatibility, stability, and security compliance.

## Test Strategy

- **Sanity Testing:** Validate the s251 2025 data is updated successfully.
- **Smoke Testing:** Execute smoke tests to confirm platform stability and availability post-deployment.
- **UAT Testing:** UAT will focus on validating the 2025 S251 data load in pre‑production to ensure the ingestion process is correct and the data is surfaced accurately across the service.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.
- Pre‑production pipeline run is successfully completed with the new s251 and supporting files.

**Exit Criteria:**

- All sanity checks pass successfully.
- UAT confirms correct ingestion and display of 2025 S251 data.
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke, sanity, and data validation, and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Conduct UAT and provide acceptance sign-off.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** Incorrect S251 data ingestion may lead to inaccurate financial benchmarking.
  - **Mitigation:** UAT will validate the full 2025 S251 dataset in pre‑production before release.

## Test Deliverables

- Test plan document
- Test cases (smoke, sanity, UAT)
- Test execution results and defect logs
- Test summary report with final release recommendation

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

{fill in these details after release is completed }

**Release (First Update):**

The initial release encountered an issue with TotalPupils calculations for LAs. A fix was put in place for that.

- **Original Planned Release:** 2025.12.1
- **Hotfix Release Version:** 2025.12.2
- **Issue Identified:** TotalPupils were calculated incorrectly for LAs with has federations.
- **Fix Implemented:** TotalPupils computations logic updated to exclude Non Lead federations schools when computing the total pupils.
- **Testing Impact:** Verify the changes are reflected along with no regression issue has surfaced with the change.

**[Azure Release Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=294446&suiteId=294447)**

**Azure DevOps tickets included in this release:**

- [290280 - Review & merge December '25 dependency updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/290280)
- [292016 - Update FBIT branding to align with GOV.UK Design System](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/292016)
- [293170 - Transition from population based to pupil based normalisation](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/293170)
- [293202 - S251 data ingestion](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/293202)
- [292017 - Integrate DfE Design system branding elements into the service](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/292017)

## Appendix

### Test Summary Report

**Summary of results:**  
(To be completed post‑testing)

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      -      |   -    |   -    |     -     |  
| Sanity Tests - Pre Prod |      2      |   2    |   0    |   100%    |  
| UAT - Pre Prod          |      -      |   -    |   -    |     -     |  
| Total                   |      -      |   -    |   -    |     -     |  

<!-- Leave the rest of this page blank -->
\newpage
