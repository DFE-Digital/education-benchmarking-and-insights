# Release Test Plan - 2024.10.5

## Introduction
**Objective:**

The purpose of this test plan is to outline the approach, scope, and activities performed during the release process. It will detail the various types of testing conducted and document the key activities carried out to ensure the release is executed smoothly. This plan aims to ensure all aspects of the updates are thoroughly tested, meet the required standards, and are validated before deployment.

**Scope:**

This test plan covers the testing in pre production and production to validate all updates included in the release.

**Release date:**

24/10/2024

## Test Strategy
**Types of Testing:**

- Manual Functional Testing
- Exploratory Testing
- Sanity Testing

**Approach:**

- Manual functional testing will be carried out to validate updates are in place in pre-prod.
- Exploratory testing will be carried out in pre-prod to ensure all functionalities are working as expected.
- Sanity Testing will be carried out in production to ensure its all stable. 
## Test Scope
**Issues/Updates to be Tested:**
TBA

**Updates Not to be Tested:**

TBA  

## Test Deliverables
**Documents:**

- Release test plan

**Reports:**

TBA

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

Pre-production and production has existing data, which will require updating/modifying as the updates included in this release requires pipeline rerun.

**Risk Mitigation:**
- Ran in test environment which has identical data to pre-prod and production to confirm fixes.
## Review and Approval
**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will
proceed to pre-production for checks. Upon validating in pre production the changes will be deployed to production for final sanity checks. 

**Sign-off:**

Product Owner

\newpage