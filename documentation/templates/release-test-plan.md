>[!IMPORTANT]
> Remove this and any other blockquote sections from final document.

>[!WARNING]
> Do not remove new page tag at the bottom of the document.

# Release Test Plan - [Insert release label]

**Release Date:** [Insert date]

**Release Label:** [Insert release label]

## Introduction
>
>[!NOTE]
>Update the below to reflect the purpose/objective for this plan, so that it is specific to the release.

This plan defines the approach for testing release `[release label]`, covering all functional, non-functional, and regression testing necessary.
Ensure that all new features, enhancements, and bug fixes in `[release label]` are functioning as expected without adversely impacting existing functionality.

## Scope
>
>[!NOTE]
>In-scope lists/describes the scope of testing. This shouldn't simply be a list of items from the sprint.
>Ticket title may be adequate to describe the feature/enhancement/bug fix,
>however if it doesn't than a meaning title should be added here. 
> Link to the ADO ticket should be included in the notes.
> 
>Out-of-scope list any other areas that are explicitly out of scope.

**In-scope:**

- New features
  - [List new features]
- Enhancements
  - [List enhancements]
- Bug fixes
  - [List bug fixes]

**Out-of-Scope:**

- Any new functionality not targeted for this release

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
