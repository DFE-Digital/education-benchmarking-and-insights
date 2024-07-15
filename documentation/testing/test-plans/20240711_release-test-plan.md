# Release Test Plan
Release date: 11/07/2024

## Introduction
Objective: The purpose of this test plan is to outline the approach and scope for testing the bug fixes made as part of this release. It will detail the various types of testing that will be conducted as part of release process.  This plan aims to ensure all aspects of the updates are thoroughly tested and meet the required standards before deployment.

### Scope 
This test plan covers the functional and sanity testing to validate all features and bug fixes included in the release.
## Test Strategy
### Types of Testing
- Manual Functional Testing
- Sanity Testing
## Levels of Testing
## Approach
### Manual functional Testing: 
Manual functional testing will be carried out to validate fixes are in place in pre-prod. 
### Sanity Testing
This Testing will be carried out in pre-prod to ensure all functionalities are working as expected. 

## Test Scope
### Issues to be Tested:
- [217481- App insights Analytics ](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217481) - this is part of the analytics work. further work will follow this.

The above dashboard is manually checked while interacting with the service and validated that the interaction is recorded in the MI and OI dashboard. Sanity testing is also performed on the system making sure all major parts of the service are working as expected. 

- [216189- Update Cosmos connection details for Web App to use Managed Identity](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/216189)

This update has impacted the login side of the service so all areas which requires login custom data, comparators, school details, CFP journey has been tested as part of this. Also sanity checks are performed on the whole service to ensure the service is working as expected.

- [216190 - Update SQL DB connection details for APIs to use Managed Identity](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/216190)

This has impacted the connection details for APIs so checked school/trusts/LAs areas where data is pulled which includes comparison page, historic data ,benchmarking page, spending and costs and other pages and validated that the data is still displayed on the pages which ensured the connection is working as expected. 

- [218281 -Incorrect responses from authorization attributes](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/218281)

This impacted the response code when the user is trying to access schools which user don't have permission. verified the response code sent back as 401 and now been updated to 403 (server understand the request but refuse to authorise it)

- [217961 - Graceful error handling on proxy API end points](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217961)

if the school don't have current year data then benchmarking pages are throwing 500 error which impacts the analytics. The error has been handled gracefully now. 

- [218339 -Trusts are not showing in search bar](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/218339)

This is related to Raw data that was used for trust details. Updated it to read Trust details from GIAS files which has resolved this issue. We have validated this by checking existing trust names with the updated ones and the ones that are not matching were compared with GIAS files. This ensures now we have correct trusts as expected. 

- [218046 - Learn@ MAT appears to be using incorrect Group UID](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/218046)

This is related to the raw data that was used for trust details which was causing duplicate UIDs. We have ensured now that there are no duplicate UIDs for the trusts which are linked with schools. 
### Issues Not to be Tested:
n/a

## Test Deliverables
### Documents:
Summary of testing performed and outcomes. 

### Reports:
Release test plan

## Entry and Exit Criteria
### Entry Criteria:
- All Fixes have successfully passed lower quality gates 
- All changes have been deployed in pre-production environment

### Exit Criteria:
- all identified and logged issues during the testing phases are fixed and retested
- sanity check in preprod are completed
- all planned tests have been completed and with no pending issues


## Risk Management
### Risk Identification:
n/a

### Risk Mitigation: 
n/a

## Review and Approval
### Review Process: 
The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will proceed to pre-production for final sanity checks.
### Sign-off:
Product Owner
