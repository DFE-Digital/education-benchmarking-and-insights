# Strict Platform API & Feature Discovery Rules

When executing commands that target Platform APIs and Features, you **MUST** strictly adhere to the following discovery and validation rules. This is a critical safety and scope-limiting measure to prevent unintended modifications across the monorepo.

## 1. Explicit Dual-Input Required

- You **MUST NOT** proceed without explicit input defining **BOTH** the target API and the target Feature.
- **Strict Requirement:** If either the API name or the Feature name is missing, STOP immediately. Do not guess, do not default to a previously used context, and do not try to find a feature with a similar name in another API.
- **Action:** Ask the user to clarify by providing both (e.g., 'School Search' or 'LocalAuthority Details').

## 2. Combined Path Mapping

- **Mapping**: Translate the user's input into a specific directory path: `platform/src/apis/Platform.Api.[API_NAME]/Features/[Feature_Name]`.
- **API Resolution**: Prefer PascalCase for the API name (e.g., `School`, `Trust`, `Establishment`).
- **Feature Resolution**: The feature name should correspond to a directory within the API's `Features/` folder.

## 3. Strict Verification (STOP Condition)

- **Validation**: Before proceeding with any detailed research or modification, you **MUST** use the `list_directory` or `glob` tool to verify that the exact combined path exists.
- **Failure**: If the directory `platform/src/apis/Platform.Api.[API_NAME]/Features/[Feature_Name]` cannot be found, you **MUST STOP**.
- **Clarification**: Prompt the user with a list of available APIs (from `platform/src/apis/`) or available features within the chosen API if they are unsure.

## 4. Absolute Scope Containment

- Once the path is validated, you **MUST strictly limit your operations** to that specific verified feature directory.
- You are prohibited from reading, analyzing, or modifying files outside of this specific feature boundary unless the task instructions explicitly require reading from the shared `abstractions` or `Platform.ApiTests` projects for configuration or reference.
