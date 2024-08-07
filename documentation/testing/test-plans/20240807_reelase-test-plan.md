# Release Test Plan
Release date: 07/08/2024

### Introduction
The purpose of this test plan is to outline the approach and scope for testing the updates made as part of this release. It will detail the various types of testing that will be conducted as part of release process.  This plan aims to ensure all aspects of the updates are thoroughly tested and meet the required standards before deployment.

### Scope
This test plan covers the functional and sanity testing to validate all features and bug fixes included in the release.

### Test Strategy
#### Types of Testing
- Manual Functional Testing
- Sanity Testing

### Approach
#### Manual functional Testing:
Manual functional testing will be carried out to validate fixes and updates are in place in pre-prod.
#### Sanity Testing
This testing will be carried out in pre-prod to ensure all functionalities are working as expected.

### Test Scope
#### Issues/Updates to be Tested:
- [Trust to Trust Journey - breakdown of school phases is not shown](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/217226)
This update is to show school phases when viewing the comparator set of the trust. Tested in our test environment and will be verified again in preprod. 

- [221083 double counting of academies funding](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/221083)
The bug with academies counting was fixed to remove the double counting of a field. tested in test and will be manually verified again in preprod. 
- [216667 Priority percentages incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/216667) The spending priority percentage is not changes from percentile to percentage. Tested in test and will be validated again in preprod. 
- [217308 POST MVP - Trust Journey - clickable visuals ](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/217308) the school graphs on trust homepage are now clickable. tested in test and will be manually verified again in preprod. 
- [218546 Other costs - should not be shown as priority on homepage](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/218546) The other costs is no longer flagged on homepage. Tested in test and will be manually verified again in preprod. 


### Issues Not to be Tested:
- [213294 Migration platform to .NET 8](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/213294)
We updated our APIs to .net and full regression testing was conducted in test. 
- [219333 Add anonymous user requests to MI Dashboard]  (https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/219333)
  The addition of tracking cookie opt-out means some user requests will be not be able to be correlated to individual users. These is now displayed as their own metric in the Users MI dashboard part.
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
Potential valid requests could be blocked with WAF changes.

#### Risk Mitigation:
We plan to continue to monitor the firewall logs to ensure potentially valid requests are not blocked as a result of WAF changes.
### Review and Approval
#### Review Process:
The release plan will be shared with the Product Owner for review and approval. Following their review, the updates will proceed to pre-production for final sanity checks.
#### Sign-off:
Product Owner
