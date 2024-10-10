# Release Test Plan - 09/10/2024

## Introduction
**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release.
It will detail the various types of testing that will be conducted as part of release process. This plan aims to ensure
all aspects of the updates are thoroughly tested and meet the required standards before deployment.

**Scope:**

This test plan covers the testing in pre production and production to validate all features and bug fixes included in the release.

**Release date:**

TBC

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
1. [229900 Deprecate academies-master-list file](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229900) - We are not using academy masterlist raw file anymore as its proven to be inconsistent. Testing was conducted to ensure the fallback fields are correctly added to database. We will check sanity checks in pre production again for the update.
2. [226428 Part year data: Academies and Trusts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/226428) - The update is to manage the part year academies and flagging them. It was fully tested in feature environment. There were few queries which are now added a separate task. We will check the flag is correctly showing up in pre production for part year academies. 
3. [229448 Historic chart missing for Community focused schools costs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229448) - Historic chart is now added for community focused school and have been tested in test environment. we will check these again in pre production. 
4. [229079 Financial number formatting in charts should be zero or two decimal places, not one](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229079) - Numbers have been updated to show two decimal places instead of one. Tested in test and will be checked again in pre production. 
5. [230149 Exclamation icon for part-year schools in chart should be inlined](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/230149) - The UI has been updated accordingly and was tested in Test. Will be checked again in pre production. 
6. [230538 Spending priority messages](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/230538) - Spending priority message has been updated according to business rules. We have tested it in test and will verify again in pre production. 
7. [230800 Part Year school highlighting for 'other' school(s) in comparator set](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/230800) - Updated the color accordingly and tested in test. We will verify the changes again in pre production. 
8. [231997 Removal of median values](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/231997) - Schools with missing Pupil numbers and building size were updated with the median value to compute the apportionment correctly if the values are missing for a school but it was impacting the comparator sets for the schools and hence the change has bene revered. 
9. [232000 Update month rules for period covered by return](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/232000) - Period covered by return rule is updated and have been checked in our testing environment. We will verify the changes again in pre production.  
10. [231998 Comparator sets not to include other part year schools](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/231998)- This is to update comparator set generation logic to not to include the part year schools. We have fully tested it in our testing environment and will validate again in pre production.
11. [229446 Incorrect academy in-year balance](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/229446) - This has also been fixed and currently under test. we will validate the fix again in pre production.
12. [216606 Post MVP - "Find ways to spend less" Feature ](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/216606) - Updated the spending and costs page according to the requirements. will validate the updates again in pre production.

**Updates Not to be Tested:**

1. [230650 Monthly dependabot updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/230656) - These are dependabot updates and have already been tested in previous environment.
2. [227199 update azurerm terraform provider to latest version](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/227199) - This is terraform update and was tested in test environment. No further action is needed. 
3. [229046 Pipeline versioning](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229406) - updated release name to CalVer version. This was tested and no further action is needed. 
4. [230041 Spelling error - FBIT Historic Data page - Balance revenue reserve](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/230041) - The typo has been been updated and no further testing is required. 
5. [229318 Consolidate search dependencies](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229318) - updated search dependencies and have tested in test. No further testing is required for this. 
6. [229325 Improve local authority & trust API responses](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/229325) - The API response have been improved and tested in test. No further testing is required.
7. [231999 Clean up "1 day" rules](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/231999) - for part year academies 1 day return rule is updated after the clarification. This has been tested in our testing environment and no further checks are required.
8. [230754 Replace secrets in Web app service environment variables with Key Vault references](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230754) - This is tech debt item and have been tested. no further action is needed.
9. [229461 Cache response for finance years response](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/229461) - This is a tech debt item to cache the API response. This has been validated and no further action is needed.
10. [228244 Remove `HasIncompleteData` from models in Web and front-end-components](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/228244) - Also an tech debt item and have been validated. No further action is needed.
11. [229443 Move Dashboards, Query Pack and Functions from Core to Support](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/229443) - Another tech debt item and is validated. no further action is needed. 

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

WIP

**Risk Mitigation:**
WIP

## Review and Approval
**Review Process:**

The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will
proceed to pre-production for final sanity checks.

**Sign-off:**

Product Owner

\newpage