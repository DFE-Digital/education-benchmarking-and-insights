# Platform API Test Plan: School - Census

API: School  
Feature: Census

## Test Scenarios by Endpoint

### 1. `GET schools/{urn}/census/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Census`.
  * `Dimension`: Must be empty or valid `Dimensions.Census`.
* **Scenarios**:
  * **200 OK**: Default version check (no version header).
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version (`x-api-version` = '2.0').
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 2. `GET schools/{urn}/census`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Census`.
  * `Dimension`: Must be empty or valid `Dimensions.Census`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 3. `GET schools/{urn}/user-defined/{identifier}/census`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Census`.
  * `Dimension`: Must be empty or valid `Dimensions.Census`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn` or `identifier`.

### 4. `GET schools/{urn}/comparator-set-average/census/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Census`.
  * `Dimension`: Must be empty or valid `Dimensions.Census`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Category`.
  * **400 Bad Request**: Invalid `Dimension`.
  * **404 Not Found**: Non-existent `urn`.

### 5. `GET schools/national-average/census/history`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Dimension`: Must be empty or valid `Dimensions.Census`.
  * `OverallPhase`: Must be valid `OverallPhase`.
  * `FinanceType`: Must be valid `FinanceType`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Invalid `Dimension`.
  * **400 Bad Request**: Invalid `OverallPhase`.
  * **400 Bad Request**: Invalid `FinanceType`.

### 6. `GET schools/census` (Query)

* **Versions**: `1.0`
* **Validation Rules**:
  * `Category`: Must be empty or valid `Categories.Census`.
  * `Dimension`: Must be empty or valid `Dimensions.Census`.
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
  * **400 Bad Request**: Missing/Invalid `Phase` when using `CompanyNumber` or `LaCode`.

### 7. `GET schools/census/senior-leadership`

* **Versions**: `1.0`
* **Validation Rules**:
  * `Urns`: Must not be empty.
  * `Dimension`: Must be empty or one of `Dimensions.Census.Total` or `Dimensions.Census.PercentWorkforce`.
* **Scenarios**:
  * **200 OK**: Default version check.
  * **200 OK**: Explicit version check (`x-api-version` = '1.0').
  * **400 Bad Request**: Unsupported API version.
  * **400 Bad Request**: Empty `Urns`.
  * **400 Bad Request**: Invalid `Dimension`.
