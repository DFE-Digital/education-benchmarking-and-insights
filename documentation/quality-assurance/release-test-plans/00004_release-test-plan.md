# Release Test Plan: 02/09/2024

## Introduction

**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release.
It will detail the various types of testing that will be conducted as part of release process. This plan aims to ensure
all aspects of the updates are thoroughly tested and meet the required standards before deployment.

**Scope:**

This test plan covers the functional and sanity testing to validate all features and bug fixes included in the release.

**Release date:**

02/09/2024

## Test Strategy

**Types of Testing:**

- Manual Functional Testing
- Sanity Testing

**Approach:**

- Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod.
- Sanity testing will be carried out in pre-prod to ensure all functionalities are working as expected.

## Test Scope

**Issues/Updates to be Tested:**

1. [Custom event tracking for "Save as image" button](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/223210) -
   we are now tracking save an image actions. this is validated in test environment and we will verify it again in
   preprod.
2. [Access Denied message - content review](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/218513) -
   this is copy update which has been tested in test environment and will be validated again in pre production.
3. [Post MVP feature 9 - Display Gross and Net Catering Income](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/214976) -
   We are now showing gross and net catering income on comparison page. Validated in test environment and will be
   checked again in pre production.
4. [Misleading autocomplete results](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/223871) -
   This was identified in accessibility audit to show which part of the input criteria is matched. Tested in test
   environment and will be verified again in pre production.
5. [A02: Hover over information for horizontal bar charts needs to be available for keyboard](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/225478) -
   Also identified in accessibility audit and now the hover over information is available to keyboard users. Validated
   in test environment and will be checked again pre production.
6. [Update mailbox details on service pages](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/225527) -
   This is a copy update for email address shown on service. Validated in test and will be checked again in pre
   production.
7. [Package Management](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/226361) - This is
   an update on the packages raised by the Dependant Bot. A full regression will be conducted in the test environment,
   and sanity checks will be performed in pre-production to cover this.

**Updates Not to be Tested:**

1. [Ensure Platform.Database app uses managed identities to access SQL db](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/222561) -
   this update is linked with how secrets are read in pipeline. The pipelines are green so no further testing is
   required.
2. [BfR Figures Incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/217947) - The issue with BFR figures has been corrected and tested in test. This is behind login so no further testing will be done in pre production.

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

Pre-production and production has existing data, which will require updating/modifying as the fixes included in this
release requires pipeline rerun.

**Risk Mitigation:**

- Ran in isolated clean environment prior to release to confirm fixes
- Ran in test environment which has identical data to pre-prod and production to confirm fixes

## Review and Approval

**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will
proceed to pre-production for final sanity checks.

**Sign-off:**

Product Owner

<!-- Leave the rest of this page blank -->
\newpage
