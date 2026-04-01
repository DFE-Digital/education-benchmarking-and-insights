# Release Test Plan: 2026.04.0

**Release Date:** 01/04/2026  
**Release Label:** 2026.04.0

## Introduction

This plan defines the approach for testing release `2026.04.0`, covering smoke and sanity testing activities.  
This release delivers a single enhancement to improve how DSG carried forward balances are represented on the LA homepage, providing a clearer and more accurate picture of the underlying deficit position.

## Scope

**In-scope:**

- **Enhancements**
  - Update to the LA homepage to improve the presentation of High Needs DSG carried forward balances, ensuring the displayed position better reflects the actual deficit.  

**Out-of-Scope:**

- No additional feature development or data updates.
- No UAT activities required for this release.

## Test Strategy

- **Sanity Testing:** Validate that the application deploys successfully to pre-production and that the updated DSG carried forward balance presentation behaves as expected.
- **Smoke Testing:** Execute smoke tests in production to confirm platform stability and availability post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.

**Exit Criteria:**

- All smoke and sanity checks pass successfully.
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.

## Roles and Responsibilities

- **QA Lead:** Coordinate sanity and smoke testing, and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Provide acceptance sign-off where required.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

There are no known risks associated with this release.

## Test Deliverables

- Test plan document
- Test cases (sanity and smoke testing)
- Test execution results and defect logs
- Test summary report with final release recommendation

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

{to be added here }

**Azure DevOps tickets included in this release:**

- [307652 - HN DSG carried forward position changes](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/307652)

## Appendix

### Test Summary Report

**Summary of results:**  
Release completed successfully with no issues.

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      1      |   1    |   0    |   100%    |  
| Sanity Tests - Pre Prod |      1      |   1    |   0    |   100%    |  
| Total                   |      2      |   2    |   0    |   100%    |  

<!-- Leave the rest of this page blank -->
\newpage
