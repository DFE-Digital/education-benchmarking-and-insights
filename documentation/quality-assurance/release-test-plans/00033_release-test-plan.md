# Release Test Plan: 2026.05.0

**Release Date:** TBC  
**Release Label:** 2026.05.0

## Introduction

This plan defines the approach for testing release `2026.5.0`, covering smoke, sanity testing activities.  
This release delivers a combination of Local Authority (LA) journey updates including revised high needs benchmarking frameworks, separated education healthcare plans, server-side chart rendering components, and platform dependency updates.

## Scope

**In-scope:**

- **Enhancements**
  - Implementing server-side rendering architecture for multi-series horizontal bar charts to optimise front-end visualisation performance.
  - Enchanged High needs benchmarking page with additional data points and standardisation options to provide deeper insights into LA spending patterns.
  - Executing structural revisions on the Local Authority homepage layout to optimise navigation.
  - Separating the education, health, and care plans (EHCP) into a dedicated individual area.

- **Bug Fixes**
  - N/A - No specific bug fixes are included in this release.

- **Maintenance**
  - Reviewing and merging the critical May '26 platform and package dependency updates.

**Out-of-Scope:**

- Any feature development unrelated to the items listed above.
- No large-scale data refreshes beyond those explicitly included.

## Test Strategy

- **Sanity Testing:** Validate that the application deploys successfully to pre-production and operates as expected with the updated changes.
- **Smoke Testing:** Execute smoke tests in pre-production to ensure the features behind login are working as expected.
- **Smoke Testing:** Execute smoke tests in production to confirm platform stability and availability post-deployment and also execute smoke tests in pre prod to check the features behind login are working as expected.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.
- Data pipeline run completed with the updated files.
- Pre-production pipeline run is successfully completed.
- Manual feature flag is toggled.

**Exit Criteria:**

- All smoke and sanity checks pass successfully.
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke, sanity testing and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Provide acceptance sign-off where required.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** The feature flag must be manually toggled and supplemented with post-release code changes to guarantee it stays in the toggled state, incorrectly applying these changes could introduce a risk.
  - **Mitigation:** Immediate post-deployment validation after feature flag is toggled.
- **Risk:** May '26 dependency updates could introduce regressions or dependency conflicts across unrelated platform paths.
  - **Mitigation:** Run extended smoke checks on critical user journeys.

## Test Deliverables

- Test plan document
- Test cases (smoke, sanity)
- Test execution results and defect logs
- Test summary report with final release recommendation

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

{add notes here}
**Azure DevOps tickets included in this release:**

- [286015 - High needs - additional data (iteration for s251)](https://dfe-ssp.visualstudio.com/_workitems/edit/286015)
- [287958 - Server side rendering of multi series horizontal bar charts](https://dfe-ssp.visualstudio.com/_workitems/edit/287958)
- [295178 - Benchmark high needs spending standardisation options](https://dfe-ssp.visualstudio.com/_workitems/edit/295178)
- [295179 - Benchmark high needs spending](https://dfe-ssp.visualstudio.com/_workitems/edit/295179)
- [299439 - LA Homepage revision](https://dfe-ssp.visualstudio.com/_workitems/edit/299439)
- [299498 - Benchmark education, health and care plans](https://dfe-ssp.visualstudio.com/_workitems/edit/299498)
- [312542 - Review & merge May '26 dependency updates](https://dfe-ssp.visualstudio.com/_workitems/edit/312542)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      0      |   0    |   0    |    0%     |  
| Smoke Tests - Pre Prod  |      0      |   0    |   0    |    0%     |  
| Sanity Tests - Pre Prod |      0      |   0    |   0    |    0%     |  
| Total                   |      0      |   0    |   0    |    0%     |  

\newpage
