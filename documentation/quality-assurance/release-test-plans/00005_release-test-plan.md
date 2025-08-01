﻿# Release Test Plan: 19/09/2024

## Introduction

**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release.
It will detail the various types of testing that will be conducted as part of release process. This plan aims to ensure
all aspects of the updates are thoroughly tested and meet the required standards before deployment.

**Scope:**

This test plan covers the testing in pre production and production to validate all features and bug fixes included in the release.

**Release date:**

19/09/2024

## Test Strategy

**Types of Testing:**

- Manual Functional Testing
- Exploratory Testing

**Approach:**

- Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod.
- Exploratory testing will be carried out in pre-prod to ensure all functionalities are working as expected.

## Test Scope

**Issues/Updates to be Tested:**

1. [224500 Part year Data: Maintained Schools](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/224500) -We have successfully managed the part-year maintained schools, and the logic for creating comparator sets and RAG ratings has been updated. A banner now appears for part-year maintained schools. This functionality has been thoroughly tested in both the development environment (D01) and the test environment (T01). We will validate that the banner for part-year schools is appearing as expected in the pre-production and production environments.
2. [223941 Review/update overall phase mappings](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/223941) - The overall phase mapping logic has been updated. We have tested the changes local, dev, and test. everything is in place as expected and will verify the numbers again in pre production.
3. [223831 Add DLQ monitor to dashboard](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/223831) - Added a monitor for dead letter queue so that we have a visibility of any pipeline failures. This has been validated in test and the visibility of the dashboard will be checked again in pre production.
4. [222562 Threat Detection Policy settings reverting](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/222562) - Thread deduction policy is now updated and validated against test environment. We will check the auditing storage account again in pre production to ensure changes are populated as expected.
5. [215810 Misconfigured HTTP Security Header](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/215819) - To comply with ITHC report HTTP security header has been updated accordingly. This has been tested in our testing environment and will be checked again in preproduction.
6. [227113 Utilities cost not showing all schools](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/227113) - This is linked with the missing RAG rating category of few schools. Testing completed in our test environment and all rag categories are generated for all schools. We will validate the same in preproduction.  
7. [228233 Missing Educational ICT Costs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/228233) This has also been fixed as part of 227113.

**Updates Not to be Tested:**

1. [226388 Terraform provider updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/226388) - These are terraform provider updates and doesn't require any further testing.

## Test Deliverables

**Documents:**

- Release test plan

**Reports:**

- Summary of testing performed and outcomes

we have identified a regression issue during testing in production. Specifically, schools with an overall phase of UTC are missing from the Trust homepage. This affects 49 out of 2571 Trusts. After consulting with PO, we’ve decided to address this issue in the upcoming release, and it has been added to the current sprint board [here](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229260).

## Entry and Exit Criteria

**Entry Criteria:**

- All Fixes and updates have successfully passed lower quality gates
- All changes have been deployed in pre-production environment

**Exit Criteria:**

- All tests have been completed, with any issues found logged in the product backlog
- Issue priority agreed with Product owner;
  - Any new critical issues found during release test are resolved prior to release and retested
  - Any new major/minor issues found during release test will be scheduled for next release

## Risk Management

**Risk Identification:**

Pre-production and production has existing data, which will require updating/modifying as the updates/fixes included in this
release requires pipeline rerun. Additionally, there are legacy Metric RAG records from 2021 and 2022, which, due to updated logic, no longer apply but still exist in the database.

**Risk Mitigation:**

- Ran in isolated clean environment prior to release to confirm the fixes
- Ran in test environment which has identical data to pre-prod and production to confirm fixes
- Since Metric RAG is only displayed for the latest year (2023), the legacy records from 2021 and 2022 will not impact the end user or be visible. These records will be removed as part of a future task added in backlog, which will replace the outdated database records. Until then, they remain in the system.

## Review and Approval

**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will
proceed to pre-production for final sanity checks.

**Sign-off:**

Product Owner

\newpage
