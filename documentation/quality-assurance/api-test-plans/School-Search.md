# Platform API Test Plan: School - Search

API: School  
Feature: Search  

## Test Scenarios by Endpoint

### POST schools/search

- **Versions**: 1.0
- **Validation Rules**:
  - `SearchText` must be not null, minimum length 3, maximum length 100.
  - `OrderBy.Value` must be 'asc' or 'desc' if provided.
  - `OrderBy.Field` must be 'SchoolNameSortable' if OrderBy is provided.
  - `Filters` list must only contain items where `Field == "OverallPhase"`.
  - `Filters` items' `Value` must be a valid `OverallPhase`.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `SearchText` and no other optional fields.
    - Request with valid `SearchText` and valid pagination (`Page`, `PageSize`).
    - Request with valid `SearchText` and valid `OrderBy` (`Field` = "SchoolNameSortable", `Value` = "asc").
    - Request with valid `SearchText` and valid `Filters` (`Field` = "OverallPhase", `Value` = "Primary").
  - **400 Bad Request**:
    - Request with `SearchText` missing or null.
    - Request with `SearchText` less than 3 characters.
    - Request with `SearchText` greater than 100 characters.
    - Request with `OrderBy.Field` not equal to "SchoolNameSortable".
    - Request with `OrderBy.Value` not 'asc' or 'desc'.
    - Request with `Filters.Field` not equal to "OverallPhase".
    - Request with `Filters.Value` not a valid overall phase.
    - Request with an invalid `x-api-version` header.

### POST schools/suggest

- **Versions**: 1.0
- **Validation Rules**:
  - `SearchText` must be not null, minimum length 3, maximum length 100.
  - `Size` must be greater than or equal to 5.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `SearchText` and no other optional fields.
    - Request with valid `SearchText` and a valid `Size` (e.g., 10).
    - Request with valid `SearchText` and valid `Exclude` array.
    - Request with valid `SearchText` and `ExcludeMissingFinancialData` set to true.
  - **400 Bad Request**:
    - Request with `SearchText` missing or null.
    - Request with `SearchText` less than 3 characters.
    - Request with `SearchText` greater than 100 characters.
    - Request with `Size` less than 5.
    - Request with an invalid `x-api-version` header.
