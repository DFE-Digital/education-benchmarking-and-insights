# Platform API Test Plan: School - Accounts

API: School  
Feature: Accounts

## Test Scenarios by Endpoint

### 1. `GET schools/accounts/it-spending`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check (no version header).
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version (`x-api-version` = '2.0').
  * **400 Bad Request**: Invalid `Dimension`.

### 2. `GET schools/{urn}/accounts/income/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 3. `GET schools/{urn}/accounts/income`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 4. `GET schools/{urn}/comparator-set-average/accounts/income/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 5. `GET schools/national-average/accounts/income/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
  * `OverallPhase`: Must be valid `OverallPhase`.
  * `FinanceType`: Must be valid `FinanceType`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **400 Bad Request**: Invalid `OverallPhase`.
  * **400 Bad Request**: Invalid `FinanceType`.

### 6. `GET schools/{urn}/accounts/balance/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 7. `GET schools/{urn}/accounts/balance`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 8. `GET schools/{urn}/comparator-set-average/accounts/balance/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 9. `GET schools/national-average/accounts/balance/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
  * `OverallPhase`: Must be valid `OverallPhase`.
  * `FinanceType`: Must be valid `FinanceType`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **400 Bad Request**: Invalid `OverallPhase`.
  * **400 Bad Request**: Invalid `FinanceType`.

### 10. `GET schools/{urn}/user-defined/{identifier}/accounts/expenditure`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Cost`.
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn` or `identifier`.

### 11. `GET schools/{urn}/accounts/expenditure`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Cost`.
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 12. `GET schools/{urn}/comparator-set-average/accounts/expenditure/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Cost`.
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 13. `GET schools/{urn}/accounts/expenditure/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Cost`.
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 14. `GET schools/national-average/accounts/expenditure/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
  * `OverallPhase`: Must be valid `OverallPhase`.
  * `FinanceType`: Must be valid `FinanceType`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **400 Bad Request**: Invalid `OverallPhase`.
  * **400 Bad Request**: Invalid `FinanceType`.

### 15. `GET schools/accounts/expenditure` (Query)

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Cost`.
  * `Dimension`: Must be empty or valid `Dimensions.Finance`.
  * `Urns`: Must not be empty when `CompanyNumber` and `LaCode` are empty.
  * `LaCode`: Cannot be provided if `CompanyNumber` is present.
  * `Phase`: Required if querying by `CompanyNumber` or `LaCode`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **400 Bad Request**: Empty request (No identifier provided).
  * **400 Bad Request**: Both `CompanyNumber` and `LaCode` provided.
  * **400 Bad Request**: Missing/Invalid `Phase` when using identifiers.
