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
* After configuration, run the pipeline successfully.
* Check that the pipeline has deposited rows in SQL by querying the database tables for BFR (fill in the year):

```sql
SELECT * FROM [dbo].[BudgetForecastReturn] 
WHERE RunId like '<year>' AND RunType like 'default'
```

```sql
SELECT * FROM [dbo].[BudgetForecastReturnMetric]
WHERE RunId like '<year>' AND RunType like 'default'
```

## Check the outputs

To assure the quality of the BFR ingestion pipeline do some spot checks on forecasts and pupil numbers. BFR uses [specific row/line mappings](../../data/sources/bfr.md) (`EFALineNo`) in `BFR_SOFA_raw.csv` and `BFR_3Y_raw.csv` to compute Statement of Financial Activities (SOFA) and multi-year forecasts.

* Select 3 Multi-Academy Trusts in the source CSV and record their values for key `EFALineNo` fields (such as line numbers for total income, staff costs, and total forecasted expenditure).
* Query the `BudgetForecastReturn` and `BudgetForecastReturnMetric` tables for those specific trusts and verify that the financial amounts written correspond exactly to [those line numbers](../../data/sources/bfr.md#efalineno).

### 2. BFR-Specific Pupil Count Validation

BFR uses its own, dedicated forecast pupil number which can differ from the standard School Census headcount. Check the BFR tables have the BFR pupil number in.

## Gotchas

* BFR uses its own pupil number which is different to the census one.
* BFR uses previous year's BFR data to show spend over time. The manner in which it does this and business context [is described in other docs.](../../data/sources/bfr.md)
* [BFR Test Plans](../../quality-assurance/data-release-test-plans/00003_BFR-2024-2025-data-release.md)

\newpage
