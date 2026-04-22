# Platform API Test Plan: LocalAuthority - StatisticalNeighbours

API: LocalAuthority  
Feature: StatisticalNeighbours

## Test Scenarios by Endpoint

### GET local-authorities/{code}/statistical-neighbours

- **Versions**: 1.0
- **Validation Rules**:
  - None explicit
- **Scenarios**:
  - **200 OK**:
    - Request with a valid local authority code that returns statistical neighbours data.
  - **400 Bad Request**:
    - Request with an invalid or unsupported `x-api-version` header.
  - **404 Not Found**:
    - Request with a non-existent local authority code, or one that returns no statistical neighbours data.
