# Platform API Test Plan: LocalAuthority - EducationHealthCarePlans

API: LocalAuthority  
Feature: EducationHealthCarePlans

## Test Scenarios by Endpoint

### GET local-authorities/education-health-care-plans

- **Versions**: 1.0
- **Validation Rules**:
  - **Codes**: Must not be empty, must contain between 1 and 30 local authority codes.
  - **Dimension**: Must be empty or one of the supported values: Actuals, Per1000, Per1000Pupil.
- **Scenarios**:
  - **200 OK**:
    - Request with valid local authority codes (e.g., 1 to 30 codes) and a valid `Dimension`.
    - Request with valid local authority codes and an empty/missing `Dimension`.
  - **400 Bad Request**:
    - Request with empty `Codes` parameter.
    - Request with more than 30 `Codes`.
    - Request with an invalid `Dimension`.
    - Request with an invalid `x-api-version` header.

### GET local-authorities/education-health-care-plans/history

- **Versions**: 1.0
- **Validation Rules**:
  - **Codes**: Must not be empty, must contain between 1 and 30 local authority codes.
  - **Dimension**: Must be empty or one of the supported values: Actuals, Per1000, Per1000Pupil.
- **Scenarios**:
  - **200 OK**:
    - Request with valid local authority codes (e.g., 1 to 30 codes) and a valid `Dimension` that returns historical data.
    - Request with valid local authority codes and an empty/missing `Dimension` that returns historical data.
  - **400 Bad Request**:
    - Request with empty `Codes` parameter.
    - Request with more than 30 `Codes`.
    - Request with an invalid `Dimension`.
    - Request with an invalid `x-api-version` header.
  - **404 Not Found**:
    - Request with valid local authority codes where no historical data or matching years exist.
