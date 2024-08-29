# Release Test Plan - 29/08/2024

## Introduction
**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release. It will detail the various types of testing that will be conducted as part of release process.  This plan aims to ensure all aspects of the updates are thoroughly tested and meet the required standards before deployment.

**Scope:**

This test plan covers the functional and sanity testing to validate all features and bug fixes included in the release.

**Release date:** 

30/08/2024

## Test Strategy
**Types of Testing:**

- Manual Functional Testing
- Sanity Testing

**Approach:**

- Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod.
- Sanity testing will be carried out in pre-prod to ensure all functionalities are working as expected.
## Test Scope
**Issues/Updates to be Tested:**


**Updates Not to be Tested:**

## Test Deliverables
**Documents:**

- Release test plan

**Reports:**

- Summary of testing performed and outcomes

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

Pre-production and production has existing data, which will require updating/modifying as the fixes included in this release requires pipeline rerun. 

**Risk Mitigation:**

- Ran in isolated clean environment prior to release to confirm fixes
- Ran in test environment which has identical data to pre-prod and production to confirm fixes

## Review and Approval
**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will proceed to pre-production for final sanity checks.

**Sign-off:**

Product Owner

\newpage
