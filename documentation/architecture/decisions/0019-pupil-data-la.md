# Recommendation for Sourcing Local Authority Pupil Counts (0019)

Date: 2025-12-03

Author: Tech Lead

Stakeholders: Product Owners, Data Analysts, Client Stakeholders

## Status

Recommendation

## Executive Summary

The Financial Benchmarking and Insights Tool (FBIT) requires a standardised method for displaying the "Total Pupil Count" for Local Authorities (LAs) to support High Needs and SEN2 benchmarking.

**Recommendation:** It is proposed to calculate this figure by **aggregating the existing School Census data** (summing the individual schools to create the LA total).

This approach ensures that the high-level LA figures are mathematically consistent with the school-level data users view when drilling down. Furthermore, it is recommended that **investigation into alternative data sources should be deprioritised** until research is conducted to fully understand the user problem, ensuring we do not risk obscuring the complete picture.

## Context and Problem Statement

FBIT currently sources information for individual schools from the **School Census**. This enables the normalization of financial data (e.g., "Spend per Pupil") and supports contextual analysis.

As the service expands to display Local Authority data—specifically Financial data (High Needs) and SEN2 data (EHCP and SEN support), a reliable source for the LA-level pupil count is required.

**The Problem:**
A decision is needed on the best source for this figure. The chosen metric must satisfy three criteria:

1.  **Completeness:** Does it give a representative picture of the LA?
2.  **Relatability & Explainability:** Can the figure be easily explained to users, particularly when comparing the LA total against the supporting list of schools?
3.  **Strategic Focus:** Does the source solve a verified user problem, rather than adding complexity based on assumptions?

## Options Considered

The following sources were evaluated:

1.  **Aggregated School Census (Recommended):** Summing the pupil numbers from the schools already present in the FBIT data.
2.  **DSG Funding Figures:** Using the pupil count found in the Dedicated Schools Grant data.
3.  **Census + Home Schooled:** Appending a separate dataset for home-schooled pupils to Option 1.
4.  **External Investigation:** Conducting a discovery phase to identify other external pupil datasets.

## Recommendation: Option 1 (Aggregated School Census)

It is recommended to proceed with **Option 1**.

By summing the pupil numbers from the schools already held in the database to form the Local Authority total, the service ensures data lineage is preserved from the school level up to the LA level.

### Rationale

* **Correlation & Accuracy:** Preliminary investigation indicates a significant high correlation between the aggregated FBIT school data and other LA totals.
* **Explainability:** If a user sees an LA total of 50,000 pupils, and then inspects the list of schools within that LA, the numbers will sum perfectly to 50,000. This builds user trust.
* **Problem-First Approach:** There is currently no evidence to suggest that the simplest option is insufficient. Investigating alternative sources without first researching the specific user problem risks implementing a solution that fails to address the "whole problem" or obscures the complete picture.

## Analysis of Alternatives

| Option                             | Reason for Rejection                                                                                                                                                                                                                                                |
|:-----------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **DSG Funding (Opt 2)**            | DSG figures fluctuate throughout the year and use complex calculations that may not align with the FBIT census date. This creates a risk of data mismatch queries where the LA total does not match the school list.                                                |
| **Home Schooled (Opt 3)**          | While this provides a more "complete" population figure, it introduces a discrepancy between the LA total and the drill-down school list. There is currently no User Research (UR) indicating that Home Schooling figures are required for High Needs benchmarking. |
| **External Investigation (Opt 4)** | Investigating new sources presents a risk of "analysis paralysis." **Crucially, looking for data before defining the problem risks missing the complete picture.** Investigation should only occur if research proves Option 1 is failing users.                    |

## Risks and Required Actions

### Risks

* **Perceived Completeness:** The aggregated figure technically excludes pupils not registered in the census (e.g., home-schooled).
    * *Mitigation:* The metric should be clearly labeled (e.g., "Maintained Schools Pupil Count") rather than a generic "Total Children in Area" to manage expectations.

### Required Actions

1.  **Documentation:** While data documentation is strong, a gap exists regarding full **Data Lineage**. It is recommended that documentation is written specifically to explain *why* the aggregated census is the chosen source.
2.  **Research Constraint (The "Problem First" Rule):** It is formally recommended that **future investigation into alternative data sources be deferred** until specific research has been conducted to define the problem. Proceeding without this understanding carries the risk of solving the wrong problem and failing to address the user's complete picture.