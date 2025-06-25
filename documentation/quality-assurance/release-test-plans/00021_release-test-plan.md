# Release Test Plan - 2025.06.X

**Release Date:** XX/06/2025
**Release Label:** 2025.06.X

## Introduction

This plan defines the approach for testing release `2025.06.X`, covering all functional and smoke testing necessary.
It includes a new feature to begin the move to server-side rendered charts, data-driven commercial resources, as well as various content updates and bug fixes.
It also includes some manual tasks related to SFB decommissioning due to restrictions in Terraform when managing Front Door configuration.

## Scope

**In-scope:**

- New features
  - Server side rendered charts is implemented on school spending priorities page
- Enhancements
  - Commercial resources are now data driven
  - Content updates in multiple areas
  - API refactor to prepare for CMS-lite functionality
  - New ILR data file for Post-16 and sixth form schools census data ingested
  - High executives pay data is now displayed in trust benchmarking
- Bug fixes
  - Logging of requests to temporary SFB redirect app service
  - Fix to y-axis for single value line charts

**Out-of-Scope:**

- Tech Debt Items
- Any new functionality not targeted for this release
- Dependency updates

## Test Strategy

- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.
- Sanity Testing: Check the High executes pay is displayed as expected. 
- Manual validation: Due to issue documented in work item [#263444](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/263444), a manual disassociation of the only Front Door ruleset must be validated.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and deployed to the pre-production environment.
  - Manual infrastructure changes made to Front Door in pre-production environment (see [#264856](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/264856))
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

- **Risk:** Bugs & Defects in Production. Unexpected software defects can cause system crashes, data corruption, or functional failures.
  - **Mitigation:** Conduct thorough testing (unit, integration, regression testing). Implement automated testing to catch issues early.

## Test Deliverables

- Test plan document
- Test cases (Smoke)
- Test summary report outlining test results, pass/fail rates, and any outstanding issues

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Azure DevOps tickets:**

- [261909 - Prevent 500 errors from null Search Suggest responses after TaskCanceledException](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/261909)
- [261093 - Enable filtered search flag](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/261093)
- [263412 - Requests to Redirect app service not being logged](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/263412)
- [258039 - Use server side rendered charts on School Spending Priorities page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/258039)
- [259512 - Data driven commercial resource links](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/259512)
- [260743 - High Needs Content Guidance / Glossary](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/260743)
- [260803 - SEND Content Updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/260803)
- [256319 - Sixth Form data](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/256319)
- [258231 - Reference to central service cost codes displayed for academies in MAT](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/258231)
- [233948 - Same Chart y-axis values](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/233948)
- [263341 - Dependabot updates - June 2025](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/263341)
- [264107 - Content end point consolidation](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/264107)
- [263444 - Remove redundant SFB decommissioning changes from WAF](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/263444)
- [261891 - Ingest and Display High Exec Pay data](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/261891)
- [260084 - 6th form Data Source Update](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/260084)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category          | Total Tests | Passed | Failed | Pass Rate |
|------------------------|:-----------:|:------:|:------:|:---------:|
| Smoke Tests - Pre prod |      X      |   X    |   0    |   100%    |
| Sanity Tests - Prod    |      X      |   X    |   0    |   100%    |
| Total                  |      X      |   X    |   0    |   100%    |

<!-- Leave the rest of this page blank -->
\newpage
