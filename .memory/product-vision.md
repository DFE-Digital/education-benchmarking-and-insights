# Product Vision: Financial Benchmarking and Insights Tool (FBIT)

**Date**: 2026-10-23
**Status**: Live
**Target Audience**: Product Managers, Engineering Teams, Data Analysts, and Department for Education (DfE) Stakeholders

## 1. Vision Statement

The Financial Benchmarking and Insights Tool (FBIT) provides a single, unified service for managing and comparing financial and performance data across schools, academy trusts, and Local Authorities in England.

## 2. Core Objectives

- **Benchmarking**: Enable schools and Local Authorities to compare resource use against similar organisations.
- **Insights**: Allow users to interrogate financial and non-financial data to identify planning improvements.
- **Outcomes**: Support better financial planning to improve pupil educational outcomes.
- **Accountability**: Support school governors, academy trustees, and Local Authority leaders in their oversight roles.

## 3. Target Users

- **School Business Professionals and Headteachers**: Review school spending alongside pupil demographics and academic outcomes.
- **Academy Trust Leaders and Chief Financial Officers**: Monitor financial health and educational performance across trust schools.
- **Local Authority Analysts and Commissioners**: Evaluate Section 251 expenditure, High Needs provision, and Local Authority educational performance relative to statistical neighbours and national averages.
- **Governors and Trustees**: Exercise financial and educational oversight using transparent public benchmarks.

## 4. Constraints and Guardrails

- **Historical Run Compatibility**: Data processing pipelines must support both the current reporting cycle and historical reporting runs without regressions ([ASSUME-001](assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)).
- **Data Accuracy**: All published metrics must derive from official DfE statistical releases (such as CFR, AAR, Section 251, SEN2, KS2, and KS4 datasets).
- **Accessibility and Standards**: All user-facing outputs must comply with GOV.UK Design System standards and WCAG 2.2 Level AA accessibility requirements.

## Value Rule Set

### Organisational Value Definition

For the Department for Education (DfE) and FBIT stakeholders, organisational value means delivering accurate, timely, and actionable financial and educational benchmarking data that empowers schools, trusts, and Local Authorities to optimise resource allocation for better pupil outcomes ([KB-0001](knowledge/KB-0001-fbit-value-rule-set.md)).

### Evaluation Criteria

| Rule / Criterion | Description | Minimum Threshold for Go |
| :--- | :--- | :--- |
| **Pupil Outcome Alignment** | Enables correlation between financial expenditure and official pupil educational outcomes ([ASSUME-002](assumptions/ASSUME-002-attainment8-benchmarking-value.md)). | Directly exposes or persists an official DfE outcome metric for school or Local Authority benchmarking. |
| **Benchmarking Completeness** | Provides necessary comparative context (such as Local Authority and National averages) for valid comparisons. | Supplies both organisation-level and comparator/national average metrics where applicable. |
| **Historical Continuity** | Maintains operational compatibility with prior financial and academic year pipeline runs. | Zero regressions on pre-2026 pipeline runs when introducing 2026 reporting metrics. |
| **Single Source of Truth** | Derives metrics directly from authoritative DfE input datasets without manual data manipulation. | 100% automated ingestion and persistence within the standard pipeline workflow. |
