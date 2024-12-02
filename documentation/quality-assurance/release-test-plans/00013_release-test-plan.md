# Release Test Plan - 2024.12.0

**Release Date:** 02/12/2024

**Release Label:** 2024.12.0

## Introduction

This plan defines the approach for testing release `2024.12.0`.

Ensure that the enhancement, and critical bug fix in `2024.12.0` are functioning as expected without adversely impacting existing
functionality.

## Scope

**In-scope:**

- Enhancements
  - Data pipeline improvements
    - Contextual based on explicit to year
    - Code restructure to drop "src" package
    - Remove old data-pipeline start queue
  - RBAC improvements
    - Allow LA organisations to access schools
    - Allow academy access to trust
  - Add a link to Data Sources on the Home Page
  - Commercial resources shouldn't show when red/amber is due to underspend
- Bug fixes
  - Invalid search criteria on 'find organisation' page causes 400 in Azure Search
  - Unable to upsert Financial Plan for Pennine Way Primary School (131177)
  - Trust (11830749) home page returns 404 not found
  - Duplicate Tooltips displayed in Horizontal Bar Chart when using both keyboard and mouse to navigate

**Out-of-Scope:**

- Any new functionality not targeted for this release.

## Test Strategy

- Sanity Testing: Perform sanity checks on bug fixes to confirm their resolution.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.

## Entry and Exit Criteria

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

N/A

## Test Deliverables

- Test plan document.
- Test summary report outlining test results, pass/fail rates, and any outstanding issues.

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=240599&suiteId=240600)**

**Azure DevOps tickets:**

- [232974 : Contextual based on explicit to year](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232974)
- [231741 : Drop "src" package](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231741)
- [236226 : Remove old data-pipeline start queue](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/236226)
- [236150 : Add a link to Data Sources on the Home Page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/236150)
- [236854 : Allow LA organisations to access schools](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/236854)
- [236921 : Allow academy access to trust](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/236921)
- [236925 : Commercial resources shouldn't show when red/amber is due to underspend](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/236925)
- [235691 : Invalid search criteria on 'find organisation' page causes 400 in Azure Search](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235691)
- [235762 : Unable to upsert Financial Plan for Pennine Way Primary School (131177)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235762)
- [238781 : Removal of AzureWebJobsDashboard setting](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/238781)
- [239062 : Add guard for when there is no school in Trust or LA](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/239062)
- [229670 : Collect telemetry data for search traffic analytics](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/229670)
- [239313 : 404 on missing Apple favicons](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/239313)
- [226832 : A02: Duplicate Tooltips displayed in Horizontal Bar Chart when using both keyboard and mouse to navigate](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/226832)
- [235688 : The Beckmead Trust (11830749) home page returns 404 not found](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/235688)


<!-- Leave the rest of this page blank -->
\newpage
