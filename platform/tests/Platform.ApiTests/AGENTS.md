# API Testing Standards

All API functional tests in this project must adhere to the following workflow:

1. **Structural Realignment**: Feature files must be organized in subfolders matching the backend `Features` namespace (e.g., `Features/LocalAuthority/Details/`).
2. **Comprehensive Validation**: Every endpoint must have a `Scenario Outline` covering all validation rules found in its C# `Validator` class.
3. **JSON Assertions**: All tests must assert the exact JSON response payload using `AssertDeepEquals`.
4. **Data Verification Strategy**: When adding new tests, first use placeholder JSON (`[]` or `{}`), run the test, and then capture the actual response from the failure output to update the expected JSON data file.
