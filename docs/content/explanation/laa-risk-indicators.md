---
title: "Local Authority Analysis (LAA) Risk Scores"
layout: sub-navigation
sectionKey: "Explanation"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Local Authority Analysis (LAA) Risk Scores"
  parent: Explanation
---

## Historical business context

Pre late 2026 a tool external to FBIT called the Local Authority Risk Analysis Tool (LARAT) used a variety of financial and nonfinanial datapoints to score LA schools on various risk factors. These individual risk factors combined to create a headline risk score for each school.

In late 2026, this tool was determined to merge into FBIT. This makes sense as most of the datapoints used to score school risk were available in FBIT.

---

## Structure of LAA Risk Scores

The LAA risk scoring engine calculates individual indicators across three main categories, tallies flags and contributions, and assigns an overall risk grade (from **A\*** to **G**) to each maintained school.

### Categories of Risk Score

Indicators are organised into three distinct risk groups:

1. **Financial**: Evaluates solvency, revenue reserves, in-year balance trajectory, long-term surplus or deficit patterns, borrowing liabilities, and proportion of spend committed to staff and premises.
2. **School Characteristics**: Evaluates capacity utilisation, pupil roll volatility over 1 and 4 year horizons, 6th form viability, and overall pupil absence rates.
3. **Educational Performance**: Evaluates parental demand (admissions preference) alongside Key Stage 2 and Key Stage 4 attainment and progress measures.

Each category contributes towards the overall risk score, with Financial indicators representing the largest component.

## Individual Risk Indicators

Individual metrics each have:

- **A Raw Value (`RiskIndicatorValue`)**: The metric value serialised as a string.
- **Risk Score Contribution (`RiskIndicatorContribution`)**: The risk score's contribution to the risk group score.
- **Risk Flag (`RiskIndicatorFlag`)**: Categorical indicator of concern (`No flag`, `Minor`, or `Major`).

### 1. Financial Indicators

| Indicator Name | Description / Formula | Value Type | Max Flag |
| :--- | :--- | :---: | :---: |
| **Current revenue reserve as % income** | Current year `Revenue reserve / Total Income`. Penalises negative reserves (deficits), escalating from minor to major flags as deficit size increases. Labelled with the current academic year. | Percentage | Major |
| **Previous revenue reserve as % income** | Previous year `Revenue reserve / Total Income`. Assesses whether the school had an existing deficit in the prior reporting year. Labelled with the previous academic year. | Percentage | Major |
| **School has loan** | Checks whether `Other costs_Interest charges for loan and bank > 0`. Flagged if loan interest charges are present. | Boolean | Minor |
| **% spend on premises** | `LAAPremisesExpenditureRollup / NetExpenditure`. Flags when premises expenditure exceeds expected proportions of net spending. | Percentage | Minor |
| **% expenditure on staff** | `LAAStaffExpenditureRollup / NetExpenditure`. Flags when staff costs absorb an excessively high proportion of net spending. | Percentage | Minor |
| **Change in spend over 4 years as % of income** | `(Total Expenditure - Total Expenditure [Y-4]) / Total Income`. Flags substantial expenditure growth over a 4-year period relative to income. | Percentage | Major |
| **Current long term balance deficit** | Evaluates whether the school operated with a negative revenue reserve in the current year or across any recent historical years. | Boolean | Major |
| **Current long term surplus above threshold** | Evaluates whether the school has accumulated excessive reserves consistently above the surplus threshold across multiple consecutive years. | Boolean | Major |
| **Overspend** | `(Total Income - Total Expenditure) / Total Income`. Penalises in-year operational deficits, escalating from minor to major flags as overspend increases. | Percentage | Major |
| **Large decrease in balance from last year** | Flags a sharp year-on-year drop in revenue reserves when balances are already below the safe balance threshold. | Boolean | Major |

### 2. School Characteristics Indicators

| Indicator Name | Description / Formula | Value Type | Max Flag |
| :--- | :--- | :---: | :---: |
| **% capacity** | `Number of pupils / school_places`. Measures under-subscription, applying flags when capacity utilisation falls below threshold levels. Not applicable to Special schools. | Percentage | Major |
| **Change in pupil numbers from last year** | `(Pupils [current] - Pupils [Y-1]) / Pupils [Y-1]`. Penalises falling rolls, escalating flags as the rate of pupil decline increases. | Percentage | Major |
| **Change in pupil numbers over 4 years** | `(Pupils [current] - Pupils [Y-4]) / Pupils [Y-4]`. Penalises long-term decline in pupil numbers over a 4-year period. | Percentage | Major |
| **Number of 6th form pupils** | Headcount of post-16 students (`TotalPupilsSixthForm`). Flags small sixth forms that may face financial viability challenges. Not applicable to Primary or Nursery schools. | Decimal | Minor |
| **Current % total pupil absence** | Total absence rate (`sess_overall_percent`). Flags high pupil absence, with higher allowance thresholds for special provisions. Labelled with the previous academic year. | Percentage | Minor |

### 3. Educational Performance Indicators

