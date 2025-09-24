# Domain

The Financial Benchmarking and Insights Tool (FBIT) platform ingests and analyses financial data from schools, trusts,
and local authorities in England. The platform supports comparison, accountability, and strategic financial planning.

The system is structured around four key financial data periods, each representing the earliest and most reliable
opportunity to compile a complete financial projection for a given organisation type. These financial data drops also
trigger the refresh of related ancillary datasets to ensure timely, relevant, and accurate benchmarking.

## Ancillary Data Refresh Logic

Raw financial data from respective institutions are processed along with dimension data to enrich and give context.
These dimension data are all grouped and known as Ancillary data. Each new financial data period submission triggers
the refresh of its linked ancillary datasets. This ensures:

- Timely and aligned comparisons
- Up-to-date insights reflecting current school demographics, performance, and structure
- Accurate forecasting and risk analysis

## Why These Financial Data Periods?

The selected four financial data periods (BFR, AAR, CFR, S251) reflect the earliest reliable points at which a complete financial
projection can be compiled for an organisation. This enables:

- Consistent comparisons across educational sectors
- Predictive insight into financial health
- Proactive identification of risk or underperformance

> **Note**:  
> This grouping of datasets into financial data periods is not a constraint of the data itself, nor a limitation of the technical
> architecture. Rather, it is a logical construct arising from the service design and capabilities implemented.
> It reflects when sufficient and reliable data is available to support meaningful insight generation and analysis.

## Financial Data Periods and Ancillary Data

|  | **Academies** | **Academy Trusts** | **Local Authority** | **Local Authority Schools** |
|---|---|---|---|---|
| **Actual Annual Spend** | AAR (audited) | AAR (audited), BFR (pre-audit) | S251 | CFR |
| **Predicted Annual Spend** | | BFR (pre-audit) | S251 | |

**1. Budget Forecast Return (BFR):**

- **Entity**: Academy Trusts
- **Purpose**: Unaudited forward-looking projections of income, expenditure, risk, and solvency.
- **Data Contents**:
  - Projected revenue and capital income/expenditure
  - Forecasted cash flow, loans, and reserves
- **Usage examples**:
  - Used on the Forecast and Risk page (the old High Needs Benchmarking Tool/HNBT) alongside actuals from AAR data to show predicted vs actual spend for trusts.
- **Ancillary Data**:
  - BFR_3Y contains expenditure forecasts for trusts but since the Fprecast and Risk page shows data going back 2 years from the current year as well as a forecast going forward, previous BFR_SOFA releases are used to show what the predicted spend was for that year (this keeps one trend line as BFR values and one trend line as actuals from AAR). For example, the 2024 Forecast and Risk page would show the 2024 BFR_SOFA prediction against 2024 and the 2023 BFR_SOFA prediction against 2023.
  - For Fprecast and Risk to correlate AAR actual expenditure with BFR datapoints, a common point of reference is needed. BFR links to AAR on Trust UPIN to get the Company Reference Number, which acts as this common reference.

**2. Academies Accounts Return (AAR):**

- **Entity**: Academy Trusts
- **Purpose**: Audited financial accounts submission.
- **Data Contents**:
  - Actual income and expenditure per academy or trust
  - Balance sheet (assets, liabilities, reserves)
- **Usage examples**:
  - Used on the High Needs Benchmarking Tool to show trust actual expenditure.
  - Academy/Trust expenditure is sorted into cost categories comparable with LA schools, normalised (eg per pupil), and benchmarked against comparators.
  - Spend is also plotted over time using previous returns to show historic expenditure trends for schools/trusts.
  - Academies can see the apportioned revenue reserves and expenditure from their relevant trust (Academy is joined to Trust on Trust UPIN).
- **Ancillary Data**:
  - Condition Data Collection (CDC) - Contains building age and area determine comparator schools. Joined on DfE URN.
  - Pupil Census - Used to benchmark pupil numbers and also to determine comparators using data like free school meal (FSM) percentages and english as a first language. Joined on DfE URN.
  - Workforce Census - Used to benchmark workforce numbers, which are also used as data for determining comparators. Joined on DfE URN.
  - Chief Financial Officer (CFO) Details - Contact details for an organisation. Joined on Company Reference Number.
  - School Metadata from GIAS - Contact details for a school. joined on DfE URN.
  - KS2 & KS4 Progress Metrics - Data points to determine comparators. Joined on DfE URN.
  - Special Educational Needs (SEN) - Contains SEN spend to determine comparators. Joined on DfE URN.
  - High exec pay data (HExP) - Used to benchmark high exec pay for trusts. Joined on Company Reference Number.

**3. Consistent Financial Reporting (CFR):**

- **Entity**: Maintained Schools
- **Purpose**: Standardised school-level income and expenditure data.
- **Data Contents**:
  - Categorised income/expenditure statements
- **Usage examples**:
  - Used to determine comparators for LA schools
  - Used to benchmark IT spending
  - School expenditure is sorted into cost categories comparable with Academies, normalised (eg per pupil), and benchmarked against comparators.
  - Spend is plotted over time using previous returns to show historic trends for schools/trusts.
- **Ancillary Data**:
  - Condition Data Collection (CDC) - Contains building age and area determine comparator schools. Joined on DfE URN.
  - Pupil Census - Used to benchmark pupil numbers and also to determine comparators using data like free school meal (FSM) percentages and english as a first language. Joined on DfE URN.
  - Workforce Census - Used to benchmark workforce numbers, which are also used as data for determining comparators. Joined on DfE URN.
  - School Metadata from GIAS - Contact details for a school. joined on DfE URN.
  - KS2 & KS4 Progress Metrics - Data points to determine comparators. Joined on DfE URN.
  - Special Educational Needs (SEN) - Contains SEN spend to determine comparators. Joined on DfE URN.
  - Individualised Learner Record (ILR) 6th Form Data - Used as a fallback to the pupil census when datapoints are missing. Linked on DfE URN, using GIAS links to help linkage.

**4.Section 251 (S251):**

- **Entity**: Local Authorities
- **Purpose**: Reporting on planned and actual education expenditure.
- **Data Contents**:
  - Actual spend on schools, high needs, early years, and central services
  - Planned spend
- **Usage examples**:
  - Used to find comparator LAs and form comparator sets.
  - LA expenditure is sorted into cost categories, normalised (eg per pupil), and benchmarked against comparators.
  - Used on the Forecast and Risk page to show budget vs outturn historical spend for trusts.
- **Ancillary Data**:
  - Statistical Neighbours Data - Sourced from the old HNBT excel file, this is needed for the Forecast and Risk page and joined to s251 on LA code.
  - Age-related Population Estimates - ONS data to normalise LA spending per population. Joined on LA code. These estimates are from 2018 and so have 2018 LA geograhies, so do not match the current year s251 LA submissions in every case (most are fine).
  - Local Authority SEN2 ECHP plan values - Historical to current SEN expenditure data for LAs. Joined on both old and new LA code.
  
<!-- Leave the rest of this page blank -->
\newpage
