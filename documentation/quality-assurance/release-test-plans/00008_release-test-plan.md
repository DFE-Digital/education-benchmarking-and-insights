# Release Test Plan: 2024.10.4

## Introduction

**Objective:**

The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release. It will detail the various types of testing that will be conducted throughout the release process. In addition to defining the testing strategy, this plan also tells the story of the updates, capturing what has been changed, improved, or introduced in this release. This ensures that all aspects of the updates are thoroughly tested and meet the required standards before deployment, providing confidence in the quality and stability of the release.

**Scope:**

This test plan covers the testing in pre production and production to validate all features and bug fixes included in the release.

**Release date:**

22/10/2024

## Test Strategy

**Types of Testing:**

- Manual Functional Testing
- Data Validation Testing
- Exploratory Testing
- Sanity Testing

**Approach:**

- **Manual Functional Testing** will be conducted to verify that the code changes have implemented the intended functionality, and that all updates work as expected. This will cover both new features and bug fixes in the pre-production environment.
- **Data Validation Testing** will be performed by stakeholders to ensure the accuracy, completeness, and consistency of the CFR data released as part of this update. This will involve validating data integrity across platform, checking data, and ensuring that data meets standards.
- **Exploratory Testing** will be carried out in the pre-production environment to detect any unexpected behavior or edge cases in both the code changes and data updates. This testing is unscripted and aims to identify issues that may not be covered by predefined test cases.
- **Sanity Testing** will be conducted in the production environment post-deployment to confirm that both the code changes and data are stable and working as intended. This includes a quick health check of critical functionalities to ensure there are no major issues after the release.

## Test Scope

**Issues/Updates to be Tested:**

1. [230685 Run CFR scripts for 2023-4 and validate new output](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230685) - CFR 2024 data has been added and validated in various environments. We will validate the presence of CFR 2024 data in pre production but won't release to production until data validation checks are complete. This won't block other tickets going to production.
2. [217196 Direct revenue financing coming undefined](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217196) - Direct revenue financing was coming undefined which has been fixed. we will validate in pre production that it is not undefined schools anymore.
3. [232528 incorrect spending framework link](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232528) - The spending framework link were changed and updates were made on the service to reflect the same. We will validate it again in pre production.
4. [28225 Expenditure missing for converted schools](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/228225) - Previously we were showing schools on LA/Trust homepage which were part of the organisation in previous years resulting in showing 0 for schools which have closed/moved. Updated it to only show the schools which are present in the current year. Tested in dev/test and will validate again in pre production.
5. [225541 Revenue Reserve](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/225541) - Revenue reserve calculation has been updated as expected. we will validate the changes again in pre production.
6. [230616 Part Year - Misleading msg on Benchmark Spending](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230616) - Added information on the page which has missing comparator set. we have verified the changes in our testing environment and will check again in pre production.
7. [230874 RAG Guidance Content](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230874) - The content has been updated and checked in test enviorment. we will verify the changes again in pre production.
8. [230876 Cost categories guidance content](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230876) - Cost categories guidance content has been updated. We will check the changes again in pre production
9. [230287 Update Accessibility page content - Browsers tested](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/230287) - content updated on accessibility page. we will check the updates again in pre production
10. [232328 Other cost category should not be shown](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232328) - Other cost category is not shown anymore on recommended tab. We will verify the changes again in pre production.
11. [232973 Remove mixed datasets (comparator sets & RAG)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232973) - We have now removed the mixed comparators and RAG. This has been validated in dev/test and will be checked again in pre production.
12. [232741 School part of federation link to federation page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232741)- The schools part of federation are now linked with the federation page. This is tested in dev/test and will be checked again in pre production.
13. [232745 Federation homepage](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232745) - Added federation homepage to the service and tested it in test environment. will review the changes again in pre production.
14. [233513 Historic Data - Inconsistent Wording](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/233513) - Wording has been updated on the historic page to match with other pages. We have verified the changes in test and will check again in pre production.

**Updates Not to be Tested:**

1. [231298 Ensure daily backup of the databases to an Azure storage account.](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231298) - Daily backup of the databases have been set up. No further action is needed.
2. [231790 Ensure daily backup of the raw input files on an Azure storage account](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231790) - Daily backup of raw input files have been set up. No further action is needed.
3. [231299 Review & ensure database backup policy is correct](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231299) - The policy for db backup is is reviewed and tested. No further action is needed.
4. [233516 Comparator set for special schools](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/233516) - content updated as expected. No further action is needed.

## Test Deliverables

### Documents

- Release test plan

### Reports

#### Release Overview

- **Original Planned Release:** 2024.10.02
- **New Release Version:** 2024.10.04
- **Hotfixes Included:**
  - **Hotfix:** Fix for message banner not showing on the Census page when the comparator set for the school is missing.
  - **Additional Ticket 233516 with hotfix:** One additional ticket was added to the release as part of the hotfix.
  - **Hotfix Update:** It was later discovered that the updates from 233516 should have been applied to two pages instead of one. An additional hotfix was made to address this.

#### Issue Identified

- **Issue:** The message banner was not appearing on the Census page when the comparator set was missing for a school.
  - **Resolution:** Hotfix was applied to correct this issue, which updated only one page. After further review, another hotfix was made to apply the necessary changes to a second page.

#### Release 2024.10.3

- **Abandonment:** Release 2024.10.3 was abandoned due to the incomplete update that only updated one of the two necessary pages. A new release, 2024.10.4, is scheduled to resolve this.

#### Current Release (2024.10.4)

- **Final Release:** The 2024.10.4 release contains all necessary hotfixes, along with one additional ticket.
- **Testing Impact:** The release testing plan is not impacted by these changes, as the hotfixes are isolated and do not affect overall test coverage.

## Entry and Exit Criteria

**Entry Criteria:**

- All Fixes and updates have successfully passed lower quality gates
- All changes have been deployed in pre-production environment
- pipe run completed in pre-production

**Exit Criteria:**

- All tests have been completed with the exception of data validation testing, with any issues found logged in the product backlog
- Issue priority agreed with Product owner;
  - Any new critical issues found during release test are resolved prior to release and retested
  - Any new major/minor issues found during release test will be scheduled for next release

Note: The CFR data load will take place in pre-production but won't be released to production until agreed with product owner.

## Risk Management

**Risk Identification:**

- Pre-production and production has existing data, which will require updating/modifying as the updates/fixes included in this release requires pipeline rerun.
- There is a risk of spotting issues with the CFR data during the review process at this stage.

**Risk Mitigation:**

- Ran in isolated clean environment prior to release to confirm the fixes
- Ran in test environment which has identical data to pre-prod and production to confirm fixes
- We have thoroughly checked the data in lower environments. Since we are not releasing the CFR data to production at this stage, we can make any necessary changes to the data file, rerun the processes in pre-production, and only proceed with the CFR 2024 release once we are fully satisfied with the quality and accuracy of the data.

## Review and Approval

**Review Process:**

The release plan will be shared with the Product Owner and relevant stakeholders for review and approval. Upon receiving approval, the release will proceed with the planned testing and deployment activities. After the changes are validated in pre-production, they will be deployed to production for final sanity testing.

**Note:** The CFR data will be loaded into pre-production for review by the stakeholders and will not be released to production until approval. It will be released at the end after other remaining release to production is complete.

**Sign-off:**

Product Owner

\newpage
