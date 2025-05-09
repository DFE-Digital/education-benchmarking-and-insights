# Release Test Plan - 2025.05.1

_*Release version updated to 2025.05.1 after 2025.05.0 was adjusted to Enable the High Needs feature flag in code._

**Release Date:** 09/05/2025
**Release Label:** 2025.05.1

## Introduction

This plan defines the approach for testing release `2025.05.1`, covering all functional, smoke testing necessary.  
Ensure that enhancements, and updates in `2025.05.1` are functioning as expected without adversely impacting existing functionality.

This release primarily focuses on implementing Front Door redirection changes required for SFB decommissioning, along with selected tech debt updates. Certain features included in the release will remain disabled in production.

## Scope

**In-scope:**

- Updates and Enhancements
  - Implemented Front Door redirects to support SFB decommissioning, ensuring that all relevant routes are correctly redirected.
  - Updated Revenue Reserve calculations to accurately reflect and added Share of revenue reserves.

**Out-of-Scope:**

- Disabled filtered search feature
- Tech Debt Items
- Any new functionality not targeted for this release
- Dependency updates

## Test Strategy

- Sanity Testing: Check the revenue reserve figures are computed and updated correctly.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and deployed to the pre-production environment.
- Pipeline run is completed.

**Exit Criteria:**

- All high-priority test cases pass.
- No critical defects remain open.
- Signed off by stakeholders.

## Roles and Responsibilities

- **QA lead:** Coordinate testing activities, manage test cases and defect triage.
- **Engineer(s):** Execute test cases, report and retest defects.
- **Stakeholders:** Participate in user acceptance testing and provide final approval.
- **Technical lead:** Oversee release planning.
- **Project lead:** Go/no-go decisions.

## Risk Analysis

- **Risk:** Making DNS changes for SFB in production carries potential risks, as testing is only feasible within the live environment.  
  - **Mitigation:** Front door services have been tested using feature environments. If any issues arise, redirects will be manually rolled back to minimise impact.

- **Risk:** Bugs & Defects in Production. Unexpected software defects can cause system crashes, data corruption, or functional failures.
  - **Mitigation:** Conduct thorough testing (unit, integration, regression testing). Implement automated testing to catch issues early.

## Test Deliverables

- Test plan document
- Test cases (Functional, Smoke)
- Test summary report outlining test results, pass/fail rates, and any outstanding issues

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Release Overview:**

The High Needs feature flag was found to be disabled in code in version 2025.05.0. As a result, that release was abandoned, and an update was included in version 2025.05.1.

- **Original Planned Release:** 2025.05.0
- **New Release Version:** 2025.05.1
- **Hotfixes Included:** HighNeeds Feature flag was enabled in code.
- **Current Release (2025.05.1):** Contains the necessary hotfix.
- **Testing Impact:** No impact to the release testing plan.

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=259145&suiteId=259146)**

**Azure DevOps tickets:**

- [260470 - Front Door redirects for SFB Decommissioning](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/260470)
- [258469 - Revenue reserve/Share of Revenue reserve](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/258469)
- [254608 - Route parameter validation (Partial, remainder to follow in a later release)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/254608)
- [256576 - Failing Platform.Search deployments should break pipeline run](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/256576)
- [258443 - Apply CancellationTokens to all remaining Web proxy and related API endpoints](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/258443)
- [259620 - Move remaining Platform API functions to Features structure](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/259620)
- [259646 - April 2025 dependencies](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/259646)

**Azure DevOps tickets included however feature disabled and not tested:**

- [258757 - Reinstate typeahead/autocomplete for standalone organisation search pages](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/258757)
- [250281 - School Journey - Filtered search (Disabled, not tested in production)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/250281)
- [250287 - Trust Journey - Filtered search (Disabled, not tested in production)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/250287)
- [250286 - Local Authority Journey - Filtered search (Disabled, not tested in production)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/250286)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category | Total Tests | Passed | Failed | Pass Rate |
|---------------|:-----------:|:------:|:------:|:---------:|
| Smoke Tests   |      X      |   X    |   0    |    X%     |
| Sanity Tests  |      1      |   1    |   0    |   100%    |
| Total         |      X      |   X    |   0    |    X%     |

<!-- Leave the rest of this page blank -->
\newpage
