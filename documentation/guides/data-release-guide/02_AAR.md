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

Download all of these data, checking for updates in publicly available datasets. Use the latest data. AAR column codes have changed since we first ran the data and there is a small pivot we do from the SQL data which renames some columns and prepares the data for the data pipeline. [The script to do this is stored on sharepoint here.](https://educationgovuk.sharepoint.com/:u:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/CFR%20Data%20Procurement%20Process/24-25/aar/Academies_Returns_202425.sql?csf=1&web=1&e=gzNOTL)

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
* **Checks:**
  * Select 5 random schools representing different categories (e.g., small, large, federated, or multi-academy trust members).
  * Manually cross-check their overlapping metrics between the transparency file and the database outputs to verify they are the same.

## Check the outputs

### Completeness checks compared to last year

These checks check the volume of submissions, and how well non-financial data links to this year's submissions.

```sql
-- Compare total academy counts and coverage percentage of key fields against the previous year's run
SELECT
    f_curr.RunId AS Run,
    Count(f_curr.URN) AS Total_Schools,
    Count(f_curr.KS2Progress) * 100 / Count(*) AS KS2_coverage,
    Count(f_curr.KS4Progress) * 100 / Count(*) AS KS4_coverage,
    Count(f_curr.TotalPupils) * 100 / Count(*) AS Pupils_coverage,
    Count(f_curr.TeachersFTE) * 100 / Count(*) AS Teachers_coverage,
    Count(f_curr.TotalInternalFloorArea) * 100 / Count(*) AS CDC_Floor_coverage,
    Count(f_curr.BuildingAverageAge) * 100 / Count(*) AS CDC_Age_coverage,
    Count(f_curr.PercentFreeSchoolMeals) * 100 / Count(*) AS FSM_coverage,
    Count(f_curr.PercentSpecialEducationNeeds) * 100 / Count(*) AS SEN_coverage,
    Count(f_curr.PercentTeacherWithQualifiedStatus) * 100 / Count(*) AS Qualified_Teachers_coverage
FROM [dbo].[NonFinancial] f_curr
WHERE f_curr.RunId = '2025' AND f_curr.RunType = 'default' AND f_curr.FinanceType = 'Academy'
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
    Count(f_prev.BuildingAverageAge) * 100 / Count(*) AS CDC_Age_coverage,
    Count(f_prev.PercentFreeSchoolMeals) * 100 / Count(*) AS FSM_coverage,
    Count(f_prev.PercentSpecialEducationNeeds) * 100 / Count(*) AS SEN_coverage,
    Count(f_prev.PercentTeacherWithQualifiedStatus) * 100 / Count(*) AS Qualified_Teachers_coverage
FROM [dbo].[NonFinancial] f_prev
WHERE f_prev.RunId = '2024' AND f_prev.RunType = 'default' AND f_prev.FinanceType = 'Academy'
GROUP BY f_prev.RunId;
```

### Apportionments and part-year schools

The most complicated checks in AAR are on on [academy central service apportionments](./../../data/09_Academy-Apportionments.md), and how this is affected by trust leavers and joiners.

Except for revenue reserves, trust-level central expenditure is distributed (apportioned) to member academies at the start of the year (even if it left part way through the year). Most cost categories are distributed using pupil numbers, but a couple of categories like utilities are distributed using floor area. There are three categories to check: cost apportionments by pupil number, cost apportionments by floor area, and the special case of trust revenue reserves. The idea here is to recalculate apportionments from the input data to check against the database values as calculated by the data pipeline.

* **Checks:**
  * Select a Multi-Academy Trust (MAT) with a significant central expenditure balance in the `aar_cs.csv` input. Take school from that trust and get it's URN to plug into the query below. Also replace the year with the current year's data.
  * Verify that the apportioned expenditure figures written into the database for each academy map exactly to this proportion of the trust's total central services cost. For the query below, `ApportionedTeachingStaffCostsCS` should equal `RecalculatedTeachingApportionment`, and the same with utilities.

```sql
SELECT f.[URN]
      ,f.[TeachingStaffCostsCS] as ApportionedTeachingStaffCostsCS
      ,f.[TotalUtilitiesCostsCS] as ApportionedUtilitiesCostsCS
      ,f.RevenueReserve
      ,tf.TeachingStaffCostsCS * (f.[TotalPupils] * f.PeriodCoveredByReturn / 12) / prt.TrustProRataPupilTotal AS RecalculatedTeachingApportionment
      ,tf.TotalUtilitiesCostsCS * (f.[TotalInternalFloorArea] * f.PeriodCoveredByReturn / 12) / prt.TrustProRataFloorAreaTotal AS RecaculatedUtilityApportionment
  FROM [dbo].[Financial] f
  -- get trust company number
  LEFT JOIN dbo.School s ON f.URN = s.URN
  -- pro-rata trust totals
  LEFT JOIN (
    SELECT s.TrustCompanyNumber
    ,SUM(f.TotalPupils * f.PeriodCoveredByReturn / 12) as TrustProRataPupilTotal
    ,SUM(f.TotalInternalFloorArea * f.PeriodCoveredByReturn / 12) as TrustProRataFloorAreaTotal
    FROM dbo.Financial f
    LEFT JOIN dbo.School s ON f.URN = s.URN
    WHERE f.RunId like '2025'
    AND f.RunType like 'default'
    AND f.FinanceType like 'academy'
    GROUP BY s.TrustCompanyNumber
  ) prt ON prt.TrustCompanyNumber = s.TrustCompanyNumber
  -- get trust CS totals
  LEFT JOIN dbo.TrustFinancial tf on s.TrustCompanyNumber = tf.CompanyNumber
  WHERE f.RunId like '2025'
  AND f.RunType like 'default'
  AND f.URN like 131065
  AND tf.RunId like '2025'
  AND tf.RunType like 'default'
```

### Trust Revenue Reserves Check

Because Revenue Reserves represent balance sheet items at a specific point in time (the year-end), they are only apportioned to academies that are active, current members of a trust at year-end (based on trust pupil numbers at year end). Academies that left or closed during the year must receive exactly `0.0` for their apportioned revenue reserve.

* **Check:** Take a trust from the CS file, and note it's revenue reserves. Take a member academy from `aar.csv` and calculate it's apportioned revenue reserve based on which academies are members of the trust at the end of the year (revenue reserve + central revenue reserve * academy pupils / end of year trust pupils). This should line up with the revenue reserve field in `Financial`.

## Deploy the working code through deployment environments

* Open a pull request on GitHub with the schema changes required to run the new year's data.
* FBIT CICD will deploy these changes to test when they are merged. Follow the [guide to test on deployed infrastructure](./01_Overview.md#testing-on-deployed-infrastructure) to run the changes into test for wider testing.

## Gotchas + tips

* Find all the ancillary data used in the AAR release in `get_aar_ancillary_data()`.
* SQL views for AAR submissions update midmorning on weekdays.
* A specific thing to check thoroughly in AAR is the system to apportion central service spend to academies. [There are a few docs on this at a business level](../../data/09_Academy-Apportionments.md). Manually check these figures for at least 1 school/trust.
* The old and new building data collections CDC and CDC2 are combined to the file used in FBIT using [this python file](https://educationgovuk.sharepoint.com/:u:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Source%20Data/CDC/2025/cdc_combination.py?csf=1&web=1&e=AuQFas). DfE provide us with a cut of CDC2
* The High Exec Pay file can be generated from AAR data in SQL Server using [this sql query](https://educationgovuk.sharepoint.com/:u:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Source%20Data/High%20Exec%20Pay%20query/high_exec_pay_query.sql?csf=1&web=1&e=K11UI5).
* [Test plans for AAR](../../quality-assurance/data-release-test-plans/00001_AAR-2024-2025-data-release.md)

\newpage
