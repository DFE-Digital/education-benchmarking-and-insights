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

## Check the outputs

To assure the quality of the CFR ingestion pipeline, specific logical checks targeting Federated Schools must be performed to prevent double-counting.

### 1. Federated School Aggregations & View Exclusions

Maintained schools in a federation submit their financial data under a combined return led by a single "lead" school.

* **The Logic:**
  * The lead school's database record holds the aggregated financial and workforce metrics (e.g., pupil numbers, FTE, total expenditure) for the entire federation.
  * Member schools have individual entries pointing to the lead school's URN, but their financial metrics are empty or non-applicable in the final views to avoid duplicate counts.
* **Checks:**
  * Identify a federation in the raw `maintained_schools_master_list.csv` or by searching for schools with a non-zero "Lead school in federation" LAEstab.
  * Query the database to ensure the lead school contains the aggregated metrics, and member schools have `FederationLeadURN` pointing to the lead school's URN.
  * Execute a validation query against views `030-SchoolsFinancialSummary.sql` and `031-SchoolsWorkforceSummary.sql` to confirm that non-lead federation schools are correctly filtered out using:
    `WHERE s.FederationLeadURN = s.URN -- lead school` OR `s.FederationLeadURN IS NULL -- not federated`
* **SQL Verification Query:**

  ```sql
  -- Checking federation topology and lead school mappings
  SELECT URN, SchoolName, FederationLeadURN, TotalExpenditure 
  FROM [dbo].[School] s
  JOIN [dbo].[Financial] f ON s.URN = f.URN
  WHERE (s.FederationLeadURN = '<LeadURN>' OR s.URN = '<LeadURN>') AND f.RunId = '<year>';
  ```

### 2. Transparency File DNS & Federation Flags Check

* **Checks:**
  * Validate that the CFR Transparency File generator correctly tags member schools with the "DNS" (Did Not Submit) or federated submission status where their financials are combined into the lead school.
  * Check that the sum of the transparency file's school expenditures matches the totals computed on the FBIT database for active lead schools.

### Completeness checks compared to last year

```sql
-- Compare total maintained school counts and coverage percentage of key fields against the previous year's run
SELECT
    f_curr.RunId AS Run,
    Count(f_curr.URN) AS Total_Schools,
    Count(f_curr.KS2Progress) * 100 / Count(*) AS KS2_coverage,
    Count(f_curr.KS4Progress) * 100 / Count(*) AS KS4_coverage,
    Count(f_curr.TotalPupils) * 100 / Count(*) AS Pupils_coverage,
    Count(f_curr.TeachersFTE) * 100 / Count(*) AS Teachers_coverage,
    Count(f_curr.TotalInternalFloorArea) * 100 / Count(*) AS CDC_Floor_coverage,
    Count(f_curr.PercentFreeSchoolMeals) * 100 / Count(*) AS FSM_coverage,
    Count(f_curr.PercentSpecialEducationNeeds) * 100 / Count(*) AS SEN_coverage
FROM [dbo].[NonFinancial] f_curr
WHERE f_curr.RunId = '2025' AND f_curr.RunType = 'default' AND f_curr.FinanceType = 'Maintained'
GROUP BY f_curr.RunId
UNION ALL
SELECT
    f_prev.RunId AS Run,
    Count(f_prev.URN) AS Total_Schools,
    Count(f_prev.KS2Progress) * 100 / Count(*) AS KS2_coverage,
    Count(f_prev.KS4Progress) * 100 / Count(*) AS KS4_coverage,
    Count(f_prev.TotalPupils) * 100 / Count(*) AS Pupils_coverage,
    Count(f_prev.TeachersFTE) * 100 / Count(*) AS Teachers_coverage,
    Count(f_prev.TotalInternalFloorArea) * 100 / Count(*) AS CDC_Floor_coverage,
    Count(f_prev.PercentFreeSchoolMeals) * 100 / Count(*) AS FSM_coverage,
    Count(f_prev.PercentSpecialEducationNeeds) * 100 / Count(*) AS SEN_coverage
FROM [dbo].[NonFinancial] f_prev
WHERE f_prev.RunId = '2024' AND f_prev.RunType = 'default' AND f_prev.FinanceType = 'Maintained'
GROUP BY f_prev.RunId;
```

## Gotchas

* Find all the ancillary data used in the CFR release in `get_cfr_ancillary_data()`.
* Dealing with federations is a complex part of CFR. Do some spot checks on federated maintained schools to check their numbers are being handled correctly.

\newpage
