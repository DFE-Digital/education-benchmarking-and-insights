# Security Assessment: Populate Attainment 8 Metrics

## 1. Data Classification

- **Data Processed:** Aggregate Key Stage 4 (KS4) Attainment 8 scores (`ATT8SCR`), Record Type codes (`RECTYPE`), Local Authority codes (`LEA`), and school Unique Reference Numbers (`URN`).
- **Classification Level:** **Public / Official Statistics** (Department for Education published school and Local Authority performance tables).
- **Sensitive Data / PII:** None. No pupil-level, staff-level, or personally identifiable information (PII) is ingested, processed, or stored.

## 2. Threat Modelling (STRIDE)

| Threat Category | Potential Threat | Mitigation |
|---|---|---|
| **Spoofing** | Unauthorised process writes fabricated KS4 files to Blob Storage or SQL Database. | Blob Storage and Azure SQL access are restricted via managed identities and environment-scoped connection strings inside the private VNet. |
| **Tampering** | Malformed or non-numeric strings in `ATT8SCR`, `LEA`, or `RECTYPE` cause SQL injection or type corruption. | Strict schema enforcement via `input_schemas.ks4` (`Int64` and `float`), `pd.to_numeric(..., errors="coerce")`, and SQLAlchemy parameterised `INSERT` execution (`fast_executemany=True`). |
| **Repudiation** | Inability to trace which pipeline run populated specific Attainment 8 records. | Every row in `dbo.NonFinancial` and `dbo.LocalAuthorityNonFinancial` is keyed by `RunType` and `RunId` with structured pipeline logging. |
| **Information Disclosure** | Leakage of database credentials or connection strings in logs or exceptions. | `database.py` logs only `run_id` and `run_type` metadata; connection strings are read exclusively from environment variables and never logged. |
| **Denial of Service** | Missing columns in historical KS4 files or missing `RECTYPE == 5` rows crash the pipeline worker. | Year-keyed input schema evolved via `evolve_schema` (`"default"` vs `2026`), defensive `.empty` checks before extracting `RECTYPE == 5` scalar values, and `ensure_attainment8_columns_are_present` fallback columns. |
| **Elevation of Privilege** | Pipeline executes with excessive database privileges. | Pipeline uses existing least-privilege database credentials and only inserts into `dbo.NonFinancial` (`Attainment8`) and `dbo.LocalAuthorityNonFinancial` (`LocalAuthorityAvgAttainment8`, `NationalAvgAttainment8`), whose schema changes were deployed via DbUp scripts `141` through `146`. |

## 3. Authentication & Authorisation

- **Pipeline Execution:** Triggered internally via Azure Queue Storage messages in an isolated Azure Function environment.
- **Database Access:** Uses existing least-privilege service credentials configured via `DB_CONNECTION_STRING`. No changes to authentication or authorisation boundaries are introduced.

## 4. Security Controls

- **Input Validation:**
  - Excel inputs are validated against `input_schemas.ks4` column dtypes (`Int64`, `float`, `string`).
  - Non-numeric suppression markers (`NP`, `NE`, `SUPP`, `LOWCOV`) are stripped and coerced safely to floating-point values.
- **Database Persistence:**
  - All SQL insertions use SQLAlchemy `sqlalchemy.table` and `sqlalchemy.column` constructs with parameterised batch binding (`conn.execute(insert_statement, rows)`). No dynamic SQL string concatenation is used.

## 5. Compliance & Privacy

- **GDPR / UK Data Protection Act 2018:** Not applicable to aggregate institutional and regional attainment statistics.
- **GOV.UK / NCSC Secure Development Guidelines:** Complies with secure data handling and parameterised database query standards.
