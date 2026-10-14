---
title: "LAA Risk Scores"
layout: sub-navigation
sectionKey: "How-to guides"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "LAA Release"
  parent: "Data Release Guide"
---

The Local Authority Analysis (LAA) risk scores pipeline assesses maintained schools across financial, educational performance, and school characteristic indicators. LAA scores are computed alongside the annual Consistent Financial Reporting (CFR) data release.

For conceptual background and indicator definitions, see the [LAA Risk Scores Explanation](/explanation/laa-risk-indicators/).

## Get the Data

The LAA calculations combine data from the primary CFR pre-processed dataset with four external non-financial ancillary sources:

1. **Pre-processed CFR Returns:**
   - Located in the `pre-processed` blob container under `default/<year>/maintained_schools.parquet`.
   - Historical parquets for the previous 4 years must exist in their respective year folders (e.g. `default/<year-1>/maintained_schools.parquet`, etc.).
2. **External Ancillary Datasets:**
   - Placed in the `raw` container under the current reporting year folder (e.g. `raw/default/<year>/` or `raw/<year>/`):
     - **School Capacity:** Net capacity and school places (`capacity_school_*.csv`).
     - **Specialist Provision Capacity:** Capacity figures for special provisions (`specialist-provision_school_*.csv`).
     - **Pupil Absence:** Three-term overall pupil absence (`1a_Absence_3term_school.csv`).
     - **Parental Preference:** Applications and offers school-level data (`AppsandOffers_*_SchoolLevel*.csv`).

Note at the time of CFR release, new data for Capacity and Absences is not released. This is accounted for - use last year's data until it becomes available.

## Pre-Release Configuration & Annual Checks

Before triggering the pipeline with new data, review `data-pipeline/src/pipeline/laa_risk_scores/config.py`:

### 1. Ancillary Publication Lag

National datasets for school capacity lag behind the CFR reporting year for the start of the CFR cycle.

If the dataset lags behind the primary run period, register the dataset name in `ancillary_data_lagging_cfr_1y` for the target year.

### 2. File Mappings and Columns

- Confirm that file names match the entries in `laa_ancillary_files` for the target year.
- Verify that column schemas in `laa_ancillary_columns` match the headers of the newly supplied raw CSV files (e.g. `school_urn`, `school_places`, `sess_overall_percent`, `times_put_as_1st_preference`, `total_number_places_offered`).

### 3. Metric Year Suffixes

- The pipeline dynamically affixes academic year codes to lagging and prior-year indicators. Verify that `get_academic_year_code()` produces the correct formatted period codes for the new run year.

---

## Running the Pipeline

### Option A: Automated Execution with CFR (Recommended)

Include the `"deriveLaaRiskScores": true` flag in the default pipeline trigger message posted to the `data-pipeline-job-pending` queue:

```json
{
  "type": "default",
  "runId": "<year>",
  "year": {
    "aar": "<prior_year>",
    "cfr": "<year>",
    "bfr": "<prior_year>",
    "s251": "<prior_year>"
  },
  "deriveLaaRiskScores": true
}
```

When this flag is `true`, the pipeline automatically executes LAA risk derivations immediately after standard CFR and benchmarking processing finish.

### Option B: Local Testing

To run or debug LAA calculations locally in development or Docker:

1. Ensure the pre-processed `maintained_schools.parquet` files for the current year and 4 prior years exist in local storage.
2. Trigger the pipeline locally following the instructions in the [data pipeline documentation](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/data-pipeline/README.md#running-the-pipeline-locally).

---

## Check the Outputs

Once the pipeline completes, verify that the data has been correctly generated and deposited into SQL and blob storage.

### 1. Check the database tables have been populated

Ensure both denormalised tables have been populated:

```sql
-- Check total indicator records
SELECT COUNT(*) AS IndicatorRows
FROM [dbo].[LASchoolRiskIndicators]
WHERE RunId = '<year>';

-- Check total school summary records
SELECT COUNT(*) AS SchoolHeaderRows
FROM [dbo].[LASchoolRiskIndicatorsHeaders]
WHERE RunId = '<year>';
```

The header count should align with the total number of active, non-federated (or lead federated) maintained schools for the reporting year.

### 2. Check federations have filtered out

Non-lead members of a federation must be excluded from LAA scoring to prevent duplicated or unrepresentative figures. Confirm that no non-lead federated schools are present in the headers table:

```sql
SELECT h.URN, s.SchoolName, s.FederationLeadURN
FROM [dbo].[LASchoolRiskIndicatorsHeaders] h
JOIN [dbo].[School] s ON h.URN = s.URN
WHERE h.RunId = '<year>'
  AND s.FederationLeadURN IS NOT NULL
  AND s.FederationLeadURN != s.URN;
```

This query should return **0 rows**.

### 3. Grade distribution sanity check

Verify that overall grades (A\* through G) are distributed plausibly across the school cohort:

```sql
SELECT OverallGrade, COUNT(*) AS SchoolCount
FROM [dbo].[LASchoolRiskIndicatorsHeaders]
WHERE RunId = '<year>'
GROUP BY OverallGrade
ORDER BY OverallGrade;
```

Verify that the risk indicator flags are distributed plausibly:

```sql
SELECT COUNT(*) Indicators, RiskGroup, RiskIndicator, RiskIndicatorFlag
FROM LASchoolRiskIndicators
WHERE RunId LIKE '<year>'
GROUP BY RiskGroup, RiskIndicator, RiskIndicatorFlag
ORDER BY RiskGroup, RiskIndicator, RiskIndicatorFlag
```

> **Warning:** If nearly all schools receive grade **G**, this typically indicates a missing input column or an unmapped join that caused metrics to default to their maximum risk penalty.

### 4. Non-Applicable vs Missing Data Checks

Verify that phase-inapplicable schools are correctly identified with `"N/A"` and no flag, rather than being penalised for missing data:

```sql
-- Confirm Special schools receive 'N/A' and 'No flag' on capacity
SELECT TOP 10 URN, RiskIndicator, RiskIndicatorValue, RiskIndicatorFlag, RiskIndicatorContribution
FROM [dbo].[LASchoolRiskIndicators]
WHERE RunId = '<year>'
  AND RiskIndicator = '% capacity'
  AND RiskIndicatorValue = 'N/A';
```

### 5. Download Artifact Verification

Verify that the comprehensive school-level export file has been written to the `artifacts` storage container:

- Path: `artifacts/default/<year>/laa_risk_scores_download.csv`
- Ensure the file is non-empty and contains both raw data inputs and derived score/flag columns.

---

## Gotchas

- **Historical Parquet Missing:** If any of the prior 4 years of `maintained_schools.parquet` are missing from the `pre-processed` container, the pipeline will raise a `ResourceNotFoundError` and halt.
- **All-Through Schools in Ancillary Data:** Some national capacity and admissions returns split all-through schools across primary and secondary phases. The preprocessing routine aggregates these records by URN; ensure join keys remain on `school_urn`.
- **Penalty Stance on Missing Data:** For schools where an indicator *is* applicable, missing data points incur the maximum risk score and maximum flag. Always inspect unexpected spikes in major flags against input data completeness.
