# Platform API Test Plan: LocalAuthority - Search

API: LocalAuthority  
Feature: Search

## Test Scenarios by Endpoint

### POST local-authorities/suggest

- **Versions**: 1.0
- **Validation Rules**:
  - **SuggesterName**: Must not be null.
  - **SearchText**: Must not be null, must be between 3 and 100 characters in length.
  - **Size**: Must be greater than or equal to 5.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `SearchText` (between 3 and 100 characters) and valid `Size` (>= 5).
    - Request with valid `SearchText` and default `Size`.
  - **400 Bad Request**:
    - Request with a null `SuggesterName` (if overridden or bypassable).
    - Request with a null `SearchText`.
    - Request with a `SearchText` length less than 3 characters.
    - Request with a `SearchText` length greater than 100 characters.
    - Request with a `Size` less than 5.
    - Request with an invalid `x-api-version` header.

### POST local-authorities/search

- **Versions**: 1.0
- **Validation Rules**:
  - **SearchText**: Must not be null, must be between 3 and 100 characters in length (inherited from `PostSearchRequestValidator`).
  - **OrderBy.Value**: Must be null or one of the supported values: `asc`, `desc` (inherited from `PostSearchRequestValidator`).
  - **OrderBy.Field**: Must be null or exactly `LocalAuthorityNameSortable`.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `SearchText` (between 3 and 100 characters) and no `OrderBy` specified.
    - Request with valid `SearchText` and valid `OrderBy` (`Field` = `LocalAuthorityNameSortable`, `Value` = `asc`).
    - Request with valid `SearchText` and valid `OrderBy` (`Field` = `LocalAuthorityNameSortable`, `Value` = `desc`).
  - **400 Bad Request**:
    - Request with a null `SearchText`.
    - Request with a `SearchText` length less than 3 characters.
    - Request with a `SearchText` length greater than 100 characters.
    - Request with an invalid `OrderBy.Value` (not `asc` or `desc`).
    - Request with an invalid `OrderBy.Field` (not `LocalAuthorityNameSortable`).
    - Request with an invalid `x-api-version` header.
