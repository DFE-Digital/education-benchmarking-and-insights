# Test Approach


## Project Background
**Test Objectives**
  - Confirm that the service performs as per the defined User Needs, outlined during the discovery phase.
  - Verify that the service meets the business and technical requirements set out by the DfE.
  - Deliver a quality product by finding defects/bugs so that they can be fixed before delivering the service to the end user.

**Project Management**
  
The product backlog and project sprints are managed within Azure DevOps using the scrum methodology.
  - User Stories in DevOps go thrugh the following stages during the development process:
    - To Do
    - In Development
    - In Review
    - In Test
    - Complete
    - Closed

    Development of user stories is managed on a sprint board and goes through the following stages:
    - Ready
    - In Progress
    - Complete
    - Closed
Each status and Definition of Done act as control 'gates' for all work passing through this process.
  
  **Definition of Done**
    
  - Development for the ticket is 'code complete.'
    - Work items have been linked (use AB#).
    - Your code builds clean without any errors or warnings.
    - You have run all unit/integration tests and they pass.
    - Your branch has been rebased onto main.
    - You have tested by running locally.
    - PR raised, reviewed & approved.
    - Code squash merged into main. 
  - The functionality for the ticket has been tested and meets the acceptance criteria. 
  - Ticket status is updated on Azure DevOps. 
  - A sign-off is in place from the reporter and Product Owner (need to check if this is the expectation).

## Test Engineer Roles and Responsibilities
- As part of the project, the test engineer will be involved in the following activities:
  - Involve in the refinement session - Identify functional and non-functional requirements from the tickets refinement session, add relevant scenarios, review acceptance criteria, and identify test data requirements.
  - Review and add automated tests - Write automated feature/e2e tests ahead of/in step with the development of the service, maintain existing feature tests, and review unit and integration tests written by developers.
  - Perform manual acceptance testing in Dev environment - Perform manual tests in the Dev environment to ensure that edge case scenarios and other scenarios that cannot be automated are tested.
  - Perform ad-hoc exploratory testing in the Test Environment - Perform exploratory testing to ensure that all parts of the service integrate together correctly.
  - Contribute to overall quality - Work towards contributing to the overall quality of the systems, process, and deliverables by ensuring each status of the ticket satisfies the pass criteria.

## Testing Activities
### Overview
The following diagram shows the Agile Testing Quadrants outlining where each different type of testing sits, its order of implementation, and the associated categories.
![agile-testing-quadrants](images\agile-testing-quadrants.png)
The testing pyramid below shows the value of implementing different types of automated testing within a software project.
![testing-pyramid](images\testing-pyramid.png)

### Automated Testing
#### Unit Testing
Unit Tests are developed by writing a test case to cover the requirement without any code supporting it. This test obviously fails. Then, the simplest code is written to pass the test. When another requirement comes along that alters the code, another test is written to satisfy this requirement, and the code is refactored as appropriate.
These tests are the responsibility of developers to write and maintain. Reviews of the unit test coverage by test engineers or other members of the team should be requested as appropriate.

**Technology Selected**
  - [Xunit](https://github.com/xunit)
  - [AutoFixture](https://github.com/AutoFixture/AutoFixture)
  - [FluentAssertion](https://github.com/fluentassertions/fluentassertions)
  - [Moq](https://learn.microsoft.com/en-us/shows/visual-studio-toolbox/unit-testing-moq-framework)

#### Integration Testing
Integration tests are used to ensure that parts of the code and frameworks work together in the correct way to create a working service. These tests build upon the quality assured during unit testing but are faster and lighter than end-to-end tests, allowing them to be run frequently and earlier in CI.
These tests are the responsibility of the developers to write and maintain in collaboration with the test engineers.
- **Technology Selected**
- [Moq](https://learn.microsoft.com/en-us/shows/visual-studio-toolbox/unit-testing-moq-framework)
- [Xunit](https://github.com/xunit)
- [ASP.NET Integration Tests](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0)
- [AngleSharp](https://github.com/AngleSharp/AngleSharp)
- [AutoFixture](https://github.com/AutoFixture/AutoFixture)

#### Feature/E2E Testing
Feature and end-to-end tests simulate the behavior of an end user. These tests are the responsibility of test engineers to write and maintain in collaboration with developers.
**Technology Selected**
  - [Playwright](https://playwright.dev/dotnet)
  - [Xunit](https://github.com/xunit)
  - [Specflow](https://specflow.org/)

#### API Testing
API tests are designed to test that the correct responses are received from the API when requests are made to it. These tests are written by collaboration of dev and test engineer.
**Technology Selected**
  - [Moq](https://learn.microsoft.com/en-us/shows/visual-studio-toolbox/unit-testing-moq-framework)
  - [Xunit](https://github.com/xunit)
  - [FluentAssertion](https://github.com/fluentassertions/fluentassertions)

#### Performance Testing
Performance testing is used to evaluate how the service is able to cope in terms of its stability, responsiveness, and usability when put under different simulated workloads. We yet have to implement it before going live. 

**Technology Selected**
  
[will be added later on once we implement it]

#### Accessibility Testing
Automated accessibility testing will be carried out using Deque.axe.playwright library to generate feedback on whether the service meets high-level accessibility requirements in line with WCAG 2.2 AA. This will run as part of the pipeline in the later deployment steps from development to test and production.

**Technology Selected**
  -  [Deque.axe.playwright](https://github.com/dequelabs/axe-core-nuget/blob/develop/packages/playwright/README.md)
  - [FluentAssertion](https://github.com/fluentassertions/fluentassertions)
  -  [Playwright](https://playwright.dev/)

#### Security Scans
Security scans help identify vulnerabilities and mitigate them that can lead to unauthorized access, data breaches, and theft of sensitive information.
[Need to check how we are going to implement - STA uses OWASP ZAP security scans] This testing will be carried out when:
  - A new API is created.
  - A new Front End controller is added.
  - A new Front End controller method is added/updated.
  - On a release to PRE-PROD.
  - As a smoke test on a release to PROD.

**Technology Selected**

[will be added later on once we implement it]
### Manual Testing

#### Acceptance Testing (exploratory)

In order to complement the automated testing being written and run for new features, acceptance testing is carried out to ensure that the requirements set out in the story are being correctly met. This is mostly used to test areas that cannot be covered by automated tests.

Acceptance testing is carried out in an exploratory format by the test engineer(s) and/or developers. Test charters are created based on the acceptance criteria outlined in the story. Findings are logged against the story, and any defects raised as bug tickets.

#### Ad-hoc Exploratory Testing

On an ad-hoc or release basis, exploratory testing is carried out to ensure that the service integrates together correctly and works as expected on all supported devices.

This testing is carried out by test engineer(s) and/or developers when it is deemed that there is a high risk of integration or regression issues. This shall also be used for checking the compatibility of the site with all required browsers and operating systems. Test charters are created outlining the areas of the service to be tested.

### Testing Requirements

#### Supported Browsers

**Insert supported browsers**

| Operating System | Browser                      | Versions                     |
|------------------|------------------------------|------------------------------|
| Windows          | Edge                         | Latest                       |
|                  | Google Chrome                | Latest                       |
|                  | Mozilla Firefox              | Latest                       |
| macOS            | Safari                       | 12 and later                 |
|                  | Google Chrome                | Latest                       |
|                  | Mozilla Firefox              | Latest                       |
| iOS              | Safari for iOS               | 12.1 and later               |
|                  | Google Chrome                | Latest                       |
| Android          | Google Chrome                | Latest                       |
|                  | Samsung Internet             | Latest                       |

Any issues or discrepancies between browsers will be raised as defects and then assessed for priority and severity.
#### Environments

| Env Prefix | Env Name       | Testing             |
|------------|----------------|---------------------|
| -          | Local          | -                   |
| d01        | Dev            | Manual acceptance   |
| TBC        | Dev2           | Feature/e2e         |
| TBC        | Test           | Non-functional tests |
| TBC        | Production/Live| -                   |

**Path to live**

[Will be added later on ]

//TODO: Create a path to live diagram, that visualizes the stage gates from ideation through to release
