---
title: "0025: Add a feature flag for Attainment-8 2026"
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: "ADR 0026: Attainment-8 feature flag"
  parent: Architecture Decisions
  order: 25
---

Date: 2026-10-08

## Status

Draft

## Context

ADR 0026: Reason for attainment-8 2026 feature flag

The change from Progress-8 to Attainment-8 is being driven by a data availability issue.  The Progress-8 measure will not be available in 2026 as the KS2 progress data needed to enable the comparison was not captured due to Covid.  Therefore the Progress-8 metrics will be replaced With Attainment-8 from the KS4 publication for 2026 only.

This Decision document will outline the options for enabling the Attainment-8 data to be shown in the FBIT service for 2026 only.

## Decision Options

### Option 1: Specific Attainment-8 code release

This option would require a branch of code to be built that includes the Attainment-8 features (i.e. data pipeline changes to use the attainment-8 metrics and the front-end changes that show these metrics instead of the Progress-8 elements).

The code release to add this branch to main would be done in December when the new dataset is available.

### Option 2: Attainment-8 feature flag

This option would enable to the Attainment-8 branch to be added to the main code base and released ahead of the data becoming available in December but being hidden from the service depending on a feature flag being set.

The Data pipeline elements will not be controlled by the feature flag i.e. they will run and load the values into the service database whether the flag is set or not.

### Option 3: Dynamic Attainment-8 content

This option would enable the Attainment-8 feature to be shown in the service based on the RunID (year) that is currently set for the service.  The RunID is a configuration value which controls the overall data context that should be applied to the FBIT service.  Additional logic would be added to the front-end code controller layer that loads the Progress-8 feature into the service if the RunID for the service is set to 2026.

## Decision

**Option 2.**

This means:

* The Attainment-8 feature content will be shown in the service instead of the Progress-8 feature content if the Attainment-8 feature flag is set.

**Rationale:**

* Minimises complexity - no dynamic content needed for this feature.
* Enables the branch to be merged ahead of the data release
* Streamlines delivery for a short-lived feature i.e. Progress-8 will be available in 2027 and will come back into the service.

## Status
Draft
