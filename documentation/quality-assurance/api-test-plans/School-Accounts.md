# Platform API Test Plan: School - Accounts

API: School  
Feature: Accounts

## Test Scenarios by Endpoint

### GET schools/accounts/it-spending

- **Versions**: 1.0
- **Validation Rules**:
  - `Dimension` must be a valid Finance dimension (via `ItSpendingParametersValidator`).
- **Scenarios**:
  - **200 OK**:
    - Request with valid specific URNs and valid `Dimension`.
    - Request with missing `Dimension` (should default/succeed).
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Dimension` parameter.

### GET schools/{urn}/accounts/income

- **Versions**: 1.0
- **Validation Rules**: None explicit (Route parameter only).
- **Scenarios**:
  - **200 OK**: Request with a valid `urn` for an existing school.
  - **400 Bad Request**: Request with an invalid `x-api-version` header.
  - **404 Not Found**: Request with a `urn` that does not exist in the system.

### GET schools/{urn}/accounts/income/history

- **Versions**: 1.0
- **Validation Rules**:
  - `Dimension` must be a valid Finance dimension (via `IncomeParametersValidator`).
- **Scenarios**:
  - **200 OK**: Request with a valid `urn` and valid `Dimension`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Dimension` parameter.
  - **404 Not Found**: Request with a `urn` that does not exist in the system.

### GET schools/{urn}/comparator-set-average/accounts/income/history

- **Versions**: 1.0
- **Validation Rules**:
  - `Dimension` must be a valid Finance dimension (via `IncomeParametersValidator`).
- **Scenarios**:
  - **200 OK**: Request with a valid `urn` and valid `Dimension`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Dimension` parameter.
  - **404 Not Found**: Request with a `urn` that does not exist or has no comparator set.

### GET schools/national-average/accounts/income/history/

- **Versions**: 1.0
- **Validation Rules**:
  - `Dimension` must be a valid Finance dimension.
  - `OverallPhase` must be a recognized school phase.
  - `FinanceType` must be a recognized finance type.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `Dimension`, `OverallPhase`, and `FinanceType`.
    - Request with criteria that yield no data (returns empty response object, not 404).
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Dimension` parameter.
    - Request with an unrecognized `OverallPhase` parameter.
    - Request with an unrecognized `FinanceType` parameter.

### GET schools/{urn}/accounts/balance

- **Versions**: 1.0
- **Validation Rules**: None explicit (Route parameter only).
- **Scenarios**:
  - **200 OK**: Request with a valid `urn` for an existing school.
  - **400 Bad Request**: Request with an invalid `x-api-version` header.
  - **404 Not Found**: Request with a `urn` that does not exist in the system.

### GET schools/{urn}/accounts/balance/history

- **Versions**: 1.0
- **Validation Rules**: None explicitly invoked by the handler.
- **Scenarios**:
  - **200 OK**: Request with a valid `urn`.
  - **400 Bad Request**: Request with an invalid `x-api-version` header.
  - **404 Not Found**: Request with a `urn` that does not exist in the system.

### GET schools/{urn}/comparator-set-average/accounts/balance/history

- **Versions**: 1.0
- **Validation Rules**:
  - `Dimension` must be a valid Finance dimension (via `BalanceParametersValidator`).
- **Scenarios**:
  - **200 OK**: Request with a valid `urn` and valid `Dimension`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Dimension` parameter.
  - **404 Not Found**: Request with a `urn` that does not exist or has no comparator set.

### GET schools/national-average/accounts/balance/history/

- **Versions**: 1.0
- **Validation Rules**:
  - `Dimension` must be a valid Finance dimension.
  - `OverallPhase` must be a recognized school phase.
  - `FinanceType` must be a recognized finance type.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `Dimension`, `OverallPhase`, and `FinanceType`.
    - Request with criteria that yield no data (returns empty response object).
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Dimension` parameter.
    - Request with an unrecognized `OverallPhase` parameter.
    - Request with an unrecognized `FinanceType` parameter.

