# CFR Specific notes

CFR has a transparency file which is released alongside the CFR data.

## Get the data

[CFR](https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr) is the framework by which local authority schools submit their data to DfE. Data is submitted via an online form, as in AAR. Local authorities do their own checks on CFR data as they are responsible for funding LA schools, in contrast to academies who are funded directly from DfE. Schools have a deadline to submit their returns and not all schools submit at the same time, so product owners in FBIT make a call as to when enough schools have submitted, and that frozen "cut" of the submission tables is what gets used in FBIT. FBIT combines this financial with other data sources in the data pipeline. CFR uses inputs from the [data sources page.](../../data/02_Sources.md) The data sources page links to where to source these files:

* CFR_24-25_Data.csv
* gias.csv
* gias_links.csv
* census_pupils.csv
* census_workforce.csv
* sen.csv
* sen2_estab_caseload.csv
* cdc.csv
* ks2.csv
* ks4.csv
* ILR R06 cut with FSM and EHCP.xlsx
* hospital_schools.csv
* pru.csv
* lookup_la.csv

## Test the data pipeline runs locally

* [Set up the pipeline locally](./01_Overview.md#testing-locally).
* Add the new data to the relevant year folder in Azure
* [Configure the schemas for the new files](./01_Overview.md#checking-for-schema-changes) in `data-pipeline.src.pipeline.input_schemas`
* Run the pipeline to test the new data. Debug and fix any issues, eg misconfigured schemas.
* After configuration, run the pipeline successfully
* Check that the pipeline has deposited rows in SQL by querying the database tables for schools (fill in the year):

```sql
SELECT * FROM [dbo].[Financial] WHERE RunId like '<year>' AND RunType like 'default'
```

```sql
SELECT * FROM [dbo].[NonFinancial] WHERE RunId like '<year>' AND RunType like 'default'
```

## Gotchas

* Find all the ancillary data used in the CFR release in `get_cfr_ancillary_data()`.
* Dealing with federations is a complex part of CFR. Do some spot checks on federated maintained schools to check their numbers are being handled correctly.

\newpage
