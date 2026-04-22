# Platform API Test Plan: LocalAuthority - Details

API: LocalAuthority  
Feature: Details

## Test Scenarios by Endpoint

### GET local-authorities

- **Versions**: 1.0
- **Validation Rules**:
  - None explicit
- **Scenarios**:
  - **200 OK**:
    - Request for all local authorities.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.

### GET local-authorities/{code}

- **Versions**: 1.0
- **Validation Rules**:
  - None explicit
- **Scenarios**:
  - **200 OK**:
    - Request with a valid local authority code.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
  - **404 Not Found**:
    - Request with a non-existent local authority code.

### GET local-authorities/{code}/maintained-schools/finance

- **Versions**: 1.0
- **Validation Rules**:
  - **Dimension**: Must be one of: Actuals, PerUnit, PercentExpenditure, PercentIncome.
  - **OverallPhase**: Empty or one of: Primary, Secondary, Special, Pupil Referral Unit, All-through, Nursery, Post-16, Alternative Provision, University Technical College.
  - **Limit**: Empty or a number between 1 and 100.
  - **NurseryProvision**: Empty or one of: Has Nursery Classes, No Nursery Classes, Not applicable, Not recorded.
  - **SixthFormProvision**: Empty or one of: Has a sixth form, Does not have a sixth form, Not applicable, Not recorded.
  - **SpecialClassesProvision**: Empty or one of: Has Special Classes, No Special Classes, Not applicable, Not recorded.
  - **SortField**: One of: SchoolName, TotalPupils, TotalExpenditure, TotalTeachingSupportStaffCosts, RevenueReserve.
  - **SortOrder**: One of: ASC, DESC.
- **Scenarios**:
  - **200 OK**:
    - Request with a valid local authority code and valid query parameters.
    - Request with only required query parameters.
  - **400 Bad Request**:
    - Request with an invalid `Dimension`.
    - Request with an invalid `OverallPhase`.
    - Request with an invalid `Limit` (e.g., non-numeric, < 1, > 100).
    - Request with an invalid `NurseryProvision`.
    - Request with an invalid `SixthFormProvision`.
    - Request with an invalid `SpecialClassesProvision`.
    - Request with an invalid `SortField`.
    - Request with an invalid `SortOrder`.
    - Request with an invalid `x-api-version` header.
  - **404 Not Found**:
    - Request with a non-existent local authority code or no results found.

### GET local-authorities/{code}/maintained-schools/workforce

- **Versions**: 1.0
- **Validation Rules**:
  - **Dimension**: Must be one of: Actuals, PercentPupil.
  - **OverallPhase**: Empty or one of: Primary, Secondary, Special, Pupil Referral Unit, All-through, Nursery, Post-16, Alternative Provision, University Technical College.
  - **Limit**: Empty or a number between 1 and 100.
  - **NurseryProvision**: Empty or one of: Has Nursery Classes, No Nursery Classes, Not applicable, Not recorded.
  - **SixthFormProvision**: Empty or one of: Has a sixth form, Does not have a sixth form, Not applicable, Not recorded.
  - **SpecialClassesProvision**: Empty or one of: Has Special Classes, No Special Classes, Not applicable, Not recorded.
  - **SortField**: One of: SchoolName, TotalPupils, PupilTeacherRatio, EHCPlan, SENSupport.
  - **SortOrder**: One of: ASC, DESC.
- **Scenarios**:
  - **200 OK**:
    - Request with a valid local authority code and valid query parameters.
    - Request with only required query parameters.
  - **400 Bad Request**:
    - Request with an invalid `Dimension`.
    - Request with an invalid `OverallPhase`.
    - Request with an invalid `Limit` (e.g., non-numeric, < 1, > 100).
    - Request with an invalid `NurseryProvision`.
    - Request with an invalid `SixthFormProvision`.
    - Request with an invalid `SpecialClassesProvision`.
    - Request with an invalid `SortField`.
    - Request with an invalid `SortOrder`.
    - Request with an invalid `x-api-version` header.
  - **404 Not Found**:
    - Request with a non-existent local authority code or no results found.
