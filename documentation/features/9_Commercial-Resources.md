# Developer Feature Documentation: Commercial Resources

## Introduction

This document provides detailed information for developers about the implementation, usage, and integration of the Commercial Resources feature within the system.

## Overview

The Commercial Resources feature offers links to suggested frameworks that help schools identify potential cost-saving opportunities.

Resources are organised into the following categories:

- Administrative supplies
- Catering staff and services
- Educational ICT
- Educational supplies
- Non-educational support staff
- Other
- Premises and services
- Teaching and teaching support staff
- Utilities

Each category is further divided into subcategories.

These resources are displayed as links in three key areas across the service.

### Spending Priorities

When a school's spending exceeds the median of its comparator set, relevant resources are displayed next to the corresponding category chart. In this view, resources are grouped by category but not further divided into subcategories.

See [Spending and Costs](./4_Spending-and-Costs.md) for details of the implementation of this feature.

### Find ways to spend less

On the Find Ways to Spend Less page, the data is presented using a GDS tab component when viewed from a selected school. Each section is explained below. When viewed from a selected trust or local authority, only the All Resources section is displayed, and the tab component is omitted.

#### Recommended for this school

If a school's spending in a specific category exceeds the median of its comparator set, the relevant resources are displayed. Here, they are grouped by category but not divided into subcategories.

#### All resources

This section presents all available resources, regardless of relevance. Resources are grouped by category and displayed using a GDS accordion component. Within each accordion section, they are further organised by subcategories.

## Goals

### Primary Goal

As part of financial benchmarking for schools, this feature aims to direct users to resources that may help them reduce spending.

## Prerequisites/Dependencies

This feature has monorepo dependencies on:

- Web app
- Establishment API
- Insight API
- Front-end components

## Usage

### DB / View

The commercial resources records are stored in the `CommercialResources` SQL table:

```sql
    Id          integer identity (1,1) NOT NULL,
    Category    nvarchar(50)   NOT NULL,
    SubCategory nvarchar(50)   NOT NULL,
    Title       nvarchar(255)  NOT NULL,
    Url         nvarchar(2000)  NOT NULL,
    ValidFrom   datetimeoffset NOT NULL DEFAULT GETUTCDATE(),
    ValidTo     datetimeoffset NULL
```

The `VW_CommercialResources` view filters and displays only active resources within a valid date range.

### API Reference

API call to Content: Retrieves commercial resources.

Endpoint: `api/commercial-resources`

```json
    [
        {
            "category": "string",
            "subCategory": "string",
            "title": "string",
            "url": "string"
        }
    ]
```

### Web

The following controllers, view models, and views manage this feature:

<!-- TODO validate links and lines-->
School resources.

- [Controller](../../web/src/Web.App/Controllers/SchoolController.cs#L126)
- [View Model](../../web/src/Web.App/ViewModels/SchoolResourcesViewModel.cs)
- [View](../../web/src/Web.App/Views/School/Resources.cshtml)

Trust resources.

- [Controller](../../web/src/Web.App/Controllers/TrustController.cs#L109)
- [View Model](../../web/src/Web.App/ViewModels/TrustResourcesViewModel.cs)
- [View](../../web/src/Web.App/Views/Trust/Resources.cshtml)

Local Authority resources.

- [Controller](../../web/src/Web.App/Controllers/LocalAuthorityController.cs#L109)
- [View Model](../../web/src/Web.App/ViewModels/LocalAuthorityResourcesViewModel.cs)
- [View](../../web/src/Web.App/Views/LocalAuthority/Resources.cshtml)

Spending Priorities.

- [Controller](../../web/src/Web.App/Controllers/SchoolSpendingController.cs)
- [View Model](../../web/src/Web.App/ViewModels/SchoolSpendingViewModel.cs)
- [View](../../web/src/Web.App/Views/SchoolSpending/Index.cshtml)

Resources retrieved via the Insight API call ([see API Reference](#api-reference)) are cached in-memory, as they are infrequently updated. See `CommercialResourcesService` [here](../../web/src/Web.App/Services/CommercialResourcesService.cs)

`CommercialResourcesBuilder` groups and sorts the resources. See [here](../../web/src/Web.App/Domain/CommercialResources.cs#L11)

Within each of the views listed above, partials display:

-[Recommended resources](../../web/src/Web.App/Views/School/_RecommendedResources.cshtml)
-[All resources](../../web/src/Web.App/Views/Shared/CommercialResource/AllResources.cshtml)

Resources for each category are managed using view components, view models, and views.

-[View Component](../../web/src/Web.App/ViewComponents/CommercialResourceViewComponent.cs)
-[View Model](../../web/src/Web.App/ViewModels/Components/CommercialResourceViewModel.cs)
-[View Component](../../web/src/Web.App/Views/Shared/Components/CommercialResource/Default.cshtml)

\newpage
