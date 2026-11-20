# Transparency file publication strategy following pipeline generation (0024)

Date: 2026-07-21

## Status

Draft

## Context and Problem Statement

The transparency file was previously produced by an independent script, checked, and then published to the service manually. Because it was decoupled from the service, the published file stayed fixed even when corrections were later made to FBIT. The transparency file includes an index tab that records its version and what has been updated, and that index tab was also maintained by hand as part of the manual publish.

The transparency file is now produced by the FBIT data pipeline, and it is regenerated on every pipeline run. The pipeline can produce the file, but it cannot know on its own whether a given run is meant to publish a new version, nor what the index tab should say. The pipeline runs for many reasons, including full data releases, corrections, re-runs, and speculative test runs, so automatically publishing on every run would expose unverified files and churn the version history.

The team therefore needs to decide two things:

1. How the version and change summary for the index tab are supplied to the pipeline.
2. At what stage a generated file is committed to storage and made the live, downloadable version.

The options are assessed against the following criteria:

1. **Data assurance:** does it preserve a check before a file is exposed to users?
2. **Intentionality:** does publishing stay a deliberate act, so incidental or corrective runs do not silently change the live file?
3. **Version integrity:** is the index tab, version, and change summary populated consistently and traceably?
4. **Automation and effort:** how much manual, error-prone work does it remove?

## Considered Options

1. Keep publishing manual (status quo).
2. Optional publish message on the pipeline trigger drives an automatic publish.
3. Always generate to a staging location every run, with a separate deliberate promotion step.
4. Fully automatic publish on every pipeline run.

The sequence below illustrates the mechanism common to the message-driven options (2 and 3): the file is generated on every run, an optional publish message decides whether it is also staged for release, and activation on the live service remains the existing deliberate go-live step.

### Option 1: Keep publishing manual (status quo)

The pipeline generates the file, and a person then curates the workbook, updates the index tab and version, uploads it to storage, and updates the version index by hand.

* Good, because it preserves the existing check-before-publish gate with no change to current governance.
* Good, because publishing stays fully intentional, so corrections and test runs never touch the live file.
* Good, because it requires no engineering work to deliver.
* Bad, because the manual steps (editing the index tab, uploading, updating the version index) are repetitive and error-prone.
* Bad, because the version and change summary depend on a person remembering to update them consistently.

### Option 2: Optional publish message on the pipeline trigger

The pipeline trigger message gains an optional publish block (a version label and a change summary). When present, the pipeline builds the workbook including a populated index tab from that message, writes it to the download storage, and creates or updates the version index row so the new version becomes available. When absent, the file is generated only and nothing is published.

* Good, because publishing stays deliberate: supplying the message is an explicit act of intent, so corrective and test runs that omit it never publish.
* Good, because it removes the manual upload and index-update steps and reduces the chance of a mismatch between the file and its index row.
* Good, because the index tab, version, and change summary come from one structured input and are populated consistently.
* Neutral, because it adds a small schema change to the trigger message and pipeline logic to parse and apply it.
* Bad, because if the publish targets the live version immediately, it can bypass the existing verification and promotion path unless a safeguard is added.
* Bad, because a mistyped message could publish an incorrect version, so message validation and review are needed.

### Option 3: Always generate to staging, promote separately

Every run always writes the produced workbook to a staging location as a versioned artefact. Promotion to the live download storage and activation of the version index remains a separate, deliberate action (a manual approval or a dedicated publish step or flag).

* Good, because it cleanly separates producing the file from releasing it, keeping a strong verification gate.
* Good, because every run leaves a checkable artefact, improving traceability and making rollback to a prior file easy.
* Good, because promotion can be aligned exactly with the existing go-live flow.
* Neutral, because the index tab and version can be populated at generation or at promotion, which is a further sub-decision.
* Bad, because the promotion step is still manual unless automated, so it adds more manual effort than Option 2.
* Bad, because it introduces a staging concept and storage lifecycle that must be built and maintained.

### Option 4: Fully automatic publish on every run

Every run regenerates the file and immediately overwrites the live download and the active version index row, with the version derived automatically (for example from the run year).

* Good, because it is the most automated and removes all manual publishing effort.
* Good, because the published file can never lag behind the latest pipeline output.
* Bad, because it removes the check-before-publish gate entirely, so unverified or corrective runs go live immediately.
* Bad, because test, speculative, and re-run executions would churn or corrupt the live version.
* Bad, because the index tab and version become meaningless without a human-authored change summary.
* Bad, because consumers who downloaded a given version could find it silently changed underneath them.

## Decision Outcome

To be decided. The team will review the options above and record the chosen option and rationale here.

## Analysis of Alternatives

| Option | Notes for the decision |
| :--- | :--- |
| **Keep publishing manual (Opt 1)** | Safe and needs no build work, but retains error-prone manual steps and duplicates effort now that the pipeline produces the file. |
| **Optional publish message (Opt 2)** | Removes manual effort while keeping publishing intentional. Needs a trigger message schema change and a safeguard if it publishes live immediately. |
| **Generate to staging, promote separately (Opt 3)** | Strongest assurance, but leaves promotion manual unless further automated. Can be combined with Opt 2 as a staging safeguard. |
| **Fully automatic publish (Opt 4)** | Highest automation but removes the assurance gate and breaks version integrity for corrective and test runs. |

## Risks and Required Actions

### Risks

* **Accidental publish:** an incorrect or careless publish instruction could expose a wrong version. Mitigate with validation and review of the trigger for production runs.
* **Bypassing the release gate:** immediate live publishing could skip the existing verification and promotion path. Mitigate by staging the version index row (inactive or future-dated) and activating it at the established go-live step.
* **Index tab drift:** if the version and change summary are hand-entered, they can still be inconsistent. Mitigate with a defined message format and a generated default.

### Required Actions

1. Agree the chosen option and record it under Decision Outcome.
2. If a publish message is adopted, define its schema (version label, change summary) and where it is validated.
3. Update the data-release guide and the relevant test plans to reflect the agreed publish mechanism.

<!-- Leave the rest of this page blank -->
\newpage
