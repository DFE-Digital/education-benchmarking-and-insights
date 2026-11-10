---
id: ASSUME-001
title: "Historical KS4 datasets do not require Local Authority or National Attainment 8 extraction"
component: "data-pipeline/ks4"
domain: "data-processing"
tags: ["ks4", "attainment-8", "backward-compatibility", "local-authority"]
status: "validated"
created: 2026-10-23
updated: 2026-10-23
resolved: 2026-10-23
---

## Assumption

Pipeline runs for reporting cycles before 2026 do not require Local Authority or National Average Attainment 8 metrics. Historical KS4 source files might not contain or populate Local Authority (`LEA`) and record type (`RECTYPE`) breakdowns for Local Authority benchmarking. School-level Attainment 8 scores remain valid for all reporting cycles where KS4 school performance data is available.

## Context

The user request states that the Local Authority breakdown and National Average Attainment 8 values are only needed for the 2026 run, and that earlier runs must continue to work without failure. This relates to REQ-002 and REQ-005 in the `populate-attainment8-metrics` feature specification.

## Resolution

Validated by the user on 2026-10-23 (CL-002): School-level Attainment 8 will be populated for all runs where KS4 data exists, while Local Authority and National Average Attainment 8 extraction and persistence will only run on the 2026 reporting cycle onwards.
