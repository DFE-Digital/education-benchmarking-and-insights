# Release Test Plan - 2025.01.01

**Release Date:** TBC  
**Release Label:** 2025.01.01

## Introduction

This plan outlines the approach for testing the `2025.01.01` release, ensuring enhancements and critical bug fixes are validated and do not adversely impact existing functionalities.

## Scope

### **In-Scope:**

- **Enhancements**:
    - A02: Hover over information for line charts needs to be available for keyboard
    - Analytics - View which schools have been accessed

- **Bug Fixes**:
    - Spinner when no data submitted
    - Multi-selection of LA's not working
    - Benchmarking historical trends
    - Suppressing negative and zero values
    - Federation per unit RAG values incorrect
    - Federation per unit RAG values incorrect
    - Handle custom-data/part-year RAG

- **Dependencies**:
    - January 2025 - Dependent bot updates

### **Out-of-Scope:**
- Any functionality not included in this release's work items.


## Test Strategy

- **Sanity Testing**: Verify bug fixes are resolved.
- **Smoke Testing**: Validate basic application functionality after deployment.
- **Regression Testing**: Focus on areas affected by enhancements and bug fixes.

## Entry and Exit Criteria

### **Entry Criteria:**
- All code changes are merged and deployed to the pre-production environment.
- Pipeline run completed in pre-production environment.
### **Exit Criteria:**
- All high-priority test cases have passed.
- No critical defects remain open.
- Stakeholders approve release readiness.


## Roles and Responsibilities

- **QA lead:** Coordinate testing activities, manage test cases and defect triage.
- **Engineer(s):** Execute test cases, report and retest defects.
- **Stakeholders:** Participate in user acceptance testing and provide final approval.
- **Technical lead:** Oversee release planning.
- **Project lead:** Go/no-go decisions.

## Risk Analysis

n/a


## Test Deliverables

- Test plan document.
- Test summary report, including test results and outstanding issues.


## Azure DevOps Tickets

- **[236233 - Spinner when no data submitted](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/236233)**
- **[225945 - A02: Hover over information for line charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/225945)**
- **[241720 - Multi-selection of LA's not working](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241720)**
- **[237633 - Benchmarking historical trends](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/237633)**
- **[235748 - Suppressing negative and zero values](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/235748)**
- **[241716 - Federation per unit RAG values incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241716)**
- **[241759 - Federation per unit RAG values incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241759)**
- **[241954 - Handle custom-data/part-year RAG](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241954)**
- **[242081 - Analytics - View which schools have been accessed](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242081)**
- **[243084 - Dependencies - January 2025](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/243084)**  
