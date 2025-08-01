﻿# Release Test Plan: 2024.10.6

## Introduction

**Objective:**

The purpose of this test plan is to outline the approach, scope, and activities performed during the `2024.10.6` release.

It will detail the various types of testing required/conducted to assure for the bug fixes included in this release.
As well as, document the key activities carried out to ensure the release is executed successfully.

**Scope:**

- In scope:
  - Spending priorities (RAG ratings)
  - Comparator sets
  - Benchmark spending
  - Benchmark pupil & workforce data
  - Lead federation schools
  - Cost category guidance
- Out of Scope:
  - Any new functionality not targeted for this release

**Release date:**

28/10/2024

## Test Strategy

**Types of Testing:**

- Manual Functional Testing
- Exploratory Testing
- Smoke Testing

**Approach:**

- **Manual Functional Testing;** will be conducted in the pre-production to verify the bug fixes.
- **Exploratory Testing;** will be carried out in the pre-production to detect any unexpected behaviour.
- **Smoke Testing;** will be carried out in production to ensure release is stable.

## Test Scope

**Fixes to be tested:**

- [234656 - Updated cost category guidance](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234656)
- [234628 - Prevent building comparator and invalid RAG ratings being created when no building information](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234628)  
- [234612 - Banner messaging for lead federation school](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234612)

## Test Deliverables

**Documents:**

- Release test plan
- [Exploratory test charter](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/234822)
- Manual test scripts
  - [234828 - Updated cost category guidance](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=218463&suiteId=234828)
  - [234655 - Prevent building comparator and invalid RAG ratings being created when no building information](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=218463&suiteId=234655)
  - [234825 - Banner messaging for lead federation school](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=218463&suiteId=234825)  

**Reports:**

- Test summary report

| Test case Id    | Test Suite | Status |
| -------- | ------- | ------- |
| 234829 | 234828 | PASS  |
| 234842 | 234828 | PASS  |
| 234826 | 234825 | PASS  |
| 234843 | 234825 | PASS  |
| 234657 | 234655 | PASS  |
| 234831 | 234655 | PASS  |
| 234822 | 229049 | PASS  |

## Entry and Exit Criteria

**Entry Criteria:**

- All development tasks are complete.
- All fixes passed lower quality gates.
- A stable build is deployed to pre-production.

**Exit Criteria:**

- All tests have been completed,
- Issues found are communicated.
  - Priority agreed with Product owner.
  - Critical issues resolved prior release to production.
  - All other issues logged in backlog

## Risk Management

**Risk Identification:**

- Invalid data left in pre-production & production databases.
- User error on manual removal of data.

**Risk Mitigation:**

- Steps for removal of data have been run in lower environment.
- Database backups in place if restore is needed.

## Review and Approval

**Review Process:**

1. Release notification, including release test plan, shared with the Product Owner for review and approval.
2. Deployment to pre-production and quality assurance sign-off.
3. Deployment to production and quality assurance sign-off.

**Final Sign-off:**

Product Owner

\newpage
