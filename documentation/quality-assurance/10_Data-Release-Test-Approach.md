# Test Approach: Data Releases

## Context

This document is the tactical companion to the data release strategy, which is the Data Releases section of [Test Strategy: Data Ingestion](./3_Test-strategy-data-ingestion.md). It follows the same relationship that the service [Test Approach](./4_Test-Approach.md) has to the service [Test Strategy](./1_Test-strategy-service.md).

The strategy defines what is tested for a data release and the validation model that is applied. This document describes how QA works to deliver that: how QA embeds with the data team, plans each release, and runs the checks step by step with the right people. It does not restate the validation model, nor the release process itself, which is in the [data release guide](../guides/data-release-guide/01_Overview.md).

## Objectives

- Describe how QA operates through a data release, as part of the data team rather than as a gate at the end.
- Clarify how QA divides the work with the data analysts and data engineers.
- Set out how QA plans and runs the checks for each step of a release.
- Keep every release repeatable and evidenced for handover.

## How QA Works With the Data Team

A data release is delivered by QA working closely with the data analysts and data engineers throughout, not by testing a finished output. The analysts own the source data, the engineers own the pipeline, and QA owns the assurance that the data lands correctly and appears accurately in the service. QA holds the end to end view from source file to what the user sees, which neither the analyst nor the engineer sees on their own.

| QA works with | On | How QA works with them |
| :--- | :--- | :--- |
| **Data Analysts** | Source and ancillary files, schema, business rules | Walk through the incoming files and expected schema before the run; agree what "correct" looks like for the release specific logic; triage anything that looks wrong together, so QA can separate an upstream data issue from a pipeline issue; review completeness gaps and the transparency figures with them. |
| **Data Engineers** | Pipeline execution, logs, database | Agree the run steps and environments; pair on triggering the pipeline and reading the logs; run the reconciliation and regression queries together; debug ingestion failures side by side; confirm the `Parameters` flags at go-live. |
| **Stakeholders / Product Owner** | Acceptance of the data in the service | Agree the UAT scope; walk the service together for the reporting year; capture sign-off and the go/no-go decision. |

## Planning Each Release

QA produces the individual release plan for every release. This is where the approach becomes concrete, and it is done with the team, not in isolation.

- At kickoff QA drafts the release plan from the template in the [Data Release Test Plan](./11_Data-Release-Test-Plan.md), tailored to the release using the variation matrix in [Test Strategy: Data Ingestion](./3_Test-strategy-data-ingestion.md): the right files, the checks that apply, and the release specific business logic.
- QA turns the validation model into concrete steps in the plan, names the owner for each step (analyst, engineer or QA), and agrees the order with the team.
- QA writes and maintains the validation scripts, queries and completeness reports the plan depends on, so they are ready before the run and reusable next cycle.
- QA agrees the cutoff and the `test`, `preprod` and `prod` run points with the team, and books the stakeholders for UAT.

The plan is a shared, living artefact for the release: the team works to it, and QA updates it as steps complete and evidence is captured.

## How QA Engages Across the Release Lifecycle

The release runs in four phases, documented in the [data release guide overview](../guides/data-release-guide/01_Overview.md). QA contributes to each phase as follows.

| Phase | How QA works |
| :--- | :--- |
| Pre-Release | Attend kickoff, agree the plan and owners with the team, walk the incoming files with the analyst, validate the transparency file schema (CFR, AAR), and pair with the engineer on speculative pre-cutoff runs to surface schema drift early. |
| Submission and Cutoff | Agree the cutoff snapshot with the analyst and engineer, and confirm the correct files are landed in the right `raw` year directory. |
| Ingestion and Verification | Run the plan step by step with the team in `test` then `preprod`, pairing with the engineer on the pipeline and queries and with the analyst on the figures, log defects, and drive stakeholder UAT. |
| Promotion and Go-Live | Smoke check each environment with the engineer as data is promoted, and confirm the `Parameters` flags are incremented so the new data shows on the frontend. |

## Running the Checks

During the run QA carries out the validation model defined in the strategy by working through the plan with the team. The point of this section is who QA works with for each check, not what each check covers, which is in the strategy and the plan template.

- QA runs the schema and file checks on the incoming files, raising anything off spec with the analyst before the pipeline runs.
- QA and the engineer trigger the pipeline, watch the logs, and reconcile the database against source totals; QA runs the completeness report and reviews the gaps with the analyst.
- QA verifies the release specific business logic (for example AAR fund apportionment, BFR forecast, S251 budget and outturn) with the analyst who owns the rules.
- QA spot checks the service for the reporting year.
- QA runs the regression checks with the engineer to confirm historical data is untouched, and verifies the transparency file with the analyst where the release has one.
- Where the release refreshes the LAA risk indicators (CFR), QA runs the file based assurance defined in [Test Strategy: Data Ingestion](./3_Test-strategy-data-ingestion.md) with the engineer and analyst.

QA records the outcome of each check against the plan as it goes, so the plan doubles as the evidence trail.

## Reusable QA Assets

QA reuses the same assets each cycle rather than rebuilding them.

- The assurance, reconciliation and coverage queries in the [data release guide](../guides/data-release-guide/01_Overview.md).
- The completeness report script in SharePoint under `Documents > General > Analytics Discovery > Completeness Report - Data Drops`.
- The regression comparison queries over prior years, and the transparency file verification.

## Defect Handling and Evidence

- QA logs defects and QA subtasks in Azure DevOps against the release tickets, giving an audit trail from preparation to go-live, as in the service [Test Approach](./4_Test-Approach.md).
- QA classifies severity with the team; High or Critical issues block the exit criteria and feed the go/no-go decision.
- QA captures evidence for each step (query outputs, completeness reports, transparency and LAA verification, UAT notes) and links it to the release plan so sign-off is traceable.

## Collaboration and Communication Cadence

| Cadence         | Who                                   | Purpose                                             |
|:----------------|:--------------------------------------|:----------------------------------------------------|
| Release kickoff | QA, engineers, analysts, project team | Confirm sources, scope, plan and foreseen blockers  |
| Daily           | Release thread or standup             | Share submission volumes, run progress and blockers |
| Cutoff sync     | QA, analysts, engineers               | Agree and enact the cutoff snapshot                 |
| Sign-off review | QA, stakeholders, tech lead           | Review evidence and take the go/no-go decision      |

Release communication follows the [data release guide](../guides/data-release-guide/01_Overview.md) and [Release Comms](./8_Release-Comms.md).

<!-- Leave the rest of this page blank -->
\newpage
