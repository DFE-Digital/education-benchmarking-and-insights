# Financial Benchmarking and Insights Tool: Test Strategy

## Overview

The Financial Benchmarking and Insights Tool (FBIT) test strategy verifies data accuracy, schema compatibility across historical and current pipeline years, API contract stability, and UI accessibility across the monorepo.

## Test Environments & Access

- **Environments:** Local developer environment (`pytest`, `.NET` test runners, Docker Compose with MSSQL and Azurite), Azure DevOps CI pipelines, and Pre-production environments.
- **Access Needs:** Local tests run entirely offline using in-memory DataFrames, `pytest-mock`, or local Docker containers without requiring external cloud credentials.
- **Data Management:** Unit tests use deterministic in-memory fixtures (`conftest.py`); E2E pipeline tests seed local Azurite blob containers and verify rows in a containerised MSSQL database.
- **CI/CD Integration:** Azure DevOps pipelines (`pipelines/`) execute `pytest`, `ruff`, `black`, and `isort` for `data-pipeline`, and `dotnet build` / `dotnet test` for `.NET` projects on every pull request.

## Testing Approach

| Level | Scope / Objective | Tools / Frameworks | Success Criteria | Responsibilities |
|---|---|---|---|---|
| **Unit** | Individual pre-processing functions, schema validations, and SQL projections in isolation. | `pytest`, `pytest-mock`, `xUnit` | 100% pass rate; zero regressions across historical and current year schemas. | Devs |
| **Integration** | Module interactions between ancillary pre-processing, S251/school aggregation, and database insertion helpers. | `pytest`, SQLAlchemy mock connections | Accurate DataFrame merges and SQL projection column mappings. | Devs |
| **Functional** | Verification of feature requirements (`REQ-001` to `REQ-006`) against specification acceptance criteria. | `pytest` | All Given/When/Then acceptance criteria verified by automated assertions. | Devs, QA |
| **End-to-End (E2E)** | Full pipeline execution from Blob Storage input files to MSSQL database tables. | `pytest` (`tests/e2e`), Docker (Azurite, MSSQL) | Pipeline completes and database tables contain expected values. | QA, Devs |
| **Performance (API)** | Load and latency testing for Platform APIs and pipeline batch writes. | Grafana `k6`, SQLAlchemy `fast_executemany` | Batch inserts maintain linear scaling without memory exhaustion. | Devs, QA |
| **Performance (Web)** | Page load and rendering performance for web portal dashboards. | Lighthouse, Playwright | Core Web Vitals within GDS service standards. | Devs, QA |
| **Security** | Static analysis, dependency auditing, and parameterised SQL verification. | `ruff`, SonarQube, GitLeaks | Zero hardcoded secrets; 0 Critical/High vulnerabilities. | Sec, Devs |
| **Accessibility** | WCAG 2.2 AA compliance for front-end components and web views. | `axe-core`, Playwright | Zero WCAG 2.2 Level AA violations (N/A for backend-only data pipeline changes). | QA, UX |

## Risk & Mitigation

| Risk | Mitigation |
|---|---|
| Schema drift across DfE annual data releases (`2024` vs `2026`) | Use year-keyed schema dictionaries in `input_schemas/` and parameterise unit tests across both historical (`2024`/`2025`) and `2026` runs ([ASSUME-001](assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)). |
| Missing or suppressed values (`NP`, `NE`, `SUPP`, `LOWCOV`) in KS4 workbooks | Test explicit coercion to `0.0` for present rows and `np.nan` (`NULL`) for unmatched entities or missing files. |
| Multiple or missing `RECTYPE == 5` National Average rows in KS4 data | Test edge cases where `RECTYPE == 5` is absent (defaults to `np.nan`) or contains one row (broadcasts scalar float to all Local Authority rows). |

## Sign off

| Name | Role | Approved | Date |
|---|---|---|---|
| Engineering Team | Technical Lead | [x] | 2026-10-23 |
