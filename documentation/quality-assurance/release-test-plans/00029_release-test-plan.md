# Release Test Plan: <!-- TODO -->

**Release Date:** <!-- TODO -->
**Release Label:** <!-- TODO -->

## Introduction

This plan defines the approach for testing release <!-- TODO -->``, covering smoke and sanity testing activities.  
This release delivers the benchmark senior leadership feature behind a feature flag.

## Scope

**In-scope:**

- **New Features**
  - Added Benchmark senior leadership group page allowing benchmarking against the composition of the senior leadership at a more granular level.

**Out-of-Scope:**

- Resolve dotnet format issues in CI pipeline with dotnet SDK versions >= 9
- Front Door WAF Policy: SQLI false positive on form post  

## Test Strategy

- **Sanity Testing:** Validate that the application is deployed successfully to pre-production and operates as expected with the updated content and behaviour fixes.
- **Smoke Testing:** Execute smoke tests in production to confirm platform stability and availability post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.

**Exit Criteria:**

- All smoke and sanity checks pass successfully.
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke and sanity testing, and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Provide acceptance sign-off where required.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:**
  - **Mitigation:**

## Test Deliverables

- Test plan document
- Test cases (smoke, sanity testing)
- Test execution results and defect logs
- Test summary report with final release recommendation

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

<!-- TODO summary of outcome -->

**Azure DevOps tickets included in this release:**

- [293489 - Create "Benchmark senior leadership group" view](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/293489)
- [279977 - Resolve dotnet format issues in CI pipeline with dotnet SDK versions >= 9](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/279977)
- [295837 - Front Door WAF Policy: SQLI false positive on form post](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/295837)

## Appendix

### Test Summary Report

**Summary of results:**  
<!-- TODO -- outcome and test results below -->

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      -      |   -    |   -    |   -       |  
| Sanity Tests - Pre Prod |      -      |   -    |   -    |   -       |  
| Total                   |      -      |   -    |   -    |   -       |  

<!-- Leave the rest of this page blank -->
\newpage