### GET schools/accounts/expenditure

- **Versions**: 1.0
- **Validation Rules**:
  - Must include `Urns` OR one of `CompanyNumber`/`LaCode`.
  - Cannot specify both `CompanyNumber` and `LaCode`.
  - `Phase` must be provided and valid if `CompanyNumber` or `LaCode` is used.
  - `Category` must be an empty string or a valid Cost category.
  - `Dimension` must be a valid Finance dimension.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `Urns` list and valid `Dimension`.
    - Request with valid `CompanyNumber` and `Phase`.
    - Request with valid `LaCode` and `Phase`.
    - Request that yields no results (returns empty array).
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request missing `Urns`, `CompanyNumber`, and `LaCode`.
    - Request providing both `CompanyNumber` and `LaCode`.
    - Request with `CompanyNumber` or `LaCode` but missing or invalid `Phase`.
    - Request with an unrecognized `Category` parameter.
    - Request with an unrecognized `Dimension` parameter.

### GET schools/{urn}/accounts/expenditure

- **Versions**: 1.0
- **Validation Rules**:
  - `Category` must be empty or a valid Cost category.
  - `Dimension` must be a valid Finance dimension.
- **Scenarios**:
  - **200 OK**: Request with a valid `urn`, `Category`, and `Dimension`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Category` parameter.
    - Request with an unrecognized `Dimension` parameter.
  - **404 Not Found**: Request with a `urn` that does not exist in the system.

### GET schools/{urn}/accounts/expenditure/history

- **Versions**: 1.0
- **Validation Rules**:
  - `Category` must be empty or a valid Cost category.
  - `Dimension` must be a valid Finance dimension.
- **Scenarios**:
  - **200 OK**: Request with a valid `urn`, `Category`, and `Dimension`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Category` parameter.
    - Request with an unrecognized `Dimension` parameter.
  - **404 Not Found**: Request with a `urn` that does not exist in the system.

### GET schools/{urn}/comparator-set-average/accounts/expenditure/history

- **Versions**: 1.0
- **Validation Rules**:
  - `Category` must be empty or a valid Cost category.
  - `Dimension` must be a valid Finance dimension.
- **Scenarios**:
  - **200 OK**: Request with a valid `urn`, `Category`, and `Dimension`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Category` parameter.
    - Request with an unrecognized `Dimension` parameter.
  - **404 Not Found**: Request with a `urn` that does not exist or has no comparator set.

### GET schools/national-average/accounts/expenditure/history/

- **Versions**: 1.0
- **Validation Rules**:
  - `Dimension` must be a valid Finance dimension.
  - `OverallPhase` must be a recognized school phase.
  - `FinanceType` must be a recognized finance type.
- **Scenarios**:
  - **200 OK**:
    - Request with valid `Dimension`, `OverallPhase`, and `FinanceType`.
    - Request with criteria that yield no data (returns empty response object).
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Dimension` parameter.
    - Request with an unrecognized `OverallPhase` parameter.
    - Request with an unrecognized `FinanceType` parameter.

### GET schools/{urn}/user-defined/{identifier}/accounts/expenditure

*Note: Test implementation for this endpoint has been deferred. There is currently no custom/user-defined pipeline test data available in the environment to support these scenarios. They should be implemented once the requisite data is provisioned.*

- **Versions**: 1.0
- **Validation Rules**:
  - `Category` must be empty or a valid Cost category.
  - `Dimension` must be a valid Finance dimension.
- **Scenarios**:
  - **200 OK**: Request with a valid `urn`, valid `identifier` for custom comparator set, `Category`, and `Dimension`.
  - **400 Bad Request**:
    - Request with an invalid `x-api-version` header.
    - Request with an unrecognized `Category` parameter.
    - Request with an unrecognized `Dimension` parameter.
  - **404 Not Found**: Request with a `urn` or `identifier` that does not exist or cannot be matched.
