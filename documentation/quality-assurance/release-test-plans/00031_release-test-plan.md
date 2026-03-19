# Release Test Plan: 2026.3.0

**Release Date:** TBC  
**Release Label:** 2026.3.0

## Introduction

This plan defines the approach for testing release `2026.3.0`, covering smoke, sanity, and targeted validation activities.  
This release delivers a combination of dependency updates, financial methodology improvements, data corrections, template changes, WAF-related fixes, and enhancements to funding and recoupment calculations across the service.

## Scope

**In-scope:**

- **Enhancements** 

- {make these below meaning full} 
    - In-year balance and revenue reserve historical trending improvements.
    - Senior Leadership Breakdown added to relevant benchmarking and workforce views.
    - Incorporation of low-level recoupment into benchmarking charts.
    - Revenue reserve calculation revision for the FBIT service (WIP).
    - High-level funding, deficits, and recoupment updates (WIP).

- **Bug Fixes**
    - Academies incorrectly displayed with LAMS in LA View School Spending and View Pupil & Workforce Data.
    - ILR pupil numbers added after revenue reserve calculations.
    - School search blocked by WAF—issue resolved.
    - RouteValuesOnClear updated to remove unsupported collection‑expression syntax under allowed SDK versions.
    - Removal of rows from the 2024 AAR input.


- **Maintenance**
    - Review & merge February and March ’26 dependency updates.
    - Downgrade Chart Rendering Function App Plan from EP1 to Y1 (requires production changes).

**Out-of-Scope:**

- Any feature development unrelated to the items listed above.
- No large-scale data refreshes beyond those explicitly included.

## Test Strategy

- **Sanity Testing:** Validate that the application deploys successfully to pre-production and operates as expected with updated dependencies, financial logic changes, template updates, and WAF fixes.
- **Smoke Testing:** Execute smoke tests in production to confirm platform stability and availability post-deployment.
- **Targeted Validation:**
    - Validate trending logic, recoupment incorporation, and revenue reserve calculation changes.
    - Validate AAR template updates and removal of 2024 rows.
    - Validate corrected ordering of ILR pupil numbers.
    - Validate school search functionality post-WAF fix.
    - Validate LA views no longer display academies with LAMS.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.
- Pre-production pipeline run is successfully completed.

**Exit Criteria:**

- All smoke and sanity checks pass successfully.
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.
- Production changes for Function App Plan downgrade completed successfully.

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke, sanity, and targeted validation, and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Provide acceptance sign-off where required.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis { add risks here}

- **Risk:** Dependency updates may introduce regressions across unrelated components.
    - **Mitigation:** Run extended smoke checks on critical user journeys.

- **Risk:** WAF-related fixes may not fully resolve school search blocking.
    - **Mitigation:** Perform targeted search validation tests.

- **Risk:** Function App Plan downgrade may impact chart rendering performance.
    - **Mitigation:** Conduct performance checks post-deployment.

## Test Deliverables

- Test plan document
- Test cases (smoke, sanity validation)
- Test execution results and defect logs
- Test summary report with final release recommendation

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

{to be completed post relese }

**Azure DevOps tickets included in this release:**

- [290282 - Review & merge February and March '26 dependency updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/290282)
- [290722 - In year balance and revenue reserve historical trending](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/290722)
- [297409 - LA - Academies displayed with LAMS in View School Spending and View pupil and workforce data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/297409)
- [297723 - Incorporate low level recoupment into benchmarking charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/297723)
- [302784 - ILR pupil numbers added after revenue reserve calculations are made](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/302784)
- [303081 - School search blocked by WAF](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/303081)
- [303287 - Remove rows from 2024 AAR input](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/303287)
- [303965 - RouteValuesOnClear uses unsupported collection‑expression syntax](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/303965)
- [287941 - High level funding, deficits and recoupment - WIP](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/287941)
- [305819 - Revenue reserve calculation revision (FBIT service) - WIP](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/305819)
- [304148 - Downgrade Chart Rendering Function App Plan from EP1 to Y1](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/304148)

## Appendix

### Test Summary Report

**Summary of results:**  
(To be completed post‑testing)

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      -      |   -    |   -    |     -     |  
| Sanity Tests - Pre Prod |      -      |   -    |   -    |     -     |  
| Total                   |      -      |   -    |   -    |     -     |  

<!-- Leave the rest of this page blank -->
\newpage
