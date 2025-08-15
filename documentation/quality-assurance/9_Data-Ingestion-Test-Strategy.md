# Test Strategy: Data Ingestion

## Purpose

This document outlines the strategy for testing **data ingestion** within the FBIT platform.  
Each data ingestion cycle involves receiving structured datasets from upstream providers, processing them through the ingestion pipeline, storing the results in the database, and exposing the data to the service layer for end users.

The goal of this strategy is to ensure that every ingestion is **accurate, complete, and non-disruptive to existing data**.  
This document defines **how ingestion testing is approached**, including the scope, objectives, responsibilities, and validation methods.

## Scope

**In Scope:**

- Validation of all data ingestion across environments
- Verification of:

  - File format, schema, and year/context accuracy
  - Successful pipeline processing and transformation
  - Correct database storage and mapping
  - Accurate reflection of data in the service layer
- Regression checks to ensure no adverse impact on existing data

**Out of Scope:**

- Functional testing of the ingestion pipeline code (covered by CI/CD)
- Verification of raw source data accuracy (owned by upstream providers)

## Goals

- Ensure each ingestion cycle is processed correctly and completely
- Detect:

  - Schema drift or format changes from upstream sources
  - Misalignment or overwriting of existing data
  - Mapping and transformation errors
- Maintain data integrity, traceability, and stakeholder confidence across cycles

## Responsibilities

| Role           | Responsibility                                                               |
|----------------|------------------------------------------------------------------------------|
| Data Engineer  | Prepares input files, executes pipeline, monitors ingestion logs             |
| QA             | Validates file → pipeline → DB → service flow and performs regression checks |
| Product owner  | Confirms data integrity and mapping outcomes meet business rules             |
| Delivery Lead  | Confirms sign-off before Production release                                  |

## Environments

| Environment | Purpose                    | Testing Activity                                         |
|-------------|----------------------------|----------------------------------------------------------|
| local       | Early validation           | Schema checks, small sample runs                         |
| Test        | Functional testing         | End-to-end validation of ingestion with sample/full data |
| Pre Prod    | Pre-production validation  | Regression checks, stakeholder review                    |
| Prod        | Live system                | Smoke checks and final verification                      |

## Test Strategy

**Step 1 – Schema and File Validation:**

- Confirm file formats, columns, and metadata are correct
- Validate year or version context in filenames and headers

**Step 2 – Pipeline Validation:**

- Monitor logs for successful stage completion
- Validate that no rows are dropped or duplicated
- Confirm transformation and aggregation steps are applied correctly

**Step 3 – Database Validation:**

- Verify that data is stored in the correct tables with correct mappings
- Compare row counts and key metrics against input files
- Run regression scripts to ensure historical data is unaffected

**Step 4 – Service/UI Validation:**

- Verify new data appears as expected in the service layer
- Check key metrics, filters, and year switching functionality
- Perform stakeholder review on representative data sets

## Risk Mitigation

| Risk                                      | Mitigation                                                |
|-------------------------------------------|-----------------------------------------------------------|
| Upstream schema or format change          | Validate files pre-ingestion and update mapping if needed |
| Pipeline or job failure                   | Test run locally followed by in test                      |
| Regression in existing data               | Execute 1-2 year regression script                        |

## Supporting Documents

- Data Sources: [`documentation/data/2_Sources.md`](../data/2_Sources.md)
- Releases: [`documentation/data/5_Releases.md`](../data/5_Releases.md)
- Validation Scripts – for schema, mapping, and regression checks *(links to be added later)*

<!-- Leave the rest of this page blank -->
\newpage
