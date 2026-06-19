# Overview

The FBIT service updates underlying data four times a year: for AAR, BFR, CFR, and S251. Over successive releases the team has established processes for how to plan, communicate, and execute an FBIT data release smoothly from retrospectives run post-release.

## Process notes

### Things to do before the release

* At least 2 sprints before the release date, have a kickoff session with the engineers and project team members. Engineers should run the session. It should run through things like:

  * What data sources are going to be refreshed? Check the [data sources docs.](../../data/02_Sources.md) This page also lists the SQL server locations for the main financial releases.
  * Is any additional data access required to access this data? Most ancillary data is publicly available on gov.uk.
  * Does the team foresee any blockers in the release?

* An output of the kickoff session is tickets for the release preparation and execution on team management software (e.g. Azure DevOps). This provides a timeline and audit trail from the preparations through to the data fully released on FBIT.

* Create a central release thread on Teams to broadcast updates about the release. If specific issues need further discussion, have a dedicated thread for it. Note Teams DMs currently delete after a few weeks - keep relevant discussion off DMs.

* CFR and AAR have an accompanying transparency file. This is released separately to the main FBIT service, but ideally they should align as much as possible. Schema validations of the transparency file and checks between FBIT data and the transparency file should be performed after the kickoff session and before the release.

### Things to do immediately before and during the release

* Daily updates on submission volumes from schools/LAs leading up to the release keeps team focus and opens discussions about when to enact the submission cutoff and run the release.

* After the submission cutoff has been decided, one engineer should use the [test plans](../../quality-assurance/data-release-test-plans/) to orchestrate the rest of the team to release and test the new data. These test plans are UAT guides for manual verification of the new data.

* The submission cutoff is enacted by taking a snapshot of the approved submissions table in SQL as of a decided date. Any submissions made after this date are excluded from the release, even if they appear in SQL shortly after.

* To trigger the pipeline run once the data is prepared, follow the instructions in the [data-pipeline README](../../../data-pipeline/README.md#running-the-pipeline). This involves adding the a message to the `data-pipeline-job-default-start` Azure queue as UTF-8 in this form:

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

## Step-by-step process

Get files > Process files locally > Check outputs locally > Deploy changes to remote > Test changes on remote with product owners > Promote through remote environments.

### What year to use in data pipeline runs

Most of FBIT is not historical. School benchmarking on FBIT shows data for only one year's worth of data. FBIT uses year identifiers to set off data pipeline runs to specify which year of data we want for each of our 4 financial data sources. For example CFR gets released earliest, so to set a data pipeline run with the new CFR data we would increment the CFR year and the runId only:

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

Then when BFR gets released:

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

And so on. The RunId should be the highest of the years of the 4 releases, and is used as a database identifier to retrieve data on the frontend. As an aside: for some parts of FBIT eg Custom Data runs the runId isn't a number - it is still used to retrieve data rows from the database.

The year for a release corresponds to a directory location in the `raw` container. For the pipeline to run correctly the correct data must be in the right directory in the `raw` container.

## Checking for schema changes

* New files in the service will often need code config changes to define their schema for the data pipeline.
* If there are schema changes to last year, or the contents of the files is very different to last year, this should be flagged to the business as part of the data drop report as early as possible. The business will advise on how to handle these changes.
* Schema changes and filenames with dates need to be registered in the data pipeline to process the new data correctly. Conceptually there is a file name, a file schema, and a mapping of raw schema names to regularised pipeline names. All of these may need to be updated to get a new file to run in the pipeline. For example, `data-pipeline/src/pipeline/input_schemas/census_workforce.py` allows per-year configuration of: header rows, file schema, filename, column mappings (renaming inconsistent columns so they can be predictable processed in the pipeline), and a column eval config to define derived columns.
* If a schema is not defined for a year, the data pipeline will error.

### Testing locally

Testing that new data passes the data pipeline as run on a local machine first is a good idea to debug problems before testing it on deployed infrastructure. To do this, familiarity with Docker, Python, and SQL are needed.

* [First set up the data pipeline locally.](../../../data-pipeline/README.md)
* Then put the new data into blob storage locally. The year for a release corresponds to a directory location in the `raw` container. For the pipeline to run correctly the correct data must be in the right directory in the `raw` container.
* Then trigger a pipeline run locally by adding a message to the `data-pipeline-job-default-start` queue as UTF-8.
* You can tell the pipeline is running by debugging messages which display through the running of it. Errors will also display alongside the debugging messages if they occur. If the run is successful, a message will say "Pipeline run successful!"
* Check the results of the pipeline run by querying the SQL database after a run.

### Testing on deployed infrastructure

We have a few sets of deployed infrastructure: `dev` (with resources prefixed with `s198d01` in our Azure cloud), `test` (`s198t01`), `preprod` (`s198p02`), and `prod` (`s198p01`). Test in `test`, then do any second round of testing in `preprod` before the live `prod` environment. The equivalents of the local testing allow you to run, observe, and validate outputs from the data pipeline in the Azure console:

* First scale the database for the relevant environment so that the processing of the data pipeline doesn't prevent other users getting responses from it. Search for `s198t01-sql`, click on the `data` database, click on Settings > Compute and Storage and set the DTUs to 200. Wait for this to take effect.
* Add data to data storage blobs (search eg `s198t01data` in the azure console for the resource)
* Trigger a pipeline run (`data-pipeline-job-default-start` in `s198t01data`)
* View the running of the data pipeline by viewing the container logs. Search for `s198t01-ebis-aiw`, click on logs, and search for "default" in the queries hub. Run the "Recent default pipeline runs" query to see the logs/errors from that pipeline run.
* View the relevant database by connecting to it locally via a connection string, as is stored in keyvault `s198t01-ebis-keyvault`, or get login credentials to SQL from the keyvault and log in on the azure console.
* After a pipeline run, descale the database to what it was before.

## Assuring Pipeline Outputs (General Checks)

Before concluding any data release and updating the `Parameters` table, do some logical checks to guarantee dataset completeness and mathematical correctness.

### 1. Database Row Count & Ingestion Integrity

Compare the total number of records in the raw input files against the number of rows deposited in the target SQL database tables.

* Count total active schools from the primary input file (e.g., `aar.csv` or `maintained_schools_master_list.csv`).
* Verify that this matches the count of rows successfully written in `dbo.Financial` and `dbo.NonFinancial` for the target `RunId`.

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

SQL snippets to help judge data linkage percentages are included in release specific guides.

## Post-deployment

### The "parameters table"/post-release data flags

The `Parameters` table in SQL is very small and looks like the data pipeline intitation json. It is used by the frontend to display which year of financial data is used in benchmarking. After running a new year of data into prod, the year for the data release needs to be incremented in the parameters table to display to users that a new year of data is being shown. With a SQL password from KeyVault, this can be done via the Azure console.

## Release specific notes

* [AAR](./02_AAR.md)
* [CFR](./03_CFR.md)
* [BFR](./04_BFR.md)
* [S251](./05_S251.md)

\newpage
