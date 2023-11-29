# Test Plan


## History and Approvals

| Version | Name    | Role      | Date       | Remarks       |
|---------|---------|-----------|------------|---------------|
| XX      | QA Name | Test Lead | 01/01/2021 | First Release |

## Project Overview
### Purpose
The purpose of the software is to seamlessly integrate the functionalities of View My Financial Insights (VMFI) and School Financial Benchmarking (SFB). The end goal is to provide users with a unified platform where they can compare school financial data, view school spending, and analyse expenditures in comparison with similar schools. Another objective is to build data pipelines that will read data from raw data files and pass them to pipeline to store them into the schema which will later be used in the system.

### Key Features and Functionalities
The software will empower users with the ability to conduct benchmarking by comparing school financial data. Additionally, users can easily view their own school's spending patterns and compare these expenditures with similar schools or academies. There would also be a option to see a forecast of spending. Implementation of data pipelines to ingest data in the pipeline.

## Project Scope
### In-Scope Elements for Testing
Testing will encompass various elements, including accessibility, cross-browser functionality, automated and manual functional testing, regression testing, unit testing, integration testing, and API testing.

### Out-of-Scope Elements for Testing
The performance of the system under load conditions is currently out of scope for this testing phase.

## Special Attention
A dedicated focus will be placed on testing the data upload feature to ensure that all data is accurately processed and integrated into the system.

## Testing Objectives
### Primary Goals
The primary testing objectives include verifying that the product successfully combines the features of SFB and VMFI. Additionally, it aims to confirm the implementation of all acceptance criteria and ensure comprehensive logging, fixing, or backlog management of issues and bugs discovered during testing.

### Quality Attributes
A key quality attribute is data ingestion validation.

## Stakeholders
Key stakeholders include Daniel Tovey (Product Owner) and representatives from the Department for Education, Keld Oshea and Gavin Monument.

## Testing Environment
The project will utilise multiple environments, including development, testing (for automated testing), pre-production, and production. Cross-browser testing will require a subscription to Browserstack. (details of the environment will later be added here)

## Test Deliverables
Test scripts for each user story will be generated, and results will be diligently updated within the corresponding tickets, providing a comprehensive view of testing activities.

## Testing Schedule
Testing activities will be synchronised with the sprint cycle, ensuring that testing is completed for selected user stories within each sprint.

## Testing Approach
### Methodologies
The testing approach will encompass manual testing for acceptance criteria validation, automated testing for end-to-end scenarios.

### Test Case Design and Execution
Each user story will be associated with a QA ticket containing detailed test scripts. Manual testing will follow automated testing to validate acceptance criteria.

## Test Case Design
Test cases will be meticulously designed for each acceptance criterion, utilising the Gherkin syntax for clear and structured documentation.

## Test Data
### Required Test Data
Synthetic test data will be utilised to simulate various scenarios.

### Data Generation
The Data Engineers on the project will collaborate to generate relevant test data, facilitating thorough testing.

## Risk Management
### Potential Risks
A potential risk is the time constraint within sprints for adequately covering automated tests, manual testing, and cross-browser testing.

### Risk Mitigation
The entire team will collectively assess and address risks during sprint planning, ensuring a proactive approach to risk mitigation.

## Defect Management
Defects will be reported via DevOps tickets, closely linked to the respective user stories.

## Testing Team
The primary tester, Faizan Ahmad, will lead testing efforts. The entire team will share the responsibility for maintaining overall product quality.

## Communication Plan
Communication will be facilitated through DfE email or Teams. Technical discussions will primarily take place within DevOps tickets.

## Entry Criteria
| No | Entry Criteria                | Entry Criteria                             | 
|----|-------------------------------|--------------------------------------------|
| 1  | dev ticket has been completed | All Automation Tests/Regression Tests Pass | 

## Exit Criteria
The testing phase will be considered complete when all acceptance criteria have been successfully implemented and tested, automated tests have been integrated, and accessibility testing has been comprehensively conducted.

### Pass and Fail Criteria
| Test Status | Definition                                            | 
|-------------|-------------------------------------------------------|
| Passed      | Test Outcome meets the Expected Test Outcome          | 
| Failed      | Test Outcome DOES NOT meets the Expected Test Outcome |
| Blocked     | Existing Defect Blocks Test Execution                 |      
| Untested    | Yet to be Tested                                      |       


## Measurement of Success
The success of the testing effort will be measured through metrics such as automated testing coverage, defect density, and overall adherence to acceptance criteria.

## Open Points
The data pipeline implementation is yet to be finalised and testing the data pipeline will depend on how it is developed.

#  Deliverables/Reporting
Test scripts for each story will be delivered as part of testing activities. 