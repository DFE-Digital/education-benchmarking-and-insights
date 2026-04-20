# Platform API Test Plan: School - Comparators

API: School  
Feature: Comparators

## Test Scenarios by Endpoint

### POST schools/{urn}/comparators

- **Versions**: 1.0
- **Validation Rules**: None explicit (Route parameter and body only, `x-api-version` header validated implicitly).
- **Scenarios**:
  - **200 OK**:
    - Request with a valid `urn` and a fully populated characteristics body.
    - Request with a valid `urn` and an empty characteristics body.
    - Request with a valid `urn` and a partially populated characteristics body.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
