# Release Test Plan
Release date: 10/07/2024

## Introduction
Objective: The purpose of this test plan is to outline the approach and scope for testing the new features and updates being implemented today. It will detail the various types of testing that will be conducted to ensure the quality and functionality of the changes, including functional, integration, regression, and user acceptance testing. This plan aims to ensure all aspects of the updates are thoroughly tested and meet the required standards before deployment.

### Scope 
The scope of this testing plan includes thoroughly testing the new features and updates to ensure they function as intended. Additionally, regression testing will be performed on the existing system to verify that the new changes have not introduced any new issues. However, load testing, performance testing, and data testing are outside the scope of today's testing activities and will not be conducted.


## Test Strategy
### Types of Testing
#### Automated Testing:
- End-to-End (E2E) Testing
- Integration Testing
- Unit Testing
#### Manual Testing:
- Functional Testing
- Regression Testing for non-automated services
- Sanity Testing
## Levels of Testing
### Unit Testing:
Focuses on individual components or modules to ensure they work as expected in isolation.
### Integration Testing:
Ensures that different modules or services work together as intended.
### End-to-End (E2E) Testing:
Validates the complete workflow from start to finish to ensure the system meets business requirements.
## Approach
### Automated Testing:
All automated tests, including E2E, integration, and unit tests, will be executed as part of the continuous integration/continuous deployment (CI/CD) pipeline. These tests will provide quick feedback on the stability and functionality of the new features and updates.
### Manual Testing:
Manual functional testing will be carried out to validate the new features and updates. Additionally, regression testing will be performed on parts of the service that are not covered by automated tests to ensure no existing functionality is broken. Sanity testing will also be conducted to verify that the key functionalities are working as expected after the updates.

## Test Scope
### Features/Issues to be Tested:
[Ticket 217481](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217481) - this is part of the analytics work. further work will follow this. 


### Features/Issues Not to be Tested:
n/a

## Test Deliverables
### Documents:
Summary of testing performed and outcomes. 

### Reports:
release test plan

## Entry and Exit Criteria
### Entry Criteria:
- dev work is complete
- all unit, integration, e2e tests passing

### Exit Criteria: 
- testing is complete
- logged issues are fixed and retested
- features are retested in pre prod 
- sanity check in preprod are completed


## Risk Management
### Risk Identification:
n/a

### Risk Mitigation: 
n/a

## Review and Approval
### Review Process: 
The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will proceed to pre-production for final sanity checks.
### Sign-off:
Bethan Waterhouse
