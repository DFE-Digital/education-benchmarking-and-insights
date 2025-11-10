# Developer Feature Documentation: Progress 8 Data

## Introduction

This document provides detailed information for developers about the implementation, usage, and integration of the Progress 8 Data feature within the system.

## Overview

As per [#283987](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/283987), the requirement to surface Progress 8 data for KS4 schools involves updates to the client-rendered charts, tables and tooltips. This is to honour the use case:

> As a School Business Professional
>
> I want to view Progress 8 data for KS4 schools on the school Benchmark spending page
>
> so that I can identify the schools in my comparator set achieving good educational outcomes

## Goals

### Primary Goal

Retrieve progress data alongside existing financial data to display in client side charts via:

- Check boxes to toggle visibility of 'Above average' and 'Well above average' values
- Chart bar colour updates when any comparators matching toggled check box(es) are rendered
- Addition of legend (key) when any comparators matching toggled check box(es) are rendered
- Modifications to tooltip when any comparators matching toggled check box(es) are rendered
- Additional table column when any comparators matching toggled check box(es) are rendered

Progress data should be displayed for:

- Default comparator sets (Pupil and Building)
- User defined comparator set (if logged in and configured)
- Custom data derived comparator set (if logged in and configured)

### Secondary Goals

Progress data requested once per group of comparator sets (data also required by server rendered views) so as to reduce API call overhead.

Other pages that require progress data (e.g. School home page) should be able to do so without much additional development overhead.

## Prerequisites/Dependencies

The Progress 8 bandings are persisted in the `NonFinancial` table, as managed by the data pipeline.

## Usage

The Insight API School(s) Characteristics endpoints include the new `KS4ProgressBanding` property alongside other non-financial data. This is called from a controller via the new `ProgressService` and a mapping between URN and banding for each school in the set built and rendered to a `data-` attribute, ready to be resolved by the client component. It is also available elsewhere in the relevant view model for consumption by server rendered views (for introductory text).

The client rendered charts for each cost category makes its expenditure `fetch()` and upon success, the progress data is merged, keyed off the URN. The chart bars, tooltips, legend and table changes may then be resolved as if the progress data was always alongside the expenditure data. When the user changes dimension, the new expenditure data is merged again with the progress data. When the view is changed to 'table' or the toggle checkboxes are selected no side effects of additional API calls are made.

### Flow diagram (server)

```mermaid
sequenceDiagram
    accDescr: Progress 8 data (server)
    
    participant School comparator page
    participant School comparison controller
    participant Comparator set API
    participant User data service
    participant Progress bandings service
    participant School characteristics API

    Note over School comparator page: User makes request
    School comparator page->>School comparison controller: URN
    activate School comparison controller
    School comparison controller->>Comparator set API: Get default comparator set
    activate Comparator set API
    Comparator set API->>School comparison controller: 
    deactivate Comparator set API
    School comparison controller->>User data service: Check user defined comparator set
    activate User data service
    User data service->>School comparison controller:
    deactivate User data service
    alt has user defined comparator set
        School comparison controller->>Comparator set API: Get user defined comparator set
        activate Comparator set API
        Comparator set API->>School comparison controller: 
        deactivate Comparator set API
        School comparison controller->>Progress bandings service: Get user defined comparator set bandings
        activate Progress bandings service
    else
        School comparison controller->>Progress bandings service: Get default comparator set bandings across Pupil and Building sets
    end
    Progress bandings service->>School characteristics API: Get characteristics
    activate School characteristics API
    School characteristics API->>Progress bandings service: Parsed dictionary
    deactivate School characteristics API
    Progress bandings service->>School comparison controller:
    deactivate Progress bandings service
    School comparison controller->>School comparator page: Render to view
    deactivate School comparison controller
```

### Flow diagram (client)

```mermaid
sequenceDiagram
    accDescr: Progress 8 data (client)
    
    participant School comparator page
    participant React
    participant Expenditure API

    School comparator page->>React: Deserialises progress data
    activate React
    alt has bandings in Above Average or Well Above Average
         Note over React: Render check boxes
    end
    loop Each cost category component
    React->>Expenditure API: fetch() category spend
    activate Expenditure API
    Expenditure API->>React: Merge with progress data
    end
    Note over React: Render chart/table with progress data
    deactivate React
```

## Deployment

Enabled via the `KS4ProgressBanding` feature flag.

<!-- Leave the rest of this page blank -->
\newpage
