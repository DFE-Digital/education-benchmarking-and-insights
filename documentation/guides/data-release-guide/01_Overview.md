# Overview

The FBIT service updates underlying data four times a year: for AAR, BFR, CFR, and S251. Over successive releases the team has established processes for how to plan, communicate, and execute an FBIT data release smoothly, drawn from retrospectives run post-release.

Because data drops require making schema adjustments, modifying pipeline configuration files, and debugging code-level transformation issues on local Docker instances, **it is highly advised that an engineer familiar with Python, Docker, and SQL executes the data drop.**

Below is the complete chronological sequence from planning through to go-live, with detailed guidance under each step.

## Release specific notes

* [AAR](./02_AAR.md)
* [CFR](./03_CFR.md)
* [BFR](./04_BFR.md)
* [S251](./05_S251.md)

---

## Phase 1: Pre-Release (At least 2 Sprints Before)

1. **Kickoff Meeting.** Hold a release kickoff session, run by engineers, with the engineering and project teams. Cover:
   * Which data sources are being refreshed — check the [data sources docs](../../data/02_Sources.md). This page also lists the SQL server locations for the main financial releases.
   * Whether any additional data access is required. Most ancillary data is publicly available on gov.uk.
   * Any blockers the team foresees in the release, for example large schema changes.

2. **Task Creation.** Create tracking tickets in Azure DevOps for preparation and pipeline execution. This provides a timeline and audit trail from preparation through to data going live on FBIT.

3. **Establish Teams Communications.** Set up a central release thread on Teams for daily broadcasts. If specific issues need further discussion, open a dedicated thread for that issue. Note that Teams DMs currently delete after a few weeks — keep relevant discussion off DMs.

4. **Ancillary Schema & Linkage Checks.** Check for potential schema drift on the ancillary datasets and begin preparatory validations.
   * CFR and AAR have an accompanying transparency file, released separately to the main FBIT service. It should align with FBIT as much as possible — schema validations of the transparency file, and checks between FBIT data and the transparency file, should be performed after the kickoff session and before the release.

5. **Speculative Pre-Cutoff Runs.** Run early, speculative test iterations of the pipeline on `test` or `dev` environments using draft, pre-cutoff data. Early runs are highly encouraged to flush out formatting errors, schema drift, or core-to-ancillary mapping issues before go-live.

## Phase 2: Submission & Cutoff (1 Sprint Before)

1. **Submission Monitoring.** Track incoming daily school/LA submission volumes. Daily updates leading up to the release keep team focus and open discussion of when to enact the cutoff.

2. **Enact Cutoff Snapshot.** Take a static snapshot of the approved submissions table in SQL as of the decided cutoff date. Any returns submitted after this date are excluded from the release cycle, even if they appear in SQL shortly after.

3. **Prepare Raw Data Directory.** Land the final primary and ancillary files inside the correct year directory within the Azure `raw` container (e.g. `raw/default/2026/...`) using the Azure console's upload file functionality. The pipeline reads from this directory location, so the correct data must be in the right place for the run to work — see [What year to use in data pipeline runs](#what-year-to-use-in-data-pipeline-runs) below.

## Phase 3: Ingestion & Verification (Release Day)

Once the cutoff data is prepared, one engineer should orchestrate the rest of the team through release and testing using the [test plans](../../quality-assurance/data-release-test-plans/) — UAT guides for manual verification of the new data.

1. **Scale Target SQL Database.** Scale the destination Azure SQL database (e.g. `s198t01-sql`) to **200 DTUs** via Settings > Compute and Storage, so the pipeline load doesn't impact the web service or other developers. Wait for this to take effect before proceeding.

