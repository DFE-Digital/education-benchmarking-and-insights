# Release Test Plan - 18/10/2024

## Introduction
**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release.
It will detail the various types of testing that will be conducted as part of release process. This plan aims to ensure
all aspects of the updates are thoroughly tested and meet the required standards before deployment.

**Scope:**

This test plan covers the testing in pre production and production to validate all features and bug fixes included in the release.

**Release date:**

21/10/2024

## Test Strategy
**Types of Testing:**

- Manual Functional Testing
- Exploratory Testing
- Sanity Testing

**Approach:**

- Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod.
- Exploratory testing will be carried out in pre-prod to ensure all functionalities are working as expected.
- Sanity Testing will be carried out in production to ensure its all stable. 
## Test Scope
**Issues/Updates to be Tested:**
1. [230685 Run CFR scripts for 2023-4 and validate new output](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230685) - CFR 2024 data has been added and validated in various environments. We will validate the presence of CFR 2024 data in pre production. 
2. [217196 Direct revenue financing coming undefined](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217196) - Direct revenue financing was coming undefined which has been fixed. we will validate in pre production that it is not undefined schools anymore. 
3. [232528 incorrect spending framework link](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232528) - The spending framework link were changed and updates were made on the service to reflect the same. We will validate it again in pre production. 
4. [28225 Expenditure missing for converted schools](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/228225) - Previously we were showing schools on LA/Trust homepage which were part of the organisation in previous years resulting in showing 0 for schools which have closed/moved. Updated it to only show the schools which are present in the current year. Tested in dev/test and will validate again in pre production. 
5. [225541 Revenue Reserve](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/225541) - Revenue reserve calculation has been updated as expected. we will validate the changes again in pre production. 
6. [230616 Part Year - Misleading msg on Benchmark Spending](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230616) - WIP
7. [230874 RAG Guidance Content](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230874) - WIP
8. [230876 Cost categories guidance content](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230876) - Cost categories guidance content has been updated. We will check the changes again in pre production
9. [230287 Update Accessibility page content - Browsers tested](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230287) - content updated on accessibility page. we will check the updates again in pre production
10. [232328 Other cost category should not be shown](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232328) - Other cost category is not shown anymore on recommended tab. We will verify the changes again in pre production.
11. [232973 Remove mixed datasets (comparator sets & RAG)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232973) - We have now removed the mixed comparators and RAG. This has been validated in dev/test and will be checked again in pre production. 
12. [232741 School part of federation link to federation page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232741)- The schools part of federation are now linked with the federation page. This is tested in dev/test and will be checked again in pre production. 
13. [232745 Federation homepage](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232745) - Added federation hompeage to the service. WIP

**Updates Not to be Tested:**

1. [231298 Ensure daily backup of the databases to an Azure storage account.](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231298) - Daily backup of the databases have been set up. No further action is needed. 
2. [231790 Ensure daily backup of the raw input files on an Azure storage account](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231790) - Daily backup of raw input files have been set up. No further action is needed. 
3. [231299 Review & ensure database backup policy is correct](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231299) - The policy for db backup is is reviewed and tested. No further action is needed. 
4. 


## Test Deliverables
**Documents:**

- Release test plan

**Reports:**



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
proceed to pre-production for checks. Upon validating in pre production the changes will be deployed to production for final sanity checks. 

**Sign-off:**

Product Owner

\newpage