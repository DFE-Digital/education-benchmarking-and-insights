# Security Assessment: Canonical Schema Timeline and Database Projection Refactor

**Target Plan:** `.memory/specs/canonical-schema-timeline-refactor/proposal.md` | **Context:** *Internal refactoring of Python `data-pipeline` input schema resolution, canonical column mapping, and SQL table projections.*

## 1. Context Validation
| Attribute | Declared | Assessment | Match? |
|---|---|---|---|
| Sensitivity | *Internal / Official-Sensitive (School & Trust Financial Data, CFO & GIAS Head Teacher Contact Details)* | *Internal / Official-Sensitive (GIAS `HeadEmail` is cleared to `""` in pre-processing; CFO names and High Exec Pay bands are processed)* | *✅* |

## 2. CIA Triad
| Pillar | Status | Notes | Controls |
|---|---|---|---|
| Confidentiality | *Pass* | Refactor does not expose new fields or alter PII redaction (`Email` and `HeadEmail` remain cleared to `""` in `calculations.py` and `academies.py`). | Strict column allow-listing via `usecols` and `projections`. |
| Integrity | *Pass* | Step-function lookup (`resolve_year_config`) and `DatasetTimeline` prevent silent fallback to legacy `"default"` schemas on new years and remove duplicate `eval_cost_income_categories` execution. | Explicit `KeyError` on unconfigured years (`< min_year`) and strict dtype enforcement. |
| Availability | *Pass* | In-memory DataFrame projection and schema resolution add negligible overhead ($O(1)$ dictionary resolution per dataset). | Covered by unit and integration test suites. |

## 3. Data Flow
| Flow | Source | Dest | Type | Trust Boundary? | Protocol | Controls |
|---|---|---|---|---|---|---|
| Raw DfE Dataset Ingestion | Azure Blob Storage (`raw` container) | `data-pipeline` Pandas DataFrame | CSV / XLSX / ODS | Yes (External DfE files to Internal Worker) | HTTPS (Azure SDK) | Explicit `usecols` allow-list and `dtype` coercion per `DatasetTimeline`. |
| Processed Data Persistence | `data-pipeline` Pandas DataFrame | Azure SQL Database (`dbo.*` tables) | Tabular SQL Rows | Yes (Worker to Database) | TDS over TLS 1.2+ | SQLAlchemy parameterised `INSERT` via `_write_data` and `apply_db_projection` allow-list. |

## 4. Threat Assessment
* **Expression Injection via `df.eval()`**: `pandas.DataFrame.eval()` parses string expressions. All `column_eval` expressions in `input_schemas/` are static developer-authored constants in source control and never incorporate user input or external file values. Reducing `df.eval()` usage in favour of declarative column rewires and static arithmetic further reduces this surface.
* **Silent Schema Mis-Mapping on Annual Rollover**: Previously, omitting a year key caused `.get(year, schema["default"])` to silently apply a legacy schema, risking subtle data corruption if column semantics shifted. Moving to `resolve_year_config` (`<= year`) and fail-fast `KeyError` when no prior schema exists mitigates silent mis-mapping.

## 5. Compliance
* **Zero Trust / Secure by Design / Anti-Patterns:** *Pass*. Upholds **Data Minimisation** (only columns explicitly declared in `DatasetTimeline` are read from raw files via `usecols`, and only columns declared in `pipeline/config/projections.py` are written to SQL) and **Secure Defaults** (missing historical columns default deterministically to `np.nan` / SQL `NULL`).

## 6. Required Remediation
### A. Architecture / B. Requirements / C. Anti-Patterns
* [ ] **[Secure Defaults - Fail-Fast Resolution]:** Ensure `resolve_year_config` raises a `KeyError` when no integer key $\le \text{year}$ and no `"default"` key exists, rather than returning `None` (*Rationale:* NCSC Secure by Design - Fail Safely).
* [ ] **[Data Minimisation - Strict Projection Allow-List]:** Ensure `apply_db_projection` selects strictly `[*projections.values()]` so unprojected internal working columns never reach `_write_data` (*Rationale:* NCSC Data Minimisation).

## 7. Remediation Tasks
- [ ] [Security] [Global] Enforce fail-fast `KeyError` in `resolve_year_config` when neither a matching floor year (`<= year`) nor `"default"` is present (`TC-06`).
- [ ] [Security] [Global] Restrict `apply_db_projection` output strictly to target columns declared in `pipeline/config/projections.py` (`TC-05`).
