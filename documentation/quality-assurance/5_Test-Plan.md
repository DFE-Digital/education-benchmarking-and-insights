# Test Plan

## Purpose

This document provides a **practical plan for managing testing activities** during sprints, releases, and UAT phases. It is a delivery-focused guide that aligns with the overarching Test Strategy and the day-to-day QA practices outlined in the Test Approach.

## How This Differs From Test Strategy and Approach

- **Test Strategy** defines *why*, *what*, and *where* we test across the product and pipeline.
- **Test Approach** defines *how* QA works day-to-day within agile delivery.
- **This Test Plan** defines *when* and *what is planned* per sprint or release, with a focus on **planning**, **coordination**, and **UAT readiness**.

## QA Planning Within a Sprint

Each sprint includes a defined set of test activities mapped to the delivery cadence.

**Planned QA Activities:**

| Phase         | Activity                                | Owner        |
|---------------|-----------------------------------------|--------------|
| Pre-sprint    | Review stories for testability          | QA, PO       |
| Sprint start  | Create test subtasks in DevOps          | QA           |
| During sprint | Manual testing, automation, exploratory | QA/Dev       |
| End of sprint | Regression testing, bug retest          | QA           |
| Sprint review | Demo prep, defect summary               | QA, Dev      |
| Post-sprint   | UAT prep or production readiness checks | QA, Delivery |

## Planning Assumptions

- Test planning is sprint-based; no large-scale test phases.
- UAT and exploratory testing are layered on top of automated coverage.
- Acceptance criteria form the core of scripted tests, refined collaboratively.
- Defects are logged and linked to stories or epics for traceability.

## UAT Coordination

**UAT Objectives:**

- Validate production-like data outputs (esp. RAG calculations, comparisons).
- Confirm user journeys from different roles behave as expected.
- Ensure successful data ingestion from real CSVs or extracts.

**UAT Planning:**

| Task                         | Owner            |
|------------------------------|------------------|
| Define UAT scenarios         | QA + PO          |
| Prepare data & test accounts | QA + DevOps/Data |
| Track issues during UAT      | QA               |
| Capture feedback in DevOps   | QA + PO          |

## QA Deliverables Per Sprint

| Deliverable                 | Description                                          |
|-----------------------------|------------------------------------------------------|
| QA Subtasks in DevOps       | Track test coverage per story                        |
| Exploratory test charters   | For complex or high-risk stories                     |
| Automated test coverage     | E2E/API/Accessibility per eligible story             |
| Defect reports              | Linked to bugs or reopened stories                   |
| UAT support notes           | Environment setup and known limitations              |

## Tracking Progress

All testing activities are tracked directly in Azure DevOps:

- QA subtasks (per user story)
- Bug/defect tickets
- Linked test results or automation reports
- Sprint board status for visibility

We do not use a separate test management tool — test coverage is inferred through:

- Acceptance criteria (Gherkin where possible)
- Linked test evidence
- Automated run logs (CI)

## Success Measures

Testing success for each sprint is evaluated by:

- % of stories tested and signed off
- Number and severity of post-release defects
- UAT defect volume and resolution time
- Feedback from retrospectives

## Open Questions / To Be Confirmed

- UAT schedule and sign-off process per release.
- Ownership of long-term regression test packs.
- Separate performance/load testing plan (outside MVP).

## Living Plan

This Test Plan is not static — it evolves based on:

- Sprint retrospectives
- Changing priorities
- Team feedback
- External dependencies (e.g. data source availability)

<!-- Leave the rest of this page blank -->
\newpage
