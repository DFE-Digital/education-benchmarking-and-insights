# Platform API Test Plan: LocalAuthority - Accounts

API: LocalAuthority  
Feature: Accounts

## Test Scenarios by Endpoint

### GET local-authorities/accounts/high-needs

- **Versions**: 1.0
- **Validation Rules**:
  - `Codes` must not be empty.
  - `Codes` must have a length of 30 or fewer.
  - `Dimension` must be one of the supported values: `Actuals`, `PerHead`, `PerPupil`.
- **Scenarios**:
  - **200 OK**:
    - Valid request with single `code` and valid `dimension`.
    - Valid request with multiple `code` values (up to 30) and valid `dimension`.
    - Valid request with all supported `dimension` values.
  - **400 Bad Request**:
    - Request with `code` parameter omitted.
    - Request with `code` parameter empty.
    - Request with more than 30 `code` parameters.
    - Request with an unsupported `dimension` value.
    - Request with an invalid `x-api-version` header.

### GET local-authorities/accounts/high-needs/history

- **Versions**: 1.0
- **Validation Rules**:
  - `Codes` must not be empty.
  - `Codes` must have a length of 30 or fewer.
  - `Dimension` must be one of the supported values: `Actuals`, `PerHead`, `PerPupil`.
- **Scenarios**:
  - **200 OK**:
    - Valid request with single `code` and valid `dimension`.
    - Valid request with multiple `code` values (up to 30) and valid `dimension`.
    - Valid request with all supported `dimension` values.
  - **400 Bad Request**:
    - Request with `code` parameter omitted.
    - Request with `code` parameter empty.
    - Request with more than 30 `code` parameters.
    - Request with an unsupported `dimension` value.
    - Request with an invalid `x-api-version` header.
  - **404 Not Found**:
    - Request with a `code` for a local authority that has no associated financial years.
