﻿# Release Test Plan - 17/07/2024 

## Introduction
**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release. It will detail the various types of testing that will be conducted as part of release process.  This plan aims to ensure all aspects of the updates are thoroughly tested and meet the required standards before deployment.

**Scope:** 
This test plan covers the functional and sanity testing to validate all features and bug fixes included in the release.

**Release date:**

17/07/2024

## Test Strategy
**Types of Testing:**

- Manual Functional Testing
- Sanity Testing

**Approach:**

- Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod. 
- Sanity testing will be carried out in pre-prod to ensure all functionalities are working as expected. 

## Test Scope
**Issues/Updates to be Tested:**

- [218460- Missing DSI cookie declaration ](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/218460) - This is a content update which we have validated in our test environment and verify in preprod once changes are in there.
- [218415- Non-essential cookie management](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/218415) - This update has added a cookie banner for non essential cookies and manage cookies accordingly depending if the user has accepted/rejected the cookies.
- [219080 - Block non UK trafic](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/219080) - We have applied a geo location restriction to block traffic from locations that are not identified as generating from the UK.
- [219333 - Add anonymous user requests to MI Dashboard](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/219333) - Now that we have added non-essential Cookies, MI dashboard has been updated to track count of anonymous requests.

**Issues Not to be Tested:**

n/a

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

Potential valid requests could be blocked with WAF changes.

**Risk Mitigation:**

We plan to continue to monitor the firewall logs to ensure potentially valid requests are not blocked as a result of WAF changes.

## Review and Approval
**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will proceed to pre-production for final sanity checks.

**Sign-off:**

Product Owner

\newpage
