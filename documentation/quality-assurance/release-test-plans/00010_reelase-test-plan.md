# Release Test Plan - 2024.10.6

## Introduction
**Objective:**

The purpose of this test plan is to outline the approach, scope, and activities performed during the release process. It will detail the various types of testing conducted and document the key activities carried out to ensure the release is executed smoothly. This plan aims to ensure all aspects of the fixes are thoroughly tested, meet the required standards, and are validated before deployment.

**Scope:**

This test plan covers the testing in pre production and production to validate all updates included in the release.

**Release date:**

28/10/2024

## Test Strategy
**Types of Testing:**

- Manual Functional Testing
- Exploratory Testing
- Smoke Testing

**Approach:**

- **Manual Functional Testing** will be conducted to verify that the code and pipeline changes have implemented the intended functionality, and that all updates work as expected.
- **Exploratory Testing** will be carried out in the pre-production environment to detect any unexpected behavior or edge cases in both the code changes and data updates. This testing is unscripted and aims to identify issues that may not be covered by predefined test cases.
- **Smoke Testing** will be carried out in production to ensure its all stable. 
## Test Scope
**Issues/Updates to be Tested:**

- [234656 Missing bullet points (cost category list)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234656) - The content has been updated on one of the page and tested in test environment. we will check it again in pre production.
- [234628 RAG ratings when no building information](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234628) - RAG was getting computed for some categories for schools which have missing CDC data. The logic has now been updated and tested in test. we will check again in pre production. 
- [234612 Lead school - banner messaging](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234612) - The Lead school in federation is only showing the pupil count of the current school and not all schools in the federation. We have added a banner message to notify this and have tested in test. we will check it again in pre production.  

**Updates Not to be Tested:**

N/A
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

- All tests have been completed, with any issues found is communicated and priority agreed with Product owner. 

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