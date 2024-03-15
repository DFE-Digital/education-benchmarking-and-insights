# Test Plan

## History and Approvals

| Version | Name         | Role         | Date       | Remarks                              |
|---------|--------------|--------------|------------|--------------------------------------|
| 1.1     | Faizan Ahmad | Test Engineer | 29/11/2023 | Need to be reviewed by other team members |
| 1.2     | Faizan Ahmad | Test Engineer | 27/02/2024 |                                      |

## Project Overview

### Purpose
The purpose of the software is to seamlessly integrate the functionalities of View My Financial Insights (VMFI) and School Financial Benchmarking (SFB). The end goal is to provide users with a unified platform where they can compare school financial data, view school spending, and analyse expenditures in comparison with similar schools. Another objective is to build ETL data pipelines to import and operate on raw data files and deposit them into some Azure storage solution.

### Key Features and Functionalities
The software will empower users with the ability to conduct benchmarking by comparing school financial data. Additionally, users can easily view their own school's spending patterns and compare these expenditures with similar schools or academies. There would also be an option to see a forecast of spending. Implementation of data pipelines to ingest data in the pipeline.

### Project Scope

#### In-Scope Elements for Testing
Testing will encompass various elements, including manual acceptance testing, Automated E2E tests, API functional testing, accessibility, cross-browser functionality.

#### Out-of-Scope Elements for Testing
The performance of the system under load conditions is currently out of scope for this testing phase as the system is not live yet.

#### Special Attention
A dedicated focus will be placed on ensuring conformance with the required input schema. We want to make sure that any variability in the input due to user error is dealt with to the best of our capabilities to prevent any issues in the ETL process.

## Testing Objectives

### Primary Goals
The primary testing objectives include verifying that the product successfully combines the features of SFB and VMFI. Additionally, it aims to confirm the implementation of all acceptance criteria and ensure comprehensive logging, fixing, or backlog management of issues and bugs discovered during testing.

### Stakeholders
Key business stakeholders include Daniel Tovey (Product Owner) and representatives from the Department for Education, Keld Oshea and Gavin Monument.

## Testing Environment
The project will utilise multiple environments, including development, testing (for automated testing), pre-production, and production. Cross-browser testing will require a subscription to Browserstack. (details of the environment will later be added here)

## Test Deliverables
Test scripts for each user story will be generated, normally taken out from the acceptance criteria, and results will be diligently updated within the corresponding tickets, providing a comprehensive view of testing activities.

## Testing Schedule
Testing activities will be synchronised with the sprint cycle, ensuring that testing is completed for selected user stories within each sprint.

## Testing Approach

### Methodologies
The testing approach will encompass manual testing for acceptance criteria validation, automated testing for end-to-end scenarios.

### Test Entry Criteria
Entry criteria to start the testing of a particular feature will be dependent on the following. The automated tests for the new feature will be written along with the dev work when possible.

| No  | Entry Criteria                  |
|-----|---------------------------------|
| 1   | Dev ticket has been completed   |
| 2   | All integration tests are passing |
| 3   | All unit tests are passing      |

### Exit Criteria
The testing phase will be considered complete when all acceptance criteria have been successfully implemented and tested, automated tests have been integrated, and accessibility testing has been comprehensively conducted.

### Pass and Fail Criteria
| Test Status | Definition                              |
|-------------|-----------------------------------------|
| Passed      | Test Outcome meets the Expected Test Outcome |
| Failed      | Test Outcome DOES NOT meet the Expected Test Outcome |
| Blocked     | Existing Defect Blocks Test Execution   |
| Untested    | Yet to be Tested                        |

## Test Case Design and Execution

Each user story will be associated with a QA ticket containing detailed test scripts. Manual testing will follow automated testing to validate acceptance criteria.

### Test Case Design

Test cases will be meticulously designed for each acceptance criterion, utilising the Gherkin syntax (Given When Then Format) for clear and structured documentation. If the acceptance criteria are specific enough, they can be used as test cases.

#### Execution of Test:

**End-to-End (E2E) Tests:**
If the user story has changes for the front end, then E2E tests are written for them. E2E tests are crafted concurrently with the development of the user story. If the tests cannot be finalised before the completion of the development work, they are completed as much as possible. After the development work is finished and merged, E2E tests are concluded and integrated.

**Accessibility Tests:**
If the user story is about adding a new page, then automated accessibility (A11y) tests are incorporated for every new page developed, encompassing various scenarios such as assessing error messages and expanding all dropdowns on the page.

**Manual Functional Test:**
Once the dev work is complete manual testing is done to validate all the acceptance criteria have been met.

**Cross-browser testing:**
If the user story in progress has some non-GDS components developed, then cross-browser testing will come into play once the dev work is complete. Any issues spotted will be logged as an additional task under that user story. The logged ticket should have the details of the browser, OS, and device along with a screenshot of the issue.

**API Functional Test:**
If the user story has changes to the back end API, then API functional tests are added for the changes being made. They are normally done after the dev work is done.

**User Acceptance Testing:**
User Acceptance Testing (UAT) is the final phase of testing conducted before the system is released to the end-users. The purpose of UAT is to ensure that the system meets the specified requirements and that data has been successfully translated into the system. Testing will focus on checking the data integrity.
UAT will be conducted in a dedicated testing environment that closely mirrors the production environment with real data. We'll use actual data files to check if the system works correctly and does the right calculations by writing automated tests/scripts. The schedule of UAT is yet to be defined. All team will be involved in UAT with tech team focusing on writing autoamted scripts and rest team on manual testing. 

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
Defects will be reported via DevOps task, closely linked to the respective user stories.

## Testing Team
The primary tester, Faizan Ahmad, will lead testing efforts. The entire team will share the responsibility for maintaining overall product quality.

## Communication Plan
Communication will be facilitated through Teams or Slack. Discussions will primarily take place within DevOps tickets for the sake of visibility and back tracing.

## Measurement of Success
The success of the testing effort will be measured through metrics such as automated testing coverage, defect density, and overall adherence to acceptance criteria.

## Open Points
The data pipeline implementation is yet to be finalised, and testing the data pipeline will depend on how it is developed.

## Deliverables/Reporting
Test scripts for each story will be delivered as part of testing activities.
