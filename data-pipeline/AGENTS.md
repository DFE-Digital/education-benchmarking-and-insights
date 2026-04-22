# Data Pipeline: Agent Mandates

This file defines AI-specific operational guidelines for agents working within the `data-pipeline` module.
For all human-readable architecture, tech stack, development standards, and anti-patterns, refer to the `README.md` in this directory.

## AI Operational Guidelines

- **No Direct SQL Execution**: Do not write or execute raw SQL INSERT/UPDATE statements; use the existing SQLAlchemy-based abstractions.
- **Testing Mocks**: When writing unit tests for the engine, use static Pandas DataFrames or mocks. Do not write tests that require live DB or Storage connections.
