# Data Pipeline: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `data-pipeline` module.

## Development Standards

- **Pandas-First**: Use vectorized Pandas operations; avoid iterating over rows (`iterrows`) to maintain performance.
- **Parquet Intermediates**: Always save intermediate DataFrames to Blob Storage as Parquet to enable re-runs, debugging, and data lineage.
- **Schema Validation**: All raw data must be validated against `pipeline.input_schemas` before processing to catch source data regressions early.
- **Pure Logic Engines**: Keep calculation logic (RAG ranking, Similarity algorithms) independent of I/O to facilitate unit testing with mocks or local DataFrames.
- **Logging & Stats**: Utilize `setup_logger` and `stats_collector` for consistent observability and performance tracking across all pipeline stages.
- **Local Environment**: Rely on `make install` for setup and `.env` (derived from `.env-example`) for local configuration overrides.

## Anti-Patterns

- **In-Memory SQL Joins**: Avoid pulling massive DB tables to join in Pandas; push joins to the database or use indexed Parquet files from previous stages.
- **Hardcoded Year Logic**: Never hardcode academic or financial years; always derive them from the queue message payload or configuration.
- **Raw SQL Strings**: Do not write manual `INSERT` or `UPDATE` statements; use the existing SQLAlchemy-based abstractions in `pipeline.utils.database`.
- **Mixing I/O in Engines**: The core calculation engines (RAG, Comparators) should never call Azure SDKs or Database engines directly; pass data as DataFrames or Dictionaries.
- **Testing I/O in Unit Tests**: Do not write unit tests that require an active Azure Storage or SQL Server connection. Use the `tests/e2e` folder for integration tests and keep `tests/unit` strictly isolated using mocks or static Pandas DataFrames.
