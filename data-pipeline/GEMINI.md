# Data Pipeline Module

## Module Purpose
Automates the end-to-end data processing lifecycle for school financial benchmarking, transforming raw educational and financial datasets into actionable insights and performance ratings. It ensures that complex data from multiple DfE sources is cleaned, linked, and benchmarked consistently for use in the FBIT web application.

## Tech Stack
- **Language:** Python 3.11+
- **Data Engineering:** Pandas (Transformations), PyArrow (Parquet)
- **Database:** MS SQL Server (via SQLAlchemy & PyODBC)
- **Messaging:** Azure Storage Queues (Run triggers and completion signals)
- **Storage:** Azure Blob Storage (Raw input and intermediate Parquet persistence)
- **Dependency Management:** Poetry
- **Testing & QA:** pytest, coverage, black, isort, pre-commit

## Entry Points
- **Pipeline Runner:** `src/pipeline/main.py` is the main entry point, handling message ingestion from Azure Storage Queues and routing to specific pipeline runs.
- **Pre-processing:** `src/pipeline/pre_processing/main.py` orchestrates the cleaning and joining of raw data sources (AAR, BFR, CFR, S251, GIAS).
- **Comparator Sets:** `src/pipeline/comparator_sets/orchestrator.py` manages the calculation of peer groups for schools.
- **RAG Engine:** `src/pipeline/rag/orchestrator.py` triggers the benchmarking logic and Red-Amber-Green status assignments.

## Core Logic & Data Flow
1. **Trigger:** A message arrives in the `fbit-worker-queue`. If its `dequeue_count` exceeds the retry limit, it is routed to a dead-letter queue. Otherwise, it triggers the pipeline based on the `runType` (Default, Custom, UserDefined) and data years involved.
2. **Pre-processing (Stage 1):** Raw files are read from Blob Storage, cleaned according to `input_schemas`, and joined into unified DataFrames. Results are saved as Parquet.
3. **Comparator Sets (Stage 2):** Schools are grouped into sets of ~50 similar entities based on structural characteristics (e.g., pupil numbers, building size, phase).
4. **RAG Calculation (Stage 3):** Performance metrics are calculated and ranked against the respective comparator sets to determine RAG ratings (1-30, 31-70, 71-100 percentiles).
5. **Persistence:** Intermediate results are saved as Parquet files in Blob Storage; final results are upserted into the MS SQL Database via `pipeline.utils.database`.
6. **Notification:** A success/failure message is posted to the `fbit-complete-queue` with duration statistics.

## Key Definitions
- **MessageType:** Enum in `pipeline.utils.message` determining the pipeline execution path (Default, Custom, UserDefined).
- **ComparatorCalculator:** Core class in `pipeline.comparator_sets.calculations` that implements similarity algorithms.
- **_run_rag_computation_engine:** Pure function in `pipeline.rag.engine` that performs ranking and metric calculations.
- **input_schemas:** Declarative column definitions used to validate and cast raw input data (e.g., `aar_academies.py`).
- **dead_letter_queue:** An Azure Queue for messages that fail processing after a defined number of retries (tracked via `dequeue_count`).
- **stats_collector:** A singleton from `pipeline.utils.stats` used across the pipeline to aggregate row counts, execution durations, and metadata before emitting them to the completed queue.

## Integration Points
- **Core Infrastructure:** Manages Azure Container Apps via the local `terraform/` directory, while relying on shared Azure SQL and Storage Accounts provisioned by `core-infrastructure`.
- **Web / Platform:** Consumes the processed data and RAG results stored in the SQL database for frontend display.
- **External Data:** Consumes raw CSV/Excel exports from DfE systems (AAR, GIAS, CDC, etc.) stored in the `raw` container of Azure Blob Storage.

## Development Standards
- **Pandas-First:** Use vectorised Pandas operations; avoid iterating over rows (`iterrows`) to maintain performance.
- **Parquet Intermediates:** Always save intermediate DataFrames to Blob Storage as Parquet to enable re-runs, debugging, and data lineage.
- **Schema Validation:** All raw data must be validated against `pipeline.input_schemas` before processing to catch source data regressions early.
- **Pure Logic Engines:** Keep calculation logic (RAG ranking, Similarity algorithms) independent of I/O to facilitate unit testing with mocks or local DataFrames.
- **Logging & Stats:** Utilize `setup_logger` and `stats_collector` for consistent observability and performance tracking across all pipeline stages.
- **Local Environment:** Rely on `make install` for setup and `.env` (derived from `.env-example`) for local configuration overrides.

## Anti-Patterns
- **In-Memory SQL Joins:** Avoid pulling massive DB tables to join in Pandas; push joins to the database or use indexed Parquet files from previous stages.
- **Hardcoded Year Logic:** Never hardcode academic or financial years; always derive them from the queue message payload or configuration.
- **Raw SQL Strings:** Do not write manual `INSERT` or `UPDATE` statements; use the existing SQLAlchemy-based abstractions in `pipeline.utils.database`.
- **Mixing I/O in Engines:** The core calculation engines (RAG, Comparators) should never call Azure SDKs or Database engines directly; pass data as DataFrames or Dictionaries.
- **Testing I/O in Unit Tests:** Do not write unit tests that require an active Azure Storage or SQL Server connection. Use the `tests/e2e` folder for integration tests and keep `tests/unit` strictly isolated using mocks or static Pandas DataFrames.
