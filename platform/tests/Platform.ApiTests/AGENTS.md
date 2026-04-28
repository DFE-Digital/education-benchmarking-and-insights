# API Testing Standards

This file defines AI-specific operational guidelines for agents working within the `Platform.ApiTests` project.
You **MUST** read the `README.md` in this directory before proposing changes or writing code for all human-readable architecture, tech stack, development standards, and anti-patterns.

All API functional tests in this project must adhere to the following workflow:

1. **Structural Realignment**: Feature files must be organized in subfolders matching the backend `Features` namespace (e.g., `Features/LocalAuthority/Details/`).
2. **Comprehensive Validation**: Every endpoint must have a `Scenario Outline` covering all validation rules found in its C# `Validator` class.
3. **JSON Assertions**: All tests must assert the exact JSON response payload using `AssertDeepEquals`.
4. **Data Verification Strategy**: When adding new tests, first use placeholder JSON (`[]` or `{}`), run the test, and then capture the actual response from the failure output to update the expected JSON data file.
