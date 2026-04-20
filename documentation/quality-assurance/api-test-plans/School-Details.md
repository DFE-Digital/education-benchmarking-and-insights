# Platform API Test Plan: School - Details

API: School  
Feature: Details

## Test Scenarios by Endpoint

### GET schools/{urn}

- **Versions**: 1.0
- **Validation Rules**:
  - None explicit
- **Scenarios**:
  - **200 OK**:
    - Request with a valid existing `urn`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
  - **404 Not Found**:
    - Request with a `urn` that does not exist in the database.

### GET schools

- **Versions**: 1.0
- **Validation Rules**:
  - None explicit
- **Scenarios**:
  - **200 OK**:
    - Request with one or more valid `urns` in the query parameters.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.

### GET schools/{urn}/characteristics

- **Versions**: 1.0
- **Validation Rules**:
  - None explicit
- **Scenarios**:
  - **200 OK**:
    - Request with a valid existing `urn`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
  - **404 Not Found**:
    - Request with a `urn` that does not exist in the database.