| Indicator Name | Description / Formula | Value Type | Max Flag |
| :--- | :--- | :---: | :---: |
| **Parental preference** | `times_put_as_1st_preference / total_number_places_offered`. Formatted as `Low`, `Medium`, or `High` based on configured thresholds. Flags low parental demand. Not applicable to Special, Nursery, or PRU schools. | String | Major |
| **Educational progress (KS2/KS4)** | Uses Key Stage 2 progress scaled for Primary schools, or Key Stage 4 Progress 8 for Secondary schools. Flags significantly below-average pupil progress. Labelled with previous academic year. Not applicable to Special schools. | Decimal | Minor |
| **Educational attainment score (KS2/KS4)** | Uses Key Stage 2 proportion meeting expected standard for Primary schools, or Key Stage 4 Attainment 8 for Secondary schools. Flags low attainment. Labelled with previous academic year. Not applicable to Special schools. | Decimal | Minor |

## What Do the Risk Flags Mean?

Every indicator evaluated for a school assigns one of three risk flag levels: No Flag, Minor, or Major

### Aggregate Risk Metrics in Headline Reporting

At the school level, individual flags and scores roll up into three summary counts used to grade schools:

- **`Total_Risk_Score` (`Overall`)**: The arithmetic sum of all individual metric contribution scores.
- **`Total_Risks`**: The total count of all flags raised across all metrics (both `Minor` and `Major`).
- **`Total_Major_Risks`**: The count of `Major` flags raised.

### Handling Missing Data vs Non-Applicable Metrics

The scoring engine enforces strict rules depending on whether a missing value is caused by **data omission** or **non-applicability**:

- **Missing Data Penalty (Applicable School)**: If a school is applicable for an indicator but the required data point is missing (`NaN` or `None`), the engine applies a conservative risk stance: the school is penalised with the **maximum contribution score** (`RiskIndicatorContributionMax`) and the **maximum flag** (`RiskIndicatorFlag` = `Major` or `Minor`).
- **Non-Applicable School**: If a school is exempt from a metric due to its phase (see [Do Metrics Apply to All Schools?](#do-metrics-apply-to-all-schools)), no penalty is incurred. The score contribution is set to `0.0`, the flag is set to `No flag`, and the value is formatted as `"N/A"`.

## How Grades Are Applied to a School

Schools receive an overall summary risk grade ranging from **A\*** (lowest risk, outstanding financial and operational health) to **G** (highest risk).

### Sequential Evaluation

The engine tests each grade from **A\*** down to **F**. The first grade whose criteria are completely met is assigned. If a school fails the criteria for A\* through F, it is assigned grade **G** by default.

> **Key Rule**: A single major risk immediately disqualifies a school from receiving grades **A\***, **A**, or **B**. A school with a single major flag cannot achieve a grade higher than **C**.

## Data Sources and Academic Year Publication Lag

The LAA pipeline relies on both internal financial datasets and external non-financial collections.

### Primary Data Sources

The main data source is CFR and the other ancillary datasets from EBIT, but a few more data sources are needed.

- **School Capacity**: Net capacity and available school places (`capacity_school_*.csv`).
- **Specialist Provision Capacity**: Capacity figures for special provisions (`specialist-provision_school_*.csv`).
- **Pupil Absence**: Three-term total absence rates (`1a_Absence_3term_school.csv`).
- **Parental Preference**: First-preference applicant numbers against total places offered (`AppsandOffers_*_SchoolLevel*.csv`).

### Do Data Sources Lag at the Start of the CFR Year?

**Yes.** National educational statistics and external datasets operate on publication cycles that do not align identically with the financial CFR submission:

#### 1. Ancillary Publication Lag

National datasets for school capacity are published substantially later than census or financial returns. For example, during the **2026** pipeline run:

- Pupil absences and parental preference data are available for period `202526`.
- School capacity and specialist provision capacity are only published up to period `202425` (lagging by 1 academic year).

## Do Metrics Apply to All Schools?

**No.** Metrics do not apply uniformly to all schools. Applicability is restricted based on **school type**, **education phase**, and **federation governance**:

### 1. Federation Exclusions

Maintained schools that belong to a federation but are **not the lead school** (`Lead school in federation != "0"`) are filtered out during pre-processing. Only independent (unfederated) schools and the designated lead school of a federation are scored, preventing distorted reporting where financial returns are consolidated at the federation level.

### 2. Educational Phase Applicability

Certain indicators are inapplicable to specific phases:

| Indicator | Inapplicable / Excluded Phases | Rationale |
| :--- | :--- | :--- |
| **% capacity** | `Special` | Special schools operate under specialist commission models rather than standard net capacity assessments. |
| **Number of 6th form pupils** | `Primary`, `Nursery` | Sixth forms exist only in secondary, middle-deemed-secondary, or all-through settings. |
| **Parental preference** | `Special`, `Nursery`, `Pupil Referral Unit` | Admissions to nursery, special, and alternative provisions follow local authority placements, EHCPs, or referral routes rather than standard national primary/secondary preferences. |
| **Educational progress (KS2/KS4)** | `Special` | Mainstream progress measures (Progress 8, KS2 progress) are not calibrated for special school cohorts. Primary schools use KS2; Secondary schools use KS4. |
| **Educational attainment score (KS2/KS4)** | `Special` | Attainment 8 and KS2 expected standards do not apply to special school cohorts. Primary schools use KS2; Secondary schools use KS4. |

When an indicator is marked not applicable for a school, it is serialised as `"N/A"`, contributes **`0.0`** to the risk score, and receives **`No flag`**.
