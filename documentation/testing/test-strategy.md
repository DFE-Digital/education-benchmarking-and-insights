# Test Approach

On this page:
- [Project Background](#project-background)
- [Test Objectives](#test-objectives)
- [Project Management](#project-management)
- [Test Engineer Roles and Responsibilities](#test-engineer-roles-and-responsibilities)
- [Testing Activities](#testing-activities)
    - [Overview](#overview)
    - [Automated Testing](#automated-testing)
    - [Manual Testing](#manual-testing)
- [Testing Requirements](#testing-requirements)
- [Environments](#environments)


## Project Background

The developed system will replace View my financial Insights(VMFI) and School Financial Benchmarking (SFB) services. The system is to provide users with a unified platform where users can compare schooling institutions financial data, view school spending, and analyse expenditures in comparison with similar schools.

## Test Objectives

- Confirm that the service performs as per the defined User Needs, outlined during Discovery phase.
- Verify that the service meets the business and technical requirements.
- To deliver a quality product by finding defects/bugs so that they can be fixed before delivering the service to the end user.

## Project Management

The product backlog and project sprints are managed within Azure DevOps.

User Story contained within DevOps go through the following statuses during the development process:
* To Do
* In Development
* In Review
* In Test
* Complete
* Closed

Each status and Definition of Done acts as a control ‘gate’ for all work passing through this process.


### Definition of Done
* Development for the ticket is ‘code complete’. The functionality for the ticket has been tested and meets the acceptance criteria.
* Ticket status is updated on Azure DevOps
* A sign off is in place from the reporter and Product Owner.
* If work is not correct or incomplete, tickets get moved back to ‘In Dev’ and the cycle starts again.

## Test Engineer Roles and Responsibilities
As part of the project test engineer will be involved in the following activities.

- **Involve in the refinement session** - The Test Engineer will identify the functional and non-functional requirements from the tickets refinement session
  and will add the relevant scenarios and, review the acceptance criteria. Test Engineer will also identify the test data requirements at this stage.

- **Review and add automated tests** - The Test Engineer will write automated feature/e2e tests ahead of/in step with the development of service. This shall be done
  in collaboration with the developers. The Test Engineer will also maintain existing feature tests as well as reviewing unit and integration tests written by the
  developers.

- **Perform manual acceptance testing in Dev environment** - The Test Engineer will perform manual tests in the Dev environment to ensure
  that edge case scenarios, and any other scenarios that cannot be automated, are tested.

- **Perform ad-hoc exploratory testing in the Test Environment** - The Test Engineer will perform exploratory testing to ensure that all parts of the service
  integrate together correctly.

- **Contribute to overall quality** - The Test Engineer will work towards contributing to the overall quality of the systems, process and deliverables. This will
  be by ensuring each status of the ticket satisfies the pass criteria.


## Testing Activities
### Overview

The following diagram shows the Agile Testing Quadrants which outlines where each different type of testing sits, it's order of implementation,
and the categories that it is associated with.

![agile-testing-quadrants](C:\Users\fahmad\RiderProjects\education-benchmarking-and-insights-07-12\documentation\testing\images\agile-testing-quadrants.png)

The testing pyramid below shows the value of implementing different types of automated testing within a software project.

![testing-pyramid](C:\Users\fahmad\RiderProjects\education-benchmarking-and-insights-07-12\documentation\testing\images\testing-pyramid.jpg)

## Automated Testing
We have 2 solutions. One is Platform and other is Web. Both have their own tests. 

### Web Application Testing

#### Unit Testing

Unit Tests are developed by writing a test case to cover the requirement, without any code supporting it, this test obviously fails. Then simplest
code is written to pass the test. When another requirement comes along that alters the code then the another test is written to satisfy this
requirement and the code is refactored as appropriate.

These tests are the responsibility of developers to write and maintain. Reviews of the unit test coverage by test engineers or other members of the team should
be requested as appropriate.

##### Technology Selected

[Xunit](https://github.com/xunit)

#### Integration Testing

Integration tests are used to ensure that parts of the code, and frameworks are work together in the correct way to create a working
service. These work by compiling the code locally using mocked data on which actions and assertions are executed. Integration tests build upon
the quality that is assured during unit testing but are faster and lighter than end-to-end tests allowing them to be run frequently, and earlier in CI.
These tests will run in the build stage after code changes are made.
These tests are the responsibility of the developers to write and maintain in collaboration with the test engineers.


##### Technology Selected

[Moq](https://learn.microsoft.com/en-us/shows/visual-studio-toolbox/unit-testing-moq-framework)

[Xunit](https://github.com/xunit)

#### Feature/E2E Testing

Feature and end-to-end tests are both designed to simulate the behaviour of an end user. Tests are written from an end-user perspective and run
against deployed environments through a web-browser (such as Chrome, Safari, Firefox etc.) driven by web-drivers. This is the slowest type of (functional) automated
testing are run later than unit and integration tests (see testing pyramid).

These tests will run in testing environment after build stage in Testing environment. The testing environment will have pre loaded data to be used by tests. 
These tests are the the responsibility of the test engineers to write and maintain in collaboration with the developers.

##### Technology Selected

- [Playwright](https://playwright.dev/dotnet)
- [Xunit](https://github.com/xunit)
- [Specflow](https://specflow.org/)

### Platform API Testing

API tests are designed to test that the correct responses are received from the API when requests are made to it. These tests are run as part of the fuctional testing
which runs as part of the pipeline with each deployment. These tests are written by collaboration of dev and test engineer.
We have 2 types of API Tests. 

#### API Integration Test
Same as Web Application Integrations tests these tests are the responsibility of the developers to write and maintain in collaboration with the test engineers.
In the pipeline the tests are kicked off in build stage after code changes in platform project. 

##### Technology Selected

- [Moq](https://learn.microsoft.com/en-us/shows/visual-studio-toolbox/unit-testing-moq-framework)

- [Xunit](https://github.com/xunit)

#### API Functional Tests
 These tests checks the functionality and behaviour of deployed APIs. These tests are kicked off after build stage using the test environment after code changes are made.
These tests are the responsibility of the developers and Test Engineer(s) to write and maintain with collaboration. 
##### Technology Selected

- [Xunit](https://github.com/xunit)
- [Specflow](https://specflow.org/)


#### Accessibility Testing
Automated accessibility testing will be carried out using [Deque.axe.playwright](https://github.com/dequelabs/axe-core-nuget/blob/develop/packages/playwright/README.md)
library to generate feedback on whether the service meets high level accessibility requirements in line with WCAG 2.2 AA.
These tests will run every weekend in a separate pipeline using the testing environment. 

##### Technology Selected

- [Dotnet SDK](https://dotnet.microsoft.com/download)
- [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
-  [Deque.axe.playwright 4.8.0](https://github.com/dequelabs/axe-core-nuget/blob/develop/packages/playwright/README.md)
- [SpecFlow 3.9.74](https://www.nuget.org/packages/SpecFlow/3.9.74)
- [Playwright 1.4.0](https://playwright.dev/)

#### Performance Testing
Performance testing is used to evaluate how the service is able to cope in terms of its stability, responsiveness and usability when put under different simulated workloads.
This ensures that the service copes with large amount of users accessing the service at one time.


##### Technology Selected

** *add technology here* **

#### Security Scans
[need to check how we are going to implement - STA uses OWASP ZAP security scans] 
This testing will be carried out when:
- A new API is created
- A new Front End controller is added
- A new Front End controller method is added / updated.
- On a release to PRE-PROD
- As a smoke test on a release to PROD.

##### Technology Selected

** *add technology here* **

### Manual Testing

#### Acceptance Testing (exploratory)

In order to compliment the automated testing being written and run for new features, acceptance testing is carried out to ensure that the requirements
set out in the story are being correctly met. This is mostly used to test areas that cannot be covered by automated tests.

Acceptance testing is be carried out in an exploratory format by the test engineer(s) and/or developers. Test charters are created based on the acceptance
criteria outlined in the story. Findings are logged against the story and any defects raised as bug tickets.

#### Ad-hoc Exploratory Testing

On an ad-hoc or release basis, exploratory testing is be carried out to ensure that the service integrates together correctly and works as expected on all supported devices.

This testing is carried out by test engineer(s) and/or developers when it is deemed that there is a high risk of integration or regression issues. This shall be also used
for checking the compatability of the site with all required browsers and operating systems. Test charters are created outlining the areas of the service to be tested.


## Testing Requirements

### Supported Browsers

** *Insert supported browsers* **

Any issues or discrepencies between browsers will be raised as defects and then assessed for priority and severity.

## Environments

### Definitions

| Env Prefix | Env Name       | Azure License | Testing             | 
|------------|----------------|---------------|---------------------|
| -          | Local          | -             | Unit                |
| TBC        | Dev            | Development   | Manual acceptance   |
| TBC        | Dev2           | Development   | Feature/e2e         |
| TBC        | Test           | Test          | Non-functional tests |
| TBC        | Production/Live | Production    | -                   |
| TBC        | Showcase       | Production    | Security(flexible)  |

## Local
The unit and integration testing is performed by the developers to test their code.

## Development
** environments details will be added here ** 


## Path to live

![Path to Live](C:\Users\fahmad\RiderProjects\education-benchmarking-and-insights-07-12\documentation\testing\images\path-to-live.jpg)


## Test data overview

Dummy test data will be created and loaded in test environmnet. if there is a need for a test to seed more data then unique data will be used which will later be deleted at the end of the test. 

