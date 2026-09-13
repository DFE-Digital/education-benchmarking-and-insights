---
title: "Microsoft Clarity"
layout: sub-navigation
sectionKey: "Explanation"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Microsoft Clarity"
  parent: "Developers Explanation"
---

## Why Clarity scripts are injected only after consent

We deliberately load Microsoft Clarity only when a user has explicitly accepted analytics cookies. This ensures that any session data we collect is meaningful.

For our use case, we care about session‑level analytics. Clarity only provides useful stitched sessions when the user has consented. Loading Clarity without consent would produce incomplete  or low‑value session data, which does not meet our business needs.

To maintain data quality:

1. Clarity is only injected once analytics consent is granted.
2. If consent is later withdrawn, we clear Clarity cookies and Clarity is not loaded.
3. Non‑consent traffic is intentionally not tracked, as it does not produce meaningful session analytics for our purposes.

This ensures that every Clarity session we see is from a user who has explicitly opted in, and that the data remains accurate, consistent, and aligned with our analytics requirements.

## Environment & Project Structure

We maintain separate Clarity projects for each environment to prevent cross‑environment pollution and ensure that analytics remain meaningful.

These ProjectIds are loaded into configuration via Terraform for the Web project.

### Current Projects

- Dev FBIT
- Test FBIT
- Pre‑Prod FBIT
- FBIT

These projects serve different purposes:

- **FBIT (Production)**. This is the only environment that contains meaningful user behaviour. All real analytics data comes from this environment.
- **Dev FBIT, Test FBIT, Pre‑Prod FBIT**. These environments exist solely for testing and validating the Clarity integration, consent handling, cookie behaviour, script injection, and configuration. They are not expected to produce meaningful behavioural analytics.

The following environments are intentionally excluded:

- Local development
- Ephemeral feature branches (d11+)
- Automated test (d02)

These environments do not provide useful behavioural data and do not provide reliable conditions for testing Clarity. As a result, they are omitted to keep analytics clean and focused. If needed later, they can be added with a simple configuration change in Web/Terraform.
