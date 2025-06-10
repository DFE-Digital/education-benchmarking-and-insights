# Domain

The Financial Benchmarking and Insights Tool platform ingests and analyses financial data from schools, trusts,
and local authorities in England. It supports comparison, accountability, and strategic financial planning.

The system is structured around four key financial data periods, each representing the earliest and most reliable
opportunity to compile a complete financial projection for a given organisation type. These data drops also
trigger the refresh of related contextual datasets to ensure timely, relevant, and accurate benchmarking.

## Contextual Data Refresh Logic

Each new data period submission triggers the refresh of its linked contextual datasets. This ensures:

- Timely and aligned comparisons
- Up-to-date insights reflecting current school demographics, performance, and structure
- Accurate forecasting and risk analysis

## Why These Data Periods?

The selected data periods (BFR, AAR, CFR, S251) reflect the earliest reliable points at which a complete financial
projection can be compiled for an organisation. This enables:

- Consistent comparisons across educational sectors
- Predictive insight into financial health
- Proactive identification of risk or underperformance

> **Note**:  
> This grouping of datasets into data periods is not a constraint of the data itself, nor a limitation of the technical
> architecture. Rather, it is a logical construct arising from the service design and capabilities implemented.
> It reflects when sufficient and reliable data is available to support meaningful insight generation and analysis.

## Data Periods and Contextual Data

### 1. Budget Forecast Return (BFR)

- **Entity**: Academy Trusts
- **Purpose**: Forward-looking projections of income, expenditure, risk, and solvency.
- **Data Contents**:
  - Projected revenue and capital income/expenditure
  - Forecasted cash flow, loans, and reserves
- **Contextual Data**:
  - *None* (BFR is standalone forecast data)

### 2. Academies Accounts Return (AAR)

- **Entity**: Academy Trusts
- **Purpose**: Audited financial accounts submission.
- **Data Contents**:
  - Income and expenditure
  - Balance sheet (assets, liabilities, reserves)
- **Contextual Data**:
  - CDC (Condition Data Collection)
  - Pupil Census
  - Workforce Census
  - Chief Financial Officer (CFO) Details
  - School Metadata
  - KS2 & KS4 Progress Metrics
  - Special Educational Needs (SEN)

### 3. **Consistent Financial Reporting (CFR)**

- **Entity**: Maintained Schools
- **Purpose**: Standardised school-level income and expenditure data.
- **Data Contents**:
  - Categorised income/expenditure statements
- **Contextual Data**:
  - CDC
  - Pupil Census
  - Workforce Census
  - School Metadata
  - KS2 & KS4 Progress Metrics
  - SEN Data

### 4. **Section 251 (S251)**

- **Entity**: Local Authorities
- **Purpose**: Reporting on planned and actual education expenditure.
- **Data Contents**:
  - Spend on schools, high needs, early years, and central services
- **Contextual Data**:
  - Statistical Neighbours Data
  - Age-related Population Estimates
  - Education, Health and Care Plan (EHCP) Data
  
<!-- Leave the rest of this page blank -->
\newpage
