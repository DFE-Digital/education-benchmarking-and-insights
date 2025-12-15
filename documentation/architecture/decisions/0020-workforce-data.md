# Recommendation for workforce data senior leadership values (0020)

Date: 2025-12-12

Author: Data Analysts, Data Engineer

Stakeholders: Product Owners, Data Analysts, Client Stakeholders

## Status

Recommendation

## Executive Summary

The Financial Benchmarking and Insights Tool (FBIT) requires workforce data about senior leadership roles in schools which is not part of the settled school workforce file published in the annual 'School workforce in England' survey results.

**Recommendation:** Retain the 'school workforce' file (School_Tables_School_Workforce_Census_2024.xlsx) as the master dataset. Perform a join with the 'la school workforce' file (workforce_2010_2024_fte_hc_nat_reg_la_sch.csv) to add the three senior leadership fields for all matching schools.

## Context and Problem Statement

FBIT currently sources information for school workforce from the 'school workforce' file (School_Tables_School_Workforce_Census_2024.xlsx), which is part of the ['School workforce in England'](https://explore-education-statistics.service.gov.uk/find-statistics/school-workforce-in-england) publication, so the number of teaching and support staff at each school can be used in the benchmarking functionality included in the service.

**The Problem:**
The 'school workforce' file does not include the 3 senior leadership roles breakdown which are; Head Teachers, Deputy Head Teachers and Assistant Head Teachers, at the grain required for the senior leadership reporting requirements.  The chosen solution must satisfy the following criteria:

1. **Completeness:** Does it provide the required additional data
2. **Relatability & Explainability:** Can the figure be easily explained and traced back to published datasets
3. **Strategic Focus:** Does the source solve a verified user problem, rather than adding complexity based on assumptions?

## Options Considered

The following sources were evaluated:

1. Do nothing
2. Append/join workforce dataset to school dataset/table (school left join workforce)
3. Replace school dataset with workforce dataset

## Recommendation: Option

The recommendation is to proceed with **Option 2**.

By joining the workforce dataset to school dataset/table, the required fields, which are not available in the stable dataset, can be added to provide the additional information needed, while preserving the data integrity of the existing workforce data.

### Rationale

* **Correlation & Accuracy:** Investigation has confirmed that the 'school workforce' file is considered to be the settled and signed off version of the workforce data and should therefore be considered to be the default source for school workforce data.
* **Explainability:** If a user sees an LA total of 50,000 pupils, and then inspects the list of schools within that LA, the numbers will sum perfectly to 50,000, this builds user trust.  Also defaulting to the stable dataset is consistent with all datasets used in the FBIT service.
* **Problem-First Approach:** The additional fields are added as they are not present in the 'school workforce' file. Validation confirms that the data is consistent across both sources for all other overlapping records.

## Analysis of Alternatives

| Option                             | Reason for Rejection                                                                                                                                                                                                                                                |
|:-----------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Do nothing (Opt 1)**            | This was discounted as it does not meet the user requirements for senior leadership requirements|
| **Replace file (Opt 3)**            | We investigated why the Workforce file contain data for more schools than the School file that FBIT currently ingests. The difference is that the Workforce file includes schools that failed to provide complete data. The current School file used by FBIT excludes these non-reporting schools and serves as the official, signed-off dataset. FBIT continue to ingest the School_Tables_20xx signed-off file for benchmarks. To action the Senior Leadership Epic, workforce dataset would be left joined to school dataset (from school left join workforce). This will ensure that we bring in only signed off data to FBIT|

## Risks and Required Actions

### Risks

* **Perceived Completeness:** The combined file is not a signed off publication, there may be instances where the additional fields are later to be incorrect, the perceived risk of the is low.

### Required Actions

1. **Documentation:**
Add a document to the repo to explain how the data is sourced and combined so this can be understood for future data ingestion tasks.

2. **Data pipeline changes:**
Change the data pipeline to take in both workforce files and combine the data as needed.

<!-- Leave the rest of this page blank -->
\newpage
