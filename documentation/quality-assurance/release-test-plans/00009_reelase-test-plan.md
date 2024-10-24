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
- Smoke Testing

**Approach:**

- **Manual Functional Testing** will be conducted to verify that the code and pipeline changes have implemented the intended functionality, and that all updates work as expected.
- **Exploratory Testing** will be carried out in the pre-production environment to detect any unexpected behavior or edge cases in both the code changes and data updates. This testing is unscripted and aims to identify issues that may not be covered by predefined test cases.
- **Smoke Testing** will be carried out in production to ensure its all stable. 
## Test Scope
**Issues/Updates to be Tested:**

[233514 Historic Income Chart - Wrong Data](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/233514) - It was noticed the income fields values were interchanged which have now been fixed. we have validated it in test and will check again in pre production.

**Updates Not to be Tested:**

[234442 Trusts unable to access features](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234442) - Trusts were unable to access CFP and BFR features due to leading zero in company number being lost. We have now fixed it and tested it in test environment. We don't have access to organisation in pre production but will request someone to check it for us who has access.   

## Test Deliverables
**Documents:**

- Release test plan

**Reports:**

Smoke testing completed in production and everything is looking good. 

## Entry and Exit Criteria
**Entry Criteria:**

- All Fixes and updates have successfully passed lower quality gates
- All changes have been deployed in pre-production environment

**Exit Criteria:**

- All tests have been completed, with any issues found is communicated and priority agreed with Product owner. 

## Risk Management
**Risk Identification:**

Pre-production and production has existing data, which will require updating/modifying as the updates included in this release requires pipeline rerun.
Also for one of the fix included in the release for which we don't have access in pre production and production. 

**Risk Mitigation:**
- Ran in test environment which has identical data to pre-prod and production to confirm fixes.
- Tested the DSI fix in test and will request someone to check the pre production who has the correct access to an organisation. 
## Review and Approval
**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will
proceed to pre-production for checks. Upon validating in pre production the changes will be deployed to production for final sanity checks. 

**Sign-off:**

Product Owner

\newpage