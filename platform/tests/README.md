# Platform Testing Standards

This project uses a combination of API functional tests (SpecFlow) and Unit tests (xUnit).

## API Functional Testing (SpecFlow)

All API functional tests in this project must adhere to the following workflow:

1. **Structural Realignment**: Feature files must be organized in subfolders matching the backend `Features` namespace (e.g., `Features/LocalAuthority/Details/`).
2. **Comprehensive Validation**: Every endpoint must have a `Scenario Outline` covering all validation rules found in its C# `Validator` class.
3. **JSON Assertions**: All tests must assert the exact JSON response payload using `AssertDeepEquals`.
4. **Data Verification Strategy**: When adding new tests, first use placeholder JSON (`[]` or `{}`), run the test, and then capture the actual response from the failure output to update the expected JSON data file.

## Unit Testing Conventions (xUnit)

Unit tests are used to verify individual components such as Handlers, Services, Validators, Mappers, and Parameters.

### Naming Conventions

All new unit test files and classes must follow the BDD-style naming convention:

* **Handlers**: `When[Class][Action].cs` (e.g., `WhenPostSearchV1HandlerHandles.cs`)
* **Services**: `When[Service]Queries.cs` or `When[Service]Runs.cs`
* **Validators**: `When[Validator]Validates.cs`
* **Parameters**: `Given[ParametersName].cs` (e.g., `GivenFinanceSummaryParameters.cs`)
* **Mappers**: `When[Feature]MapperMaps.cs`

**Important**: The `public class` name within the file must exactly match the filename.

### Testing Patterns

* **Handlers**: Inherit from `HandlerTestBase`. Use `Moq` for dependencies and `MockHttpRequestData.Create` to mock request contexts (e.g., `BasicContext`, `IdContext`).
* **Services (Database)**: Use `.Callback` interceptors with `Moq` on `IDatabaseConnection.QueryAsync` / `QueryFirstOrDefaultAsync` to capture and verify the generated `PlatformQuery` (checking `RawSql` and `.Parameters`).
* **Parameters**: Instantiate the parameter record, call `.SetValues` with an empty and populated `NameValueCollection`, and assert default vs. mapped values.
* **Coverage**: 100% code coverage is expected for all new logic.
