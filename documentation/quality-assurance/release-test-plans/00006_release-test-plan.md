# Release Test Plan: 23/09/2024

## Introduction

**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release.
It will detail the various types of testing that will be conducted as part of release process. This plan aims to ensure
all aspects of the updates are thoroughly tested and meet the required standards before deployment.

**Scope:**

This test plan covers the testing in pre production and production to validate all features and bug fixes included in the release.

**Release date:**

23/09/2024

## Test Strategy

**Types of Testing:**

- Manual Functional Testing
- Exploratory Testing

**Approach:**

- Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod.
- Exploratory testing will be carried out in pre-prod to ensure all functionalities are working as expected.

## Test Scope

**Issues/Updates to be Tested:**

1. [212855 Accessibility statement page content](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/212855) - added a new page for accessibility statement. Tested in test and will be checked again in pre production.
2. [228013 Show actual data for Part Year schools](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/228013) - updated part years schools to show information about not having full year submission. We have validated this in test and will be checked again in pre production.  
3. [228227 Net Catering cost is incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/228227) - The issue with net costs is now fixed. This has been tested in test and will be checked again in pre production.
4. [218588 Trust-trust missing confirmation banner](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/218588) - Confirmation banner is now added on trust to trust comparison journey. This has been tested in test and will be checked again in pre production.
5. [229260 UTC Phase type not showing on trust Homepage](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229260) - This was identified as a regression of previous release and have been fixed and tested in test. Will be checked again in pre production.
6. [228611 Find Organisation auto complete sometimes malforms match highlighting](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/228611) - A fix has been made on the search which has been tested in test and will be checked again in pre production.

**Updates Not to be Tested:**

1. [225528 Upgrade Azure Key Vault](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/225528) - This is a tech debt item and no further testing is required.
2. [221013 Navigate back to School from CFP](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/221013) - Links have been added on CFP journey to go back to school homepage and has been fully tested in test environment.
3. [228386 set local_user_enabled to false](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/228386) - Tech debt item and have been tested as part of other items. no further action is needed.

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

Pre-production and production has existing data, which will require updating/modifying as the updates/fixes included in this
release requires pipeline rerun.

**Risk Mitigation:**

- Ran in isolated clean environment prior to release to confirm the fixes
- Ran in test environment which has identical data to pre-prod and production to confirm fixes

## Review and Approval

**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will
proceed to pre-production for final sanity checks.

**Sign-off:**

Product Owner

\newpage
