# Release Test Plan
Release date: 07/14/2024

### Introduction
The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release. It will detail the various types of testing that will be conducted as part of release process.  This plan aims to ensure all aspects of the updates are thoroughly tested and meet the required standards before deployment.

### Scope
This test plan covers the functional and sanity testing to validate all features and bug fixes included in the release.

### Test Strategy
#### Types of Testing
- Manual Functional Testing
- Sanity Testing
- Regression Testing

### Approach
#### Manual functional Testing:
Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod.
#### Sanity Testing
This testing will be carried out in pre-prod to ensure all functionalities are working as expected.
#### Regression Testing
A complete regression will be carried out in pre-prod to ensure all features are working as expected and updates made haven't broken anything else. 

### Test Scope
#### Issues/Updates to be Tested:
1. [Trust to Trust Journey - breakdown of school phases is not shown](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/217226) This update is to show school phases when viewing the comparator set of the trust. Tested in our test environment and will be verified again in preprod.
2. [221083 double counting of academies funding](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/221083) The bug with academies counting was fixed to remove the double counting of a field. tested in test and will be manually verified again in preprod. 
3. [216667 Priority percentages incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/216667) The spending priority percentage is not changes from percentile to percentage. Tested in test and will be validated again in preprod. 
4. [217308 POST MVP - Trust Journey - clickable visuals ](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/217308) the school graphs on trust homepage are now clickable. tested in test and will be manually verified again in preprod. 
5. [218546 Other costs - should not be shown as priority on homepage](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/218546) The other costs is no longer flagged on homepage. Tested in test and will be manually verified again in preprod. 
6. [218538 Data sources page - link to census data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/218538) The content on data sources pages has been updated. The changes will be verified in preprod. 
7. [216448 POST-MVP Content Updates - school homepage and schools we chose](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/216448) This is also content update. Tested in test and will be validated in preprod. 
8. [221270 In Year Balance incorrect on Trust Pages](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/221270) The in year balance on the trust homepage has now been corrected and tested in test. The numbers will be validated again in preprod. 
9. [216448 https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/216448](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/216448) This is content update and has been tested in dev/test. Will be verified again in preprod. 
10. [218109 - MVP - Content Related Fixes ](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/218109) This is also content related updates and verified in test. 
11. [221010 - Breakdown of phases for School comparator set , LA and Trust Homepage](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/221010) We are now showing school phases on comparator pages and trust homepage. This has been verified in test and will be manually tested again in preprod. 
### Updates Not to be Tested:
1. [213294 Migration platform to .NET 8](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/213294) We updated our APIs to .net 8 and full regression testing was conducted in test. 
2. [219333 Add anonymous user requests to MI Dashboard](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/219333) The addition of tracking cookie opt-out means some user requests will be not be able to be correlated to individual users. These is now displayed as their own metric in the Users MI dashboard part.
3. [219590 - Use ClaimTypes.NameIdentifier for user id rather than email](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/219590) This is update with how we are saving user data when custom comparator or data is used. It has been tested in test and no further actions are needed in preprod.  
4. [219227 Add WAF log details to the operational dashboard](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/219227) WAF logs are now added to Operational dashboard and is tested in dev/test. No further testing is required in preprod. 
5. [222350 Implement dead letter queue](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/222350) We have dead letter queue for data pipelines which will dequeue the message after 5 retries so that it is not in the queue forever. Tested in dev/test and no further action is needed in preprod.
6. [217947 BfR Figures Incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/217947) We have made the fixes which requires further validating of the figures. The feature is currently turned off in pre prod and production.

### Test Deliverables
#### Documents:
- Release test plan
#### Reports:
- Summary of testing performed and outcomes

### Entry and Exit Criteria
#### Entry Criteria:
- All Fixes and updates have successfully passed lower quality gates
- All changes have been deployed in pre-production environment

#### Exit Criteria:
- All tests have been completed, with any issues found logged in the product backlog
- Issue priority agreed with Product owner;
    - Any new critical issues found during release test are resolved prior to release and retested
    - Any new major/minor issues found during release test will be scheduled for next release


### Risk Management
#### Risk Identification:
Pre-production and production has existing data, which will require updating/modifying as the fixes included in this release requires pipeline rerun. 

#### Risk Mitigation:
- Ran in isolated clean environment prior to release to confirm fixes
- Ran in test environment which has identical data to pre-prod and production to confirm fixes

### Review and Approval
#### Review Process:
The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will proceed to pre-production for final sanity checks.
#### Sign-off:
Product Owner
