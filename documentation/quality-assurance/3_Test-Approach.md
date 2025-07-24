# Test Approach

## Context

The FBIT service merges financial insight and benchmarking functionalities into a unified digital platform. This Test Approach document outlines the tactical testing methods and day-to-day QA practices aligned with agile delivery. It complements the Test Strategy by detailing how testing is embedded in the development lifecycle without restating strategy-level decisions (e.g., test types, tools, or environments).

This Test Approach defines **how** testing is performed by the QA team within the agile process — not what is tested or with which tools. It supports the high-level Test Strategy by outlining day-to-day practices and collaborative responsibilities that enable continuous delivery of high-quality features.

## Objectives

- Define the **daily practices** followed by the QA function.
- Clarify the **roles and expectations** for testers within the agile team.
- Describe how testers **engage across the sprint lifecycle**.
- Outline the **process for executing and evolving testing** alongside development.

## Integration with Agile Delivery

**QA Touchpoints per Sprint:**

| Phase              | QA Activity                                                                 |
|--------------------|-----------------------------------------------------------------------------|
| Backlog Refinement | Review stories for testability, define acceptance criteria collaboratively. |
| Sprint Planning    | Create test subtasks, estimate effort, identify automation scope.           |
| During Sprint      | Pair with devs, write & run tests in parallel, log early feedback.          |
| Code Complete      | Perform exploratory/manual checks and close QA subtasks.                    |
| End of Sprint      | Support UAT, update regression packs, retrospective contributions.          |

**Definition of Done (QA-Specific):**

A story is not "Done" from QA's perspective unless:

- Tests (unit/integration/automation/manual) covering key acceptance paths are complete.
- Exploratory testing is documented with findings (even if none).
- Edge cases and error states are validated.
- A QA subtask or checklist is marked complete with notes and/or links to test runs.

## QA Responsibilities in Practice

| Activity                      | Responsibility                                                    |
|-------------------------------|-------------------------------------------------------------------|
| Test Case Design              | Design scenario-based tests from acceptance criteria & examples.  |
| Exploratory Testing           | Create test charters based on risk, unknowns, and usage patterns. |
| Automation Collaboration      | Work with devs to automate tests incrementally, not post hoc.     |
| Peer Review & Quality Gate    | Review PRs from a testability standpoint.                         |
| Continuous Test Feedback      | Provide fast feedback during in-sprint testing.                   |
| Cross-Functional Contribution | Raise usability, accessibility, or performance concerns early.    |

## Automation Approach (QA-Led)

- E2E tests are written in parallel to stories when UI changes are involved.
- Feature files are Gherkin-based, making them readable for devs, BAs, and testers.
- Reusability and fixture libraries are preferred over copy-paste test steps.
- Tests are written to run headlessly on CI but verified locally with visual trace when debugging.

## Exploratory Testing Practice

Exploratory testing is a critical layer in the quality process, especially for:

- Newly integrated components.
- Ambiguous or loosely scoped stories.
- Regression checks where automation is incomplete.

We use the following lightweight structure for charters:

- **Goal**: What we want to learn.
- **Scope**: Feature, role, or flow.
- **Constraints**: Devices, browsers, data.
- **Notes**: Observations and insights.
- **Bugs/Tasks**: Logged as DevOps items.

## QA Collaboration Cadence

| Cadence    | Activity              | Purpose                                      |
|------------|-----------------------|----------------------------------------------|
| Daily      | Standup               | Identify blockers or test environment issues |
| Weekly     | QA Catch-up           | Share flaky test insights, tool upgrades     |
| Sprint End | Retrospective         | Reflect on test debt, missed cases           |
| Continuous | Dev Pairing & Reviews | Improve testability and avoid regressions    |

## QA Maintenance Responsibilities

- Maintain test data fixtures for automation.
- Review and deprecate outdated E2E scenarios.
- Tag and isolate flaky tests for triage.
- Participate in documentation updates and internal QA wikis.

## Continual Improvement Focus Areas

- Evolve regression packs incrementally with each sprint.
- Shift accessibility and API testing further left.
- Encourage developers to review and contribute to E2E coverage.
- Automate commonly repeated manual flows or admin interactions.

<!-- Leave the rest of this page blank -->
\newpage
