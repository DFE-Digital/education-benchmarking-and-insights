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
## Approach
### Automated Testing:
All automated tests, including E2E, integration, and unit tests, will be executed as part of the continuous integration/continuous deployment (CI/CD) pipeline. These tests will provide quick feedback on the stability and functionality of the new features and updates.
### Manual Testing: 
Manual functional testing will be carried out to validate the new features and updates. Additionally, regression testing will be performed on parts of the service that are not covered by automated tests to ensure no existing functionality is broken. Sanity testing will also be conducted to verify that the key functionalities are working as expected after the updates.

## Test Scope
### Features/Issues to be Tested:
- [217481- App insights Analytics ](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217481) - this is part of the analytics work. further work will follow this.

The above dashboard is manually checked while interacting with the service and validated that the interaction is recorded in the MI and OI dashboard. Sanity testing is also performed on the system making sure all major parts of the service are working as expected. 

- [216189- Update Cosmos connection details for Web App to use Managed Identity](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/216189)

This update has impacted the login side of the service so all areas which requires login custom data, comparators, school details, CFP journey has been tested as part of this. Also sanity checks are performed on the whole service to ensure the service is working as expected.

- [216190 - Update SQL DB connection details for APIs to use Managed Identity](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/216190)

This has impacted the connection details for APIs so checked school/trusts/LAs areas where data is pulled which includes comparison page, historic data ,benchmarking page, spending and costs and other pages and validated that the data is still displayed on the pages which ensured the connection is working as expected. 

- [218281 -Incorrect responses from authorization attributes](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/218281)

This impacted the response code when the user is trying to access schools which user don't have permission. verified the response code sent back as 401 and now been updated to 403 (server understand the request but refuse to authorise it)

- [217961 - Graceful error handling on proxy API end points](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217961)

WIP 

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
