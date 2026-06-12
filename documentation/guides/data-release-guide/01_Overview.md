# Overview

The FBIT service updates underlying data four times a year: for AAR, BFR, CFR, and S251. Over successive releases the team has established processes for how to plan, communicate, and execute an FBIT data release smoothly from retrospectives run post-release.

## Things to do before the release

* At least 2 sprints before the release date, have a kickoff session with the engineers and project team members. Engineers should run the session. It should run through things like:

  * What data sources are going to be refreshed? Check the [data sources docs.](../../data/02_Sources.md) This page also lists the SQL server locations for the main financial releases.
  * Is any additional data access is required to access this data? Most ancillary data is publically available on gov.uk.
  * Does the team foresee any blockers in the release?

* An output of the kickoff session is tickets for the release preparation and execution on team management software (e.g. Azure DevOps). This provides a timeline and audit trail from the preparations through to the data fully released on FBIT.

* Create a central release thread on Teams to broadcast updates about the release. If specific issues need further discussion, have a dedicated thread for it. Note Teams DMs currently delete after a few weeks - keep relevant discussion off DMs.

* CFR and AAR have an accompanying transparency file. This is released separately to the main FBIT service, but ideally they should align as much as possible. Schema validations of the transparency file and checks between FBIT data and the transparency file should performed after the kickoff session and before the release.

## Things to do immediately before and during the release

* Daily updates on submission volumes from schools/LAs leading up to the release keeps team focus and opens discussions about when to enact the submission cutoff and run the release.

* After the submission cutoff has been decided, one engineer should use the [test plans](../../quality-assurance/data-release-test-plans/) to orchestrate the rest of the team to release and test the new data. These test plans are UAT guides for manual verification of the new data.

* The submission cutoff is enacted by taking a snapshot of the approved submissions table in SQL as of a decided date. Any submissions made after this date are excluded from the release, even if they appear in SQL shortly after.

* To trigger the pipeline run once the data is prepared, follow the instructions in the [data-pipeline README](../../../data-pipeline/README.md#running-the-pipeline).

## Release specific notes

* [AAR](./02_AAR.md)
* [CFR](./03_CFR.md)
* [BFR](./04_BFR.md)
* [S251](./05_S251.md)

\newpage
