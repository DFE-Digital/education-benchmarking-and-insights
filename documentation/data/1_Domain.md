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
| **Actual Annual Spend** | AAR | AAR (auditable accounts), BFR (pre-audit) | S251 (actuals) | CFR |
| **Predicted Annual Spend** | | BFR | S251 (predicted) | |

**1. Budget Forecast Return (BFR):**

- **Entity**: Academy Trusts
- **Purpose**: Forward-looking projections of income, expenditure, risk, and solvency.
- **Data Contents**:
  - Projected revenue and capital income/expenditure
  - Forecasted cash flow, loans, and reserves
- **Ancillary Data**:
  - *None* (BFR is standalone forecast data)

**2. Academies Accounts Return (AAR):**

- **Entity**: Academy Trusts
- **Purpose**: Audited financial accounts submission.
- **Data Contents**:
  - Income and expenditure
  - Balance sheet (assets, liabilities, reserves)
- **Ancillary Data**:
  - Condition Data Collection (CDC)
  - Pupil Census
  - Workforce Census
  - Chief Financial Officer (CFO) Details
  - School Metadata
  - KS2 & KS4 Progress Metrics
  - Special Educational Needs (SEN)
  - High exec pay data (HExP)

**3. Consistent Financial Reporting (CFR):**

- **Entity**: Maintained Schools
- **Purpose**: Standardised school-level income and expenditure data.
- **Data Contents**:
  - Categorised income/expenditure statements
- **Ancillary Data**:
  - Condition Data Collection (CDC)
  - Pupil Census
  - Workforce Census
  - School Metadata
  - KS2 & KS4 Progress Metrics
  - Special Education Needs (SEN) Data
  - Individualised Learner Record (ILR) 6th Form Data

**4.Section 251 (S251):**

- **Entity**: Local Authorities
- **Purpose**: Reporting on planned and actual education expenditure.
- **Data Contents**:
  - Spend on schools, high needs, early years, and central services
- **Ancillary Data**:
  - Statistical Neighbours Data
  - Age-related Population Estimates
  - Education, Health and Care Plan (EHCP) Data
  
<!-- Leave the rest of this page blank -->
\newpage
