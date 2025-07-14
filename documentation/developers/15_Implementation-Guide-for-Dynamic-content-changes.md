# Lifecycle of a Content Change (DB Update Flow)

## Introduction

This guide outlines the step-by-step process for making updates to dynamic content (such as banners and commercial links) by updating the database.
It ensures consistency, proper validation, and safe deployment across all environments.

## Overview

The lifecycle of a content change involves the following key stages:

 **Drafting:**

Business or Product Owner provides:

- Banner text or commercial link URL
- Start and end dates (if applicable)

**DB Script Preparation:**

Developer or QA writes an SQL script to insert or update the dynamic content in the database

The script includes:

- Required values (text, links, flags)
- Validity dates
- A comment header with Date and associated work item

In order to track the history of updates, relevant SQL scripts should be persisted to the repo in the [scripts/content](../../scripts/content) folder.

**Apply to Pre-Prod:**

- Script is applied in the Pre-Prod environment
- QA or engineer verifies the following:

  - Correct appearance of the banner/link on the UI
  - Visibility rules (based on date flags or conditional logic)

**Sign-off:**

- Product Owner or Stakeholder reviews the content in Pre-Prod
- Formal sign-off is recorded before proceeding to Production

**Apply to Prod:**

- The engineer runs the final, reviewed version of the script on the Production environment.
- To avoid impacting live users, we do not manually restart the app service to clear the cache. Instead, we allow the cache to expire naturally.
- After approximately one hour (once the cache clears automatically), smoke testing is performed to confirm the changes are live:

  - Visual confirmation of the banner or link
  - Perform a basic check to ensure any associated links function correctly.

This process ensures dynamic content updates are safely reviewed, tested, and promoted with full traceability and sign-off.

<!-- Leave the rest of this page blank -->
\newpage
