---
title: "0025: LAA Lagging Datasets"
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: "ADR 0025: Lagging Datasets"
  parent: Architecture Decisions
  order: 25
---

Date: 2026-10-08

## Status

Draft

## Context

ADR 0025: Strategy for Handling Lagging Data Sets in Annual LAA Data Updates

The Local Authority Analysis (LAA) tool relies on data from the Consistent Financial Reporting (CFR) data set alongside several ancillary data sources (such as school capacity and pupil absence data).
When releasing the LAA feature for the 2026 reporting cycle, the core CFR 2026 data is available. However, certain ancillary data sets lag behind and are only updated publicly in the second quarter of the year (around March-2027).
Historically (in the legacy LARAT tool), updates were handled iteratively: an initial release was made when the CFR data became available (around December), populating lagging metrics using the previous year’s available values. A second data refresh was performed later in the year once all ancillary data sources had been published.

## Decision Options
Option 1: Two-Stage Annual Release (Historical Pattern)

Initial release using newly available CFR data.
For lagging metrics (specifically capacity and pupil absence), fall back to the most recent available historical data (i.e. previous year's values for capacity and Autumn-term only values for pupil absence).  
Execute a second release in Q2 (approx. March) once lagging ancillary data sets are published.
The impact of this is thought to low due to the Capacity value being a relatively slow moving data set and pupil absence being correct at the point of update.

Option 2: Single Synchronized Release

Defer the annual LAA update release until all ancillary data sets are published (March-2027).
Perform a single annual data release with all metrics fully in sync.

Option 3: Blanking Lagging Metric

Release the CFR data immediately upon availability, but set lagging metrics (capacity) to empty/null values until the updated data set is released.  Pupil absence would be updated to use the Autumn term only publication in this option.  The overall score for the school would be reduced by 1.5 while this metric is not available.

Status
Proposed (Pending client sign-off)