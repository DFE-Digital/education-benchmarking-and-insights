# Developer Feature Documentation: Service Banners

## Introduction

This document provides detailed information for developers about the implementation, usage, and integration of the Service Banners feature within the system.

## Overview

Service Banners that make use of the [GDS notification banner](https://design-system.service.gov.uk/components/notification-banner/) component may be content-managed to display on selected pages across the web application.
This allows notifications regarding known platform problems, data releases, or other issues to be easily displayed to the user without the need for a code release.

## Goals

### Primary Goal

Allows notifications to be displayed to the user on the following pages, without the need for a code release:

- Service home page
- Local authority home page
- School home page (academy)
- School home page (maintained school)
- Trust home page

### Secondary Goals

- Allow notifications to appear on a single or multiple pages
- Ensure only one notification banner present on page (e.g. to not conflict with user action confirmation notifications)
- Support formatting within message body
- Time frame during which notification should be visible may be customised

## Prerequisites/Dependencies

### External Dependencies

A user with write access to the database who may configure the banner(s) as required.

### Internal Dependencies

- Database migrations should have been run to create the new table and view
- Content API must be running
- Cache should be configured (see [web README](../../web/README.md))
  - For development purposes, cache may also be disabled by setting `"Disabled": true`

## Usage

### Database

The banner records are stored in the `Banner` SQL table:

```sql
Id          integer identity (1,1)  NOT NULL,
Title       nvarchar(255)           NOT NULL,                       -- Corresponds to notification `title`
Heading     nvarchar(2000)          NULL,                           -- Corresponds to notification `heading` as markdown
Body        nvarchar(max)           NOT NULL,                       -- Corresponds to notification `body` as markdown
Target      nvarchar(255)           NOT NULL,                       -- JSON array of strings corresponding to known targets (see below)
ValidFrom   datetimeoffset          NOT NULL DEFAULT GETUTCDATE(),
ValidTo     datetimeoffset          NULL,

CONSTRAINT PK_Banner PRIMARY KEY (Id),
```

e.g.:

| Id | Title             | Heading                     | Body                                                                 | Target                                       | ValidFrom                          | ValidTo                            |
|----|-------------------|-----------------------------|----------------------------------------------------------------------|----------------------------------------------|------------------------------------|------------------------------------|
| 1  | Home page         | Banner on home page         | This banner will appear on the **home page** only during 2025        | ["ServiceHome"]                              | 2025-01-01 00:00:00.0000000 +00:00 | 2025-12-31 23:59:59.9999999 +00:00 |
| 2  | School home pages | Banner on school home pages | This banner will appear on **school home pages** only from July 2025 | ["SchoolHomeAcademy","SchoolHomeMaintained"] | 2025-07-01 00:00:00.0000000 +00:00 |                                    |

The `VW_ActiveBanners` view filters and displays only active banners within a valid date range, expanded by target using `CROSS APPLY`.

e.g. For a request during September 2025:

| Title             | Heading                     | Body                                                                 | Target               | ValidFrom                          |
|-------------------|-----------------------------|----------------------------------------------------------------------|----------------------|------------------------------------|
| Home page         | Banner on home page         | This banner will appear on the **home page** only during 2025        | ServiceHome          | 2025-01-01 00:00:00.0000000 +00:00 |
| School home pages | Banner on school home pages | This banner will appear on **school home pages** only from July 2025 | SchoolHomeAcademy    | 2025-07-01 00:00:00.0000000 +00:00 |
| School home pages | Banner on school home pages | This banner will appear on **school home pages** only from July 2025 | SchoolHomeMaintained | 2025-07-01 00:00:00.0000000 +00:00 |

`ValidFrom` is returned in case multiple banners are returned for a particular target. The API will return the most recent only.

### API Reference

The banner endpoint is within the Content API. Either one banner is returned, or a `404` for a particular target.

Endpoint: `GET api/banner/{target}`

`200` response:

```json
[
    {
        "title": "string",
        "heading": "string",
        "body": "string"
    }
]
```

### Razor view configuration

The main site `_Layout.cshtml` has an optional named section `banner` that supporting pages may populate in order for the banner to be rendered at the correct location on the page. To prevent multiple banners from being rendered, the content of the section may be conditionally added. The banner for a particular target may be looked up via an API call and rendered to the section using the `BannerViewComponent`.

Putting this all together, on the school home page this should be something like:

```cs
@section banner {
    @if (noOtherBanners)
    {
        var target = Model.IsPartOfTrust ? BannerTargets.SchoolHomeAcademy : BannerTargets.SchoolHomeMaintained;
        @await Component.InvokeAsync("Banner", new { target })
    }
}
```

An optional argument `columnClass` may also be set to ensure the banner width matches the rest of the page content, as per the GDS notification banner usage guidelines. e.g:

```cs
@section banner {
    @await Component.InvokeAsync("Banner", new
    {
        target = BannerTargets.ServiceHome, 
        columnClass = "govuk-grid-column-two-thirds"
    })
}
```

The view component performs the API request if no result is in memory cache for the given target. The result is then cached and returned.

If not null, the notification is rendered and for the heading and body any markdown is rendered as HTML using [markdig](https://github.com/xoofx/markdig) via [Westwind.AspNetCore.Markdown](https://github.com/RickStrahl/Westwind.AspNetCore.Markdown)'s tag helpers. GDS classes are applied in `GdsMarkdownExtension` on a node-by-node basis, e.g.:

```cs
switch (node)
{
    case LinkInline:
        attributes.AddClass("govuk-link");
        break;
    // ...
}
```

## Configuration

The banner targets are in the static `BannerTargets` class. The original values are:

- `LocalAuthorityHome`
- `SchoolHomeAcademy`
- `SchoolHomeMaintained`
- `ServiceHome`
- `TrustHome`

The banner cache configuration is within the `CacheOptions` app settings in Web and the values are defined in minutes, e.g.:

```json
{
  "CacheOptions": {
    "Banners": {
      "SlidingExpiration": 10,
      "AbsoluteExpiration": 60,
      "Disabled": false
    }
  }
}
```

## Deployment

There are no deployment considerations for this feature. It is not managed via a feature flag.

## Known Issues

- The notification banner is intentionally implemented using the default 'blue' version only.
- Only the most common GDS classes are rendered from the body/heading markdown.

<!-- Leave the rest of this page blank -->
\newpage
