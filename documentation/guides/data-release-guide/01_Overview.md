# Overview

The FBIT service updates underlying data four times a year: for AAR, BFR, CFR, and S251. Over successive releases the team has established processes for how to plan, communicate, and execute an FBIT data release smoothly from retrospectives run post-release.

## Things to do before the release

* At least 2 sprints before the release date, have a kickoff session with the engineers and project team members. Engineers should run the session. It should run through things like:

  * What data sources are going to be refeshed?
  * Is any additional data access is required to access this data?
  * Does the team foresee any blockers in the release?

* An output of the kickoff session is the release preparation and execution ticketed up on team management software (e.g. Azure DevOps). This provides an audit trail from the preparations through to the data fully released on FBIT.

* Create a central release thread on Teams to broadcast updates about the release. If specific issues need further discussion, have a dedicated thread for it. Note Teams DMs currently delete after a few weeks - keep relevant discussion off DMs.

* CFR and AAR have an accompanying transparency file. This is released separately to the main FBIT service, but ideally they should align as much as possible. Schema validations of the transparency file and checks between FBIT data and the transparency file should rechecked after the kickoff session and before the release.

## Things to do immediately before and during the release

* Daily updates on submission volumes from schools/LAs leading up to the release keeps team focus and opens discussions about when to enact the submission cutoff and run the release.

* After the submission cutoff has been decided, one engineer should use the test plan to orchestrate the rest of the team to release the new data.

\newpage