2. **Send Start Trigger Message.** Post the UTF-8 trigger message to the `data-pipeline-job-default-start` Azure queue in the target storage account. See [trigger schema](#triggering-a-pipeline-run) below. Full instructions are in the [data-pipeline README](../../../data-pipeline/README.md#running-the-pipeline).

3. **Monitor Logs.** Watch execution logs in the container logs queries hub (`s198t01-ebis-aiw`): search logs, then search "default" in the queries hub and run the "Recent default pipeline runs" query. A successful run displays "Pipeline run successful!"

4. **Execute Assurance & Coverage Queries.** Run the general checklist (row count verification, comparative non-null coverage checks, duplicate constraint checks) and release-specific logic validations — see [Assuring Pipeline Outputs](#assuring-pipeline-outputs-general-checks) below and the [test plan](../../quality-assurance/3_Test-strategy-data-ingestion.md).

5. **Descale SQL Database.** Restore the Azure SQL database DTUs to its original baseline.

## Phase 4: Release Promotion & Go-Live (Go-Live Day)

1. **Promote Environments.** Merge the schema/config pull requests on GitHub, triggering FBIT CI/CD deployment. Promote through `dev` → `test` → `preprod` → `prod`.

2. **Update Parameter Flags.** Update the SQL `dbo.Parameters` table to increment the active year identifiers. This instantly labels the fresh data live on the frontend — see [The Parameters table](#2-data-completeness) below.

---

## Reference

### Triggering a pipeline run

To trigger a pipeline run once data is prepared, add a message to the `data-pipeline-job-default-start` Azure queue as UTF-8, in this form:

```json
{
  "type": "default",
  "runId": <year>,
  "year": {
    "aar": <year>,
    "cfr": <year>,
    "bfr": <year>,
    "s251": <year>
  }
}
```

### What year to use in data pipeline runs

Most of FBIT is not historical — school benchmarking on FBIT shows only one year's worth of data. FBIT uses year identifiers to set off data pipeline runs, specifying which year of data is wanted for each of the 4 financial data sources.

CFR is released earliest, so a pipeline run with new CFR data increments only the CFR year and the `runId`:

```json
{
  "type": "default",
  "runId": 2026,
  "year": {
    "aar": 2025,
    "cfr": 2026,
    "bfr": 2025,
    "s251": 2025
  }
}
```

Then when BFR is released:

```json
{
  "type": "default",
  "runId": 2026,
  "year": {
    "aar": 2025,
    "cfr": 2026,
    "bfr": 2026,
    "s251": 2025
  }
}
```

And so on. `runId` should be the highest of the years of the 4 releases, and is used as a database identifier to retrieve data on the frontend. Aside: for some parts of FBIT, e.g. Custom Data runs, `runId` isn't a number — it's still used to retrieve data rows from the database.

The year for a release corresponds to a directory location in the `raw` container. For the pipeline to run correctly, the correct data must be in the right directory in the `raw` container.

### Checking for schema changes

* New files in the service will often need code config changes to define their schema for the data pipeline.
* If there are schema changes from last year, or the contents of the files are very different to last year, flag this to the business as part of the data drop report as early as possible. The business will advise on how to handle these changes.
* Schema changes and filenames with dates need to be registered in the data pipeline to process the new data correctly. Conceptually there is a file name, a file schema, and a mapping of raw schema names to regularised pipeline names — all of these may need to be updated to get a new file to run in the pipeline. For example, `data-pipeline/src/pipeline/input_schemas/census_workforce.py` allows per-year configuration of: header rows, file schema, filename, column mappings (renaming inconsistent columns for predictable processing), and a column eval config to define derived columns.
* If a schema is not defined for a year, the data pipeline will error.

### Testing locally

Testing that new data passes the data pipeline on a local machine first is a good way to debug problems before testing on deployed infrastructure. Familiarity with Docker, Python, and SQL is needed.

1. [Set up the data pipeline locally.](../../../data-pipeline/README.md)
2. Put the new data into blob storage locally. The year for a release corresponds to a directory location in the `raw` container — the correct data must be in the right directory for the pipeline to run correctly.
3. Trigger a pipeline run locally by adding a message to the `data-pipeline-job-default-start` queue as UTF-8 (see [Triggering a pipeline run](#triggering-a-pipeline-run)).
4. Debugging messages display as the pipeline runs; errors display alongside them if they occur. A successful run shows "Pipeline run successful!"
5. Check the results by querying the SQL database after the run.

### Testing on deployed infrastructure

There are several sets of deployed infrastructure: `dev` (resources prefixed `s198d01`), `test` (`s198t01`), `preprod` (`s198p02`), and `prod` (`s198p01`). Test in `test`, then do a second round in `preprod` before the live `prod` environment.

1. Scale the database for the relevant environment so pipeline processing doesn't prevent other users getting responses. Search for `s198t01-sql`, click the `data` database, go to Settings > Compute and Storage, and set DTUs to 200. Wait for this to take effect.
2. Add data to data storage blobs (search e.g. `s198t01data` in the Azure console).
3. Trigger a pipeline run (`data-pipeline-job-default-start` in `s198t01data`).
4. View the pipeline run via container logs: search `s198t01-ebis-aiw`, click logs, search "default" in the queries hub, and run the "Recent default pipeline runs" query to see logs/errors from that run.
5. View the relevant database by connecting locally via a connection string (stored in keyvault `s198t01-ebis-keyvault`), or get login credentials from the keyvault and log in via the Azure console.
6. After the run, descale the database back to its prior setting.

### Assuring Pipeline Outputs (General Checks)

Before concluding any data release and updating the `Parameters` table, run logical checks to guarantee dataset completeness and mathematical correctness.

### 1. Database Row Count & Ingestion Integrity

Compare the total number of records in the raw input files against the number of rows deposited in the target SQL database tables. After a successful pipeline run, summary-level stats on how many schools are in each dataset are logged by the data pipeline and written to the `CompletedPipelineRun` table in SQL.

* Count total active schools from the primary input file (e.g. `aar.csv` or `maintained_schools_master_list.csv`).
* Verify this matches the count of rows successfully written in `dbo.Financial` and `dbo.NonFinancial` for the target `RunId`.

```sql
-- Financial rows check
SELECT Count(*), RunId FROM [dbo].[Financial] 
WHERE RunId = '<year>' AND RunType = 'default' 
GROUP BY RunId;

-- Non-Financial rows check
SELECT Count(*), RunId FROM [dbo].[NonFinancial] 
WHERE RunId = '<year>' AND RunType = 'default' 
GROUP BY RunId;
```

### 2. Data completeness

SQL snippets to help judge data linkage percentages are included in release-specific guides.

### The "Parameters table" / post-release data flags

The `Parameters` table in SQL is very small and structured like the data pipeline initiation JSON. The frontend uses it to display which year of financial data is shown in benchmarking. After running a new year of data into `prod`, increment the year for the release in the Parameters table to display the new data to users. With a SQL password from KeyVault, this can be done via the Azure console.
