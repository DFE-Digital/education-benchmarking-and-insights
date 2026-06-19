# BFR Specific notes

BFR data is used for the forecast and risk tool. It doesn't need any ancillary data and just needs to be run through the data pipeline directly from the SQL tables. Increment the `bfr_year` when starting the data pipeline and place the new data in the new year's folder in blob storage.

## Get the data

BFR data is simply exported from the SQL tables outlined in the [data sources page.](../../data/02_Sources.md) The files are:

* BFR_3Y_raw.csv
* BFR_SOFA_raw.csv

## Test the data pipeline runs locally

* [Set up the pipeline locally](./01_Overview.md#testing-locally).
* Add the new data to the relevant year folder in Azure
* [Configure the schemas for the new files](./01_Overview.md#checking-for-schema-changes) in `data-pipeline.src.pipeline.input_schemas.bfr`. For BFR, it may not be necessary.
* Run the pipeline to test the new data. Debug and fix any issues, eg misconfigured schemas.
* After configuration, run the pipeline successfully
* Check that the pipeline has deposited rows in SQL by querying the database tables for BFR (fill in the year):

```sql
SELECT * FROM [dbo].[BudgetForecastReturn] 
WHERE RunId like '<year>' AND RunType like 'default'
```

```sql
SELECT * FROM [dbo].[BudgetForecastReturnMetric]
WHERE RunId like '<year>' AND RunType like 'default'
```

## Gotchas

* BFR uses its own pupil number which is different to the census one.
* BFR uses previous year's BFR data to show spend over time. The manner in which it does this and business context [is described in other docs.](../../data/sources/bfr.md)

\newpage
