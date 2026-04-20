# Platform API Test Plan: School - MetricRagRatings

API: School  
Feature: MetricRagRatings

## Test Scenarios by Endpoint

### GET schools/metric-rag-ratings

- **Versions**: 1.0
- **Validation Rules**:
  - `OverallPhase` must be a valid overall phase if provided.
  - At least one of `LaCode`, `Urns`, or `CompanyNumber` must be provided.
- **Scenarios**:
  - **200 OK**:
    - Valid request providing `LaCode`.
    - Valid request providing `Urns`.
    - Valid request providing `CompanyNumber`.
    - Valid request providing `Urns` and a valid `OverallPhase`.
  - **400 Bad Request**:
    - Request providing an invalid `OverallPhase`.
    - Request missing all of `LaCode`, `Urns`, and `CompanyNumber`.
    - Request with an invalid `x-api-version` header.

### GET schools/metric-rag-ratings/details

- **Versions**: 1.0
- **Validation Rules**:
  - `Categories` must contain valid cost categories if provided.
  - `Statuses` must contain valid RAG status values if provided.
  - Either a collection of `Urns` or a `CompanyNumber` must be provided.
- **Scenarios**:
  - **200 OK**:
    - Valid request providing a collection of `Urns`.
    - Valid request providing a `CompanyNumber`.
    - Valid request providing `Urns`, `Categories` and `Statuses`.
  - **400 Bad Request**:
    - Request with invalid `Categories`.
    - Request with invalid `Statuses`.
    - Request missing both `Urns` and `CompanyNumber`.
    - Request with an invalid `x-api-version` header.

### GET schools/user-defined/{identifier}/metric-rag-ratings (DEFERRED)

*Note: Testing for this endpoint is currently deferred as there is no user-defined test data available.*

- **Versions**: 1.0
- **Validation Rules**:
  - None explicit.
- **Scenarios**:
  - **200 OK**:
    - Valid request with an identifier and default data context.
    - Valid request with an identifier and `useCustomData=true`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
