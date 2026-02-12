# Release Test Plan: 2026.02.0 <!-- TODO: confirm label -->

**Release Date:** 2026/02/12 <!-- TODO: confirm date -->
**Release Label:** 2026.02.0 <!-- TODO: confirm label -->

## Introduction

This plan defines the approach for testing release `2026.02.0`<!-- TODO: confirm label -->, covering smoke and sanity testing activities.  
This release delivers an enhancement to the high needs benchmarking journey with a redesign for the selection of comparators. Statistical neighbours for the local authority are now pre-selected as default and the layout of the page has been updated.

## Scope

**In-scope:**

- **Enhancements**
  - Choose local authorities to compare high needs spending page now has statistical neighbours pre-selected as default and the layout has been refreshed.

- **Bug Fixes**
  - Senior Leadership - stray semicolon in razor markup

**Out-of-Scope:**

- Reviewed and merge January ’26 dependency updates to ensure platform compatibility, stability, and security compliance.
- Request blocked by WAF
- Establish route to get data out of databricks

## Test Strategy

- **Sanity Testing:** Validate that the application is deployed successfully to pre-production and operates as expected including benchmark high needs journey from comparator selection through to viewing the leadership group page once feature flag is enabled.
- **Smoke Testing:** Execute smoke tests in production to confirm platform stability and availability post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.

**Exit Criteria:**

- All smoke and sanity checks pass successfully.
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke and sanity testing, and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Provide acceptance sign-off where required.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** WAF updates can only be applied and validated in production environments, meaning they cannot be fully exercised in standard lower‑tier environments
  - **Mitigation:** The changes have been deployed and verified in a production‑like feature environment to provide confidence that they will behave as expected.

## Test Deliverables

- Test plan document
- Test cases (smoke testing)
- Test execution results and defect logs
- Test summary report with final release recommendation

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

<!-- TODO: update with outcome for this release -->

**Azure DevOps tickets included in this release:**

- [280605 - Comparator selection](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/280605)
- [299146 - Request blocked by WAF](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/299146)
- [299029 - Senior Leadership - error in razor markup](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/299029)
- [290281 - Reviewed and merge January ’26 dependency updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/290281)
- [296814 - Establish route to get data out of databricks](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/296814)

## Appendix

### Test Summary Report

**Summary of results:**  

<!-- TODO: update with outcome for this release and test results -->

| Test Category                | Total Tests | Passed | Failed | Pass Rate |  
|------------------------------|:-----------:|:------:|:------:|:---------:|  
| Sanity tests - Pre-Prod      |      -      |   -    |   -    |   -       |
| Smoke tests - Prod           |      -      |   -    |   -    |   -       |
| Total                        |      -      |   -    |   -    |   -       |  

<!-- Leave the rest of this page blank -->
\newpage
