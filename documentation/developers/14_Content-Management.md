# Content Management

Various 'static' pages and banners within the platform are backed by content managed via the core database.
These are all resolved via the Content API and it is the intention that these will be managed by the client via a user interface.
For the time being, any changes to content managed here should be carried out manually in the database which does carry an element of risk and lack of auditing.

> 💡 This documentation will be updated when additional CMS features are supported. This should cover the proposed admin application, auditing, authentication, caching etc.

## Current return years

### Endpoint

- `/api/current-return-years`

### Source

The table `[dbo].[Parameters]` contains the following values for the current years, as populated and returned in a `FinanceYears` model:

- `LatestAARYear`
- `LatestCFRYear`
- `LatestS251Year`

### Consuming pages

- School spending
- Trust spending
- School summary
- Local authority high needs

## Commercial resources

### Endpoint

- `/api/commercial-resources`

### Source

The view `[dbo].[VW_CommercialResources]` is used to return a collection of `CommercialResource` models that are currently marked as active. Sub categories of each resource are listed as 'tags' that are hydrated into each parent category before being returned. The underlying table for the view is `[dbo].[CommercialResources]`.

### Consuming pages

- School find ways to spend less
- Trust find ways to spend less
- Local authority find ways to spend less
- School spending priorities

## Transparency files

### Endpoint

- `/api/files/transparency`

### Source

The view `[dbo].[VW_ActiveFiles]` is used to return a collection of `FileModel`s containing metadata for files that are currently marked as active. The underlying table for the view is `[dbo].[File]`. The underlying SQL query filters files by the following types:

- `transparency-aar`
- `transparency-cfr`

### Consuming page

- Data sources

## Lifecycle of a Content Change (DB Update Flow)

This is step-by-step process for making updates to dynamic content (such as banners and commercial links) by updating the database.
It ensures consistency, proper validation, and safe deployment across all environments.

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
