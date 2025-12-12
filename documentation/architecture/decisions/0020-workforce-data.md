# Recommendation for workforce data senior leadership values (0020)

Date: 2025-12-12

Author: Data Analysts, Data Engineer

Stakeholders: Product Owners, Data Analysts, Client Stakeholders

## Status

Recommendation

## Executive Summary

The Financial Benchmarking and Insights Tool (FBIT) requires workforce data about senior leadership roles in schools which is not part of the settled school workforce file which is published as part of the annual 'School workforce in England' survey results.

**Recommendation:** Is to use continue to use the 'school workforce' file (School_Tables_School_Workforce_Census_2024.xlsx) as the primary source of school workforce data and join to the 'la school file workforce' file (workforce_2010_2024_fte_hc_nat_reg_la_sch.csv) to add the 4 senior leaderships fields where present for the schools included in the school workforce file.  

## Context and Problem Statement

FBIT currently sources information for school workforce from the 'school workforce' file (School_Tables_School_Workforce_Census_2024.xlsx), which is part of the ['School workforce in England']( https://explore-education-statistics.service.gov.uk/find-statistics/school-workforce-in-england/2024) publication, so the number of teaching and support staff at each school can be used in the benchmarking functionality included in the service.

**The Problem:**
The 'school workforce' file does not include the 4

1. **Completeness:** Does it provide the required additional data
2. **Relatability & Explainability:** Can the figure be easily explained
3. **Strategic Focus:** Does the source solve a verified user problem, rather than adding complexity based on assumptions?

## Options Considered

The following sources were evaluated:

1. Do nothing
2. Append/join workforce dataset to school dataset/table (school left join workforce)
3. Replace school dataset with workforce dataset

## Recommendation: Option

It is recommended to proceed with **Option 1**.

By

### Rationale

* **Correlation & Accuracy:** Preliminary investigation indicates a significant high correlation between the aggregated FBIT school data and other LA totals.
* **Explainability:** If a user sees an LA total of 50,000 pupils, and then inspects the list of schools within that LA, the numbers will sum perfectly to 50,000. This builds user trust.
* **Problem-First Approach:** There is currently no evidence to suggest that the simplest option is insufficient. Investigating alternative sources without first researching the specific user problem risks implementing a solution that fails to address the "whole problem" or obscures the complete picture.

## Analysis of Alternatives

| Option                             | Reason for Rejection                                                                                                                                                                                                                                                |
|:-----------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **xxxxx (Opt 2)**            | Add reason.|

## Risks and Required Actions

### Risks

* **Perceived Completeness:** The aggregated file
  * *Mitigation:* The metric should be

### Required Actions

1. **Documentation:**
2.

<!-- Leave the rest of this page blank -->
\newpage
