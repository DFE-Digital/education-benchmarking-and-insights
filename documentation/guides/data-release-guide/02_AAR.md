# AAR Specific notes

## Get the data

[AAR](https://www.gov.uk/guidance/academies-accounts-return) is academy returns data on a academy and trust level. Academies and trusts ("central services") are different types of organisation, so submit their returns separately. They submit in a big online form: AAR. These returns get ingested into an operational database in DfE behind their forms service, then gradually get approved by DfE and filter through to SQL views which we consume in FBIT. Schools have a deadline to submit their returns and not all schools submit at the same time, so product owners in FBIT make a call as to when enough schools have submitted, and that frozen "cut" of the submission tables is what gets used in FBIT. FBIT combines this financial with other data sources in the data pipeline. AAR uses inputs from the [data sources page.](../../data/02_Sources.md) The data sources page links to where to source these files:

* aar.csv
* aar_cs.csv
* gias.csv
* gias_links.csv
* census_pupils.csv
* census_workforce.csv
* sen.csv
* sen2_estab_caseload.csv
* cdc.csv
* ks2.csv
* ks4.csv
* cfo.csv
* HExP.csv
* ILR R06 cut with FSM and EHCP.xlsx

Download all of these data, checking for updates in publicly available datasets. Use the latest data.

## Test the data pipeline runs locally

* [Set up the pipeline locally](./01_Overview.md#testing-locally).
* Add the new data to the relevant year folder in Azure
* [Configure the schemas for the new files](./01_Overview.md#checking-for-schema-changes) in `data-pipeline.src.pipeline.input_schemas`
* Run the pipeline to test the new data. Debug and fix any issues, eg misconfigured schemas.
* After configuration, run the pipeline successfully
* Check that the pipeline has deposited rows in SQL by querying the database tables for academies and trusts (fill in the year):

```sql
SELECT * FROM [dbo].[Financial] WHERE RunId like '<year>' AND RunType like 'default'
```

```sql
SELECT * FROM [dbo].[NonFinancial] WHERE RunId like '<year>' AND RunType like 'default'
```

```sql
SELECT * FROM [dbo].[TrustFinancial] WHERE RunId like '<year>' AND RunType like 'default'
```

## Generate the transparency file

* AAR has a transparency file which is released alongside the AAR data. This is created through a separate set of [SQL scripts on sharepoint.](https://educationgovuk.sharepoint.com/:f:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/CFR%20Data%20Procurement%20Process/24-25/aar?csf=1&web=1&e=ahBZZC) These scripts have the raw AAR data as inputs.
* The transparency file has some extra features compared to the outputs on FBIT, but it should line up with the numbers in FBIT. Take 5 random schools and check that where the columns overlap, they are the same in the transparency file and in FBIT. Start conversations with the business early about if any schema changes to the transparency file are required.

## Check the outputs

## Deploy the working code through deployment environments

* Open a pull request on GitHub with the schema changes required to run the new year's data.
* FBIT CICD will deploy these changes to test when they are merged. Follow the [guide to test on deployed infrastructure](./01_Overview.md#testing-on-deployed-infrastructure) to run the changes into test for wider testing.

## Gotchas + tips

* Find all the ancillary data used in the AAR release in `get_aar_ancillary_data()`.
* SQL views for AAR submissions update midmorning on weekdays.
* A specific thing to check thoroughly in AAR is the system to apportion central service spend to academies. [There are a few docs on this at a business level](../../data/09_Academy-Apportionments.md). Manually check these figures for at least 1 school/trust.
* The old and new building data collections CDC and CDC2 are combined to the file used in FBIT using [this python file.](https://educationgovuk.sharepoint.com/:u:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Source%20Data/CDC/2025/cdc_combination.py?csf=1&web=1&e=AuQFas). DfE provide us with a cut of CDC2
* The High Exec Pay file can be generated from AAR data in SQL Server using [this sql query](https://educationgovuk.sharepoint.com/:u:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Source%20Data/High%20Exec%20Pay%20query/high_exec_pay_query.sql?csf=1&web=1&e=K11UI5).

\newpage
