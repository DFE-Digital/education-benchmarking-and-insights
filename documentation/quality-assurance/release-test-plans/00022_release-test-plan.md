# Release Test Plan: 2025.07.1

**Release Date:** 30/07/2025  
**Release Label:** 2025.07.1

## Introduction

This plan defines the approach for testing release `2025.07.1`, covering all functional and smoke testing necessary.  
This release includes backend infrastructure improvements, security-related updates, enhancements to data ingestion and pipeline performance, and content changes across the site.

Key changes include upgrading the data pipeline to Python 3.13, SSR fixes on the Spending Priorities page, and displaying updated high executive pay content.

## Scope

**In-scope:**

- Enhancements

  - High exec pay content updates
  - Partial SSR image rendering on Spending Priorities page now fixed
  - Navigation icon clean-up

**Out-of-Scope:**

- Page banner dynamic messages functionality
- Future changes around full Databricks pipeline migration
- Initial work to integrate with Databricks
- Python 3.13 upgrade for the Data Pipeline
- WAF policy provisioning and rule cleanup
- Dependency updates

## Test Strategy

- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.
- Sanity Testing: Check the changes have been deployed to pre-production are as expected.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and deployed to the pre-production environment.

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

- **Risk:** Compatibility issues from Python 3.13 upgrade
  - **Mitigation:** validate pipeline run in earlier environments

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

**Release Overview:**

The release was completed successfully with no issues. The Policy team has also been informed.

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=271580&suiteId=271581)**

**Azure DevOps tickets:**

- [263444 – Remove redundant SFB decommissioning changes from WAF](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/263444)
- [264114 – Page banner messages](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/264114)
- [264479 – Replace DisableOrganisationClaimCheck feature flag with environment variable](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/264479)
- [264559 – De-parallelise data pipeline](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/264559)
- [264698 – Upgrade Data Pipeline to Python 3.13](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/264698)
- [264999 – Review & merge July '25 dependency updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/264999)
- [265181 – High exec pay - content updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/265181)
- [266317 – Provision new WAF policy](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/266317)
- [266558 – Add Stats to Message JSON in CompletedPipelineRun Table](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/266558)
- [267134 – Partial Download image on Spending Priorities page - SSR](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/267134)
- [269583 – Review and merge Dependabot PR addressing aiohttp request smuggling vulnerability](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/269583)
- [270017 – Remove icons from the navigation links](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/270017)
- [270026 – Review & merge mid-July 2025 dependency updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/270026)
- [270637 – Change 'pay bands' to 'emoluments' in the informative message shown to the users](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/270637)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |
|-------------------------|:-----------:|:------:|:------:|:---------:|
| Smoke Tests - Prod      |      1      |   1    |   0    |   100%    |
| Sanity Tests - Pre Prod |      3      |   3    |   0    |   100%    |
| Total                   |      4      |   4    |   0    |   100%    |

<!-- Leave the rest of this page blank -->
\newpage
