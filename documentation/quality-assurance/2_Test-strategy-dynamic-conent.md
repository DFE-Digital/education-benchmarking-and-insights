# Test Strategy: Dynamic Content

## Purpose

This document outlines the strategy for safely managing and validating updates to **dynamic content** within the FBIT platform. These include time-sensitive banners and commercial resource links that are stored and updated via the database rather than the application codebase.

This strategy complements the broader Test Strategy by addressing scenarios where changes bypass standard CI/CD automation and require environment-based validation.

Dynamic content updates are **small but high-risk changes**. This strategy ensures they:

- Follow a **peer-reviewed, auditable** process
- Are **validated in a staging environment** before going live
- Receive **explicit sign-off** by a product stakeholder
- Are deployed to Production **only after formal review**

This keeps the system accurate, trustworthy, and change-safe — even without full automation.

## Scope

**In Scope:**

- Dynamic banner messages rendered on the UI
- Commercial links (e.g. recommended resources)
- Content logic (start/end date, audience targeting)
- Deployment pipeline from **Pre-Prod → Prod**
- Client sign-off and visibility verification

**Out of Scope:**

- Functional testing of underlying components (covered in E2E)
- Link health (covered via automated uptime/link checkers)
- Static or content-coded UI changes

## Goals

- Prevent regressions caused by incorrect DB content updates
- Maintain consistency and traceability across environments
- Ensure all content changes are signed off before Production
- Establish a clear, reviewable change lifecycle for content

## Responsibilities

| Role                | Responsibility                                        |
|---------------------|-------------------------------------------------------|
| Engineer (Author)   | Prepares DB update script and coordinates testing     |
| Engineer (Reviewer) | Reviews script for safety, scope, and correctness     |
| QA (Optional)       | Assists in Pre-Prod validation and documentation      |
| Product Owner       | Signs off content in Pre-Prod after visual inspection |
| Delivery Lead       | Confirms sign-off before Production deployment        |

## Environments

| Environment | Purpose              | Usage                                                                                |
|-------------|----------------------|--------------------------------------------------------------------------------------|
| Pre-Prod    | Testing and sign-off | Validate DB update reflects exactly what will appear in Prod, signed off by clients. |
| Prod        | Live system          | Final DB update done after full sign-off.                                            |

## Test Types

**Peer review:**

All scripts must be reviewed by another engineer during the PR review process.
Reviewer checks for:

- Correct implementation
- Consistency with the previous scripts unless changed purposely

**Pre-production manual testing:**

- Confirm DB updates show correct banner text/link
- Check content visibility logic (e.g., expiry dates, start dates)
- Sign off by clients

**Production manual testing:**

- Perform visual smoke check (no deep testing in Prod)

## Risk Mitigation

| Risk                          | Mitigation                                            |
|-------------------------------|-------------------------------------------------------|
| Incorrect content in Prod     | Pre-Prod visual validation + sign-off required        |
| Visibility logic fails        | All logic paths must be manually reviewed in Pre-Prod |
| Unreviewed DB script deployed | PR review is mandatory before any change is applied   |

## Supporting Docs

- Content management logic: [`developers/14_Content-Management.md`](../developers/14_Content-Management.md)
- Banner component rendering logic: available in FE repo (see `BannerComponent.tsx`)

<!-- Leave the rest of this page blank -->
\newpage
