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

<!-- Leave the rest of this page blank -->
\newpage
