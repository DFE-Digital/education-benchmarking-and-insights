# Transition from Population-based to Pupil-based Normalisation/Standardisation (0018)

Date: 2025-12-02

Author: Tech Lead

Stakeholders: Product Owner, Client Stakeholders, Engineering Team

## Status

Proposed

## Context

Currently, the application calculates funding and metric comparisons using Office for National Statistics (ONS) population data (specifically the "Per Head 2 to 18" population demographic).

To improve the accuracy of benchmarking and needs analysis, we require a shift to **Per Pupil** calculations based on actual school census data.

**Key Constraints & Risks:**

* **Future Roadmap:** A more comprehensive version of this logic is planned for a future phase.
* **Risk of Rework:** We must ensure that the solution implemented now does not create "technical debt" or code that needs to be completely thrown away when the future phase begins. The solution needs to be a stepping stone, not a cul-de-sac.
* **Data Integrity:** The solution must maintain the integrity of the data pipeline.

## Options Considered

**Option A: Strategic Pipeline Integration (Recommended):**

Updating the existing data pipeline and API to support Pupil counts natively, leveraging the work already done during the standardisation/normalisation investigation.

* **Pros:**
  * **Minimizes Rework:** We are building the foundation for the future phase now.
  * **Robustness:** Automated, reproducible, and robust.
  * **Extensibility:** Leverages existing API extensionality.
* **Cons:** Slightly higher initial development complexity than a "hacky" fix, but safer in the long run.
* **Outcome:** Selected.

**Option B: Do Nothing (Defer to Roadmap):**

Wait until the scheduled "complete solution" phase in the future roadmap to implement these changes.

* **Pros:**
  * Zero immediate effort.
  * Allows the team to focus entirely on the current backlog without context switching.
* **Cons:**
  * **Business Impact:** The current ONS population data is leading to inaccuracies in benchmarking. The business requires accurate per-pupil data immediately to support current decision-making; deferring would prolong the use of less accurate metrics.
  * **Missed Opportunity:** We miss the chance to build the foundational data structures now, which actively reduces the complexity of the future roadmap item.
* **Outcome:** Rejected.

## The Decision

We will proceed with **Option A: Strategic Pipeline Integration**. We will integrate pupil figures directly into the automated data pipeline and update the API/Web layers to consume this new data source.

This approach mitigates the risk of rework because the API schema changes and pipeline aggregations are necessary prerequisites for the future "complete solution" anyway. We are essentially pulling a piece of future architectural work forward.

## Technical Implementation Plan

**Data Pipeline & Database:**

* **Action:** Update the ETL (Extract, Transform, Load) pipeline to output pupil figures aggregated at the Local Authority (LA) level.
* **Note:** This utilizes the school data previously prepared during the standardisation/normalisation analysis.
* **New Views:** Implement associated database views to support the specific "Need Calculation" logic.

**API Layer:**

* **Action:** Update the API to query the new views.
* **Design Pattern:** We will utilize the existing "extensionality" baked into the API design, allowing the frontend to toggle between viewing data as "Actual," "Per Head," or the new "Per Pupil."
* **Response:** Update API response payloads to include the specific Pupil Count fields.

**Web / Frontend:**

* **Benchmarking Page:**
  * Update table columns to display per-pupil metrics.
  * Update chart titles and introductory text.
  * Remove legacy references/links to ONS data in the Help & Support section.
* **Historic Data:** Update chart titles and descriptors.
* **Mapping:** Ensure the UI correctly maps to the new API fields.

## Effort Estimates & Planning

While the changes can technically occur in parallel, reducing the critical path, we are estimating **3 Days** total effort for planning purposes to account for testing and deployment.

| Area      | Tasks                                                   | Estimate   |
|:----------|:--------------------------------------------------------|:-----------|
| **Data**  | Pipeline updates, Aggregation logic, DB Views           | 1 Day      |
| **API**   | Endpoint updates, View integration, Response formatting | 1 Day      |
| **Web**   | Content updates, Chart titles, Table columns, Help text | 1 Day      |
| **Total** |                                                         | **3 Days** |

## Consequences

* **Positive:** The application will reflect more accurate "Per Pupil" data immediately.
* **Positive:** The "Idempotent" nature of our data pipeline is preserved (no manual patches to maintain).
* **Neutral:** Requires coordination across full stack (Data/API/Web).

## Notes & Discounted Alternatives

**Discounted: Manual Data Injection (Hotfix):**

We discounted the option of manually calculating and injecting pupil numbers (via SQL script) for the following reasons:

1. **Data Integrity:** It breaks the idempotency of the data pipeline. Manual changes would be overwritten every time the pipeline runs, requiring constant manual maintenance.
2. **Speed vs. Risk:** While theoretically bypassing pipeline code, the effort required to write and validate safe SQL scripts neutralizes the speed benefit.
3. **Validation:** High QA overhead due to the risk of human error in manual entry.

<!-- Leave the rest of this page blank -->
\newpage
