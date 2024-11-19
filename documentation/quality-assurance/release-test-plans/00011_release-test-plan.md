# Release Test Plan - 2024.11.2
_*Release label incremented due to pipeline failures._

**Release Date:** 18/11/2024

**Release Label:** 2024.11.2

## Introduction

This plan defines the approach for testing release `2024.11.2`, covering all functional testing necessary.

Ensure that enhancements, and bug fixes in `2024.11.2` are functioning as expected without adversely impacting existing 
functionality.

## Scope

**In-scope:**
- Enhancements
  - Aggregation of contextual data for whole Federation.
  - Revised list of commercial resources.
  - Updated infrastructure configuration.
  - Improvements to data pipeline.
    - Default and custom workload separation.
    - Cleanup of unused data points in input schema.
- Bug fixes 
  - Correction of typo on CFP journey.
  - Revised calculation for AAR total expenditure.
  - Address accessibility issues on spending prioritising page.
  - Fixes LA homepage unavailable when unmapped school phase.
  - Updated API endpoints to handle Trusts & LA's with large number of schools.

**Out-of-Scope:**
- Benchmark report cards.
- Any new functionality not targeted for this release.

## Test Strategy

- Functional Testing:
  - Features: Test updated features for correct functionality.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**
- All code changes for release are completed and deployed to the pre-production environment.
- Pre-production environment is set up with required data.
- Verify feature flag for benchmarking report cards, to ensure feature is disabled.

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

- **Risk:** Pending pipeline jobs are lost when new queues are added.
    - **Mitigation:** All queue to be left in place. With any pending jobs to be manually moved to the relevant new queue.

## Test Deliverables

- Test plan document.
- Test cases (Functional).
- Test summary report outlining test results, pass/fail rates, and any outstanding issues.

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=237478&suiteId=237479)**

**Azure DevOps tickets:**
- [234742 : Aggregation of federation contextual data](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234742)
- [233625 : Updated commercial resources](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/233625)
- [231301 : Prevent accidental destruction of sensitive resources](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231301)
- [231303 : Ensure soft delete and versioning enabled for key vaults and storage accounts](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231303)
- [229479 : Sync Key Vault referenced secrets during Platform API / Web App deployments](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/229479)
- [235469 : Wording on Scenario planner on ICFP feature is incorrect](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235469)
- [235614 : Package dependency updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235614)
- [235396 : Incorrect total expenditure for academies](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235396)
- [217530 : Separate data pipeline into two workloads](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217530)
- [231353 : Remove unused columns from data pipeline input schema](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231353)
- [231354 : Remove unused columns from data pipeline output schema](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231354)
- [235679 : LA home page unavailable (County Durham - 840)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235679)
- [235684 : LA benchmarking pages not shown (Lancashire - 888)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235684)
- [233972 : Spending prioritising colour context issue on stat suffix](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/233972)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category    | Total Tests | Passed | Failed | Pass Rate |
|------------------|:-----------:|:------:|:------:|:---------:|
| Functional Tests |     16      |   16   |   0    |   100%    |
| Smoke Tests      |     20      |   20   |   0    |   100%    |
| Total            |     36      |   36   |   0    |   100%    |

**Known issues:**

1. [237597 : Unable to view Spending Priorities for schools missing building comparators](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/237597)
   - Status: Triage - discovered post-release via monitoring alert
   - Severity: TBC 


<!-- Leave the rest of this page blank -->
\newpage
