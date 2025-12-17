# Release Test Plan: 2025.12.3

**Release Date:** 2025/12/19  
**Release Label:** 2025.12.3

## Introduction

This plan defines the approach for testing release `2025.12.3`, covering smoke and sanity testing activities.  
This release delivers a combination of accessibility and behaviour fixes to the accordion component used in filter across the service, along with content updates to replace the term *priority* with *focus* based on user feedback.

## Scope

**In-scope:**

- **Enhancements**
  - Removal of the word *priority* across the service and replacement with *focus* to reflect updated terminology and user feedback.

- **Bug Fixes**
  - Fix applied to ensure the accordion within the filter component behaves correctly when JavaScript is turned off, aligning with standard GDS behaviour.  
    This update applies to all pages where the filter component is used.

**Out-of-Scope:**

- No additional feature development or data updates.

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

- **Risk:** Content updates replacing priority with focus may lead to unclear wording across the service.
  - **Mitigation:** Conducted a focused content review in lower environment to ensure clarity.

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

This release includes a fix to ensure GDS‑compliant behaviour for accordion component in filter and updates terminology across the service to replace *priority* with *focus*.

**Azure DevOps tickets included in this release:**

- [293378 - Accordion within filter component not following standard GDS behaviour](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/293378)
- [294914 - RAG Priority Wording](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/294914)

## Appendix

### Test Summary Report

**Summary of results:**  
(To be completed post‑testing)

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      -      |   -    |   -    |     -     |  
| Sanity Tests - Pre Prod |      -      |   -    |   -    |     -     |  
| Total                   |      -      |   -    |   -    |     -     |  

<!-- Leave the rest of this page blank -->
\newpage
