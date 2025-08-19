# Release Test Plan: 2025.08.1

**Release Date:** TBC  
**Release Label:** 2025.08.1

## Introduction

This plan defines the approach for testing release `2025.08.1`, covering smoke and sanity testing activities required for the data pipeline.  
This release delivers enhancements to the data pipeline to support the CFR 2025 data drop and modularisation for maintainability.

## Scope

**In-scope:**

- Enhancements

  - Data pipeline enhancements to ingest IT spending breakdown costs to support CFR 2025 data drop

**Out-of-Scope:**

- Modularisation of the data pipeline for maintainability
- Benchmarking IT spending charts (behind feature flag, not enabled in this release)

## Test Strategy

- Sanity Testing: Validate that the application and data pipeline deploy successfully and are operational with 2025 CFR data load.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and deployed to the pre-production environment.

**Exit Criteria:**

- All smoke checks complete successfully.
- No critical defects remain open.
- Sign-off obtained from stakeholders.

## Roles and Responsibilities

- **QA lead:** Coordinate smoke/sanity validation and oversee sign-off.
- **Engineer(s):** Execute validation steps, investigate and retest defects.
- **Stakeholders:** Review pipeline outputs and provide acceptance sign-off.
- **Technical lead:** Oversee pipeline modularisation and build fixes.
- **Project lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** Ingestion of new IT spending breakdown could cause data inconsistencies with CFR 2025 data.
  - **Mitigation:** Validate data integrity and data pipeline run in previous environments
- **Risk:** Modularisation may break existing dependencies.
  - **Mitigation:** Execute regression-style sanity pipeline run in previous environments.

## Test Deliverables

- Test plan document
- Smoke test cases
- Test summary report with results and sign-off status

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Release Overview:**

This is a backend-focused release. No UI testing is required beyond verifying pipeline runs.  
Only sanity checking of data pipeline runs is required in pre-production.  
Benchmarking IT spending charts will remain behind a feature flag (off by default).

**Azure DevOps tickets included in this release:**

- [266033 – CFR changes to IT spend breakdown](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/266033)
- [266552 – Modularise data pipeline](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/266552)
- [270078 – Benchmarking IT spending charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/270078)
- [273597 – Tweak data pipeline stats collector to display file date](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/273597)
- [275406 – Data pipeline docker build failure](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/275406)

## Appendix

### Test Summary Report

**Summary of results (to be completed post-testing):**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      TBC    |   TBC  |   TBC  |    TBC    |  
| Sanity Tests - Pre Prod |      TBC    |   TBC  |   TBC  |    TBC    |  
| Total                   |      TBC    |   TBC  |   TBC  |    TBC    |  

<!-- Leave the rest of this page blank -->
\newpage
