# Release Test Plan - [TBC]

**Release Date:** [TBC]
**Release Label:** [TBC]

## Introduction

This plan defines the approach for testing release `[TBC]`, covering all functional, non-functional, and regression testing necessary.
Ensure that all new features, enhancements, and bug fixes in `[TBC]` are functioning as expected without adversely impacting existing functionality.

This release encompasses CFO details update/refresh, as well as the High Needs data ingestion.

## Scope

**In-scope:**

- New features
  - High Needs Benchmarking feature consisting of the dashboard page, benchmarking, national view, and historic data.
- Enhancements
  - CFO contact details in the service have been updated with the latest data.
  - The "-T" suffix has been removed from cost codes in the service.
  - Custom comparators will now exclude schools that have not submitted their returns.
- Bug fixes
  - The Nursery School ICFP journey now correctly displays the applicable years.

**Out-of-Scope:**

- Any new functionality not targeted for this release
- Dependency updates 

## Test Strategy
>
>[!NOTE]
>Add/remove/update where necessary to reflect the types of testings for this release.

- Functional Testing:
  - Features: Test new and updated features for correct functionality.
  - Regression: Verify that existing functionality remains intact with new changes.
- Non-Functional Testing:
  - Performance: Load testing on peak usage scenarios.
  - Security: Test for SQL injection, XSS, and other vulnerabilities.
- Exploratory Testing: Explore features and functionality without predefined scripts, to uncover issues and assess quality.
- User Acceptance Testing: Coordinate with business stakeholders to validate functionality aligns with business needs.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.
- Sanity Testing: Perform sanity checks on bug fixes to confirm their resolution.

## Entry and Exit Criteria
>
>[!NOTE]
>Add/remove/update where necessary to reflect the criteria for this release.

**Entry Criteria:**

- All code changes for release are completed and deployed to the pre-production environment.
- Pre-production environment is set up with required data.

**Exit Criteria:**

- All high-priority test cases pass.
- No critical defects remain open.
- Signed off by stakeholders.

## Roles and Responsibilities

- **QA lead:** Coordinate testing activities, manage test cases and defect triage.
- **Engineer(s):** Execute test cases, report and retest defects.
- **Stakeholders:** Participate in user acceptance testing and provide final approval.
- **Technical lead:** Oversee release planning.
- **Project lead:** Go/no-go decisions.

## Risk Analysis
>
>[!NOTE]
>Add risks (with mitigation) for this release.

- **Risk:**
  - **Mitigation:**

## Test Deliverables
>
>[!NOTE]
>Add/remove/update where necessary to reflect the deliverables for this release.

- Test plan document
- Test cases (Functional, Regression, Non-Functional)
- Test charters
- Test summary report outlining test results, pass/fail rates, and any outstanding issues

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

<!-- Leave the rest of this page blank -->
\newpage
