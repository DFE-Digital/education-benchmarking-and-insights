# Platform API Test Implementation Workflow

This procedural guide details how to implement 100% functional test coverage with strict JSON assertions specifically for Platform API features, following a formal test plan.

## Procedural Steps

### 1. Discovery & Load Test Plan

- **Mandatory Discovery Rules**: Read and follow the discovery and validation rules in `ai-tools/instructions/shared-api-discovery.md` to resolve the target **API** and **Feature** from the provided input.
- If either the API name or the Feature name is missing, or if the target cannot be found, you **MUST STOP** and follow the **Fallback & Clarification** rules in the discovery document.
- Locate and read the corresponding test plan in `documentation/quality-assurance/api-test-plans/[API]-[Feature].md`.
- Identify the scenarios, versions, and validation rules to be implemented.

### 2. Structural Realignment

- Verify the feature file location in `platform/tests/Platform.ApiTests/Features/`.
- **Mandate:** Feature files MUST reside in subfolders matching the backend namespace (e.g., `Features/LocalAuthority/Details/`).
- If feature files are flat in a module folder, move them into the appropriate sub-directory and update their `Feature:` header to include the parent grouping (e.g., `Feature: Local Authority Details - Maintained Schools`).
- Ensure the corresponding Step Binding classes have the correct `[Scope(Feature = "...")]` attribute.
- **Code-Behind Cleanup:** When moving .feature files, ensure you clean up orphaned code-behind files (e.g., .feature.cs) to prevent compiler warnings.

### 3. Implementation of Gherkin & JSON Assertions

- Implement the scenarios defined in the test plan in the .feature file.
- All scenarios MUST assert the exact JSON response payload using `AssertDeepEquals`.
- **Decoupled Payloads:** NEVER add a `<ProjectReference>` to the API backend projects from the test project. NEVER use strongly-typed domain models (e.g., `SearchRequest`) for test payloads. Instead, use C# anonymous types with explicit `camelCase` property names (e.g., `_request = new { searchText = "school", size = 10 };`) to ensure the HTTP payload perfectly mimics an external client request.
- **Supported API Versions (200 OK):** Test default behavior and parameterized versions as defined in the plan.
- **Bad Request Scenarios (400):** Implement the `Scenario Outline` that iterates through EVERY validation rule identified in the plan.
- **Unsupported API Version (400):** Include the scenario asserting a `400 Bad Request` with a `ProblemDetails` response for unsupported versions.
- **Prohibit BDD Anti-Patterns:** Do not include HTTP routes, JSON structures, or implementation details in Gherkin feature files or comments. Feature files must describe behavior only.
- **Handle Deferred/Zombie Code:** If a scenario in the test plan is marked as 'Deferred' or 'Ignored' (e.g., due to missing test data), DO NOT implement it in the .feature file with an `@ignore` tag. Omit it entirely to prevent zombie code in the test suite.

### 4. Data Verification Strategy (Recording Mode)

When creating or updating expected JSON data files, use the built-in recording infrastructure:

1. **Initialize Placeholders:** Create a placeholder JSON file (containing exactly `{}` or `[]`) in the appropriate `Data/` subfolder (e.g., `platform/tests/Platform.ApiTests/Data/School/Search/`) prior to running the suite. *Crucial: If you skip this, the Embedded Resource loader will crash the test before it can reach your recording logic.*
2. **Inject Recording Hook:** In your C# step binding, wrap a call to the permanent helper method in a compiler directive block just before the `AssertDeepEquals` check:

   ```csharp
   #if RECORD_API_TESTS
       TestDataProvider.OutputTestJsonData(content, "Search_default.json", "School", "Search");
       return;
   #endif
   ```

3. **Execute with MSBuild Flag:** Run the tests using the specific MSBuild syntax required to preserve the `DEBUG` constant while activating the recorder (use `%3B` as the semicolon separator in PowerShell):
   `dotnet test platform/tests/Platform.ApiTests/Platform.ApiTests.csproj --filter "FeatureTitle='[Your Feature]'" /p:DefineConstants="DEBUG%3BRECORD_API_TESTS"`
4. **Cleanup:** After the files are successfully generated in the `Data/` directory, remove the `#if RECORD_API_TESTS` blocks from your step bindings.
5. **Verify:** Re-run the tests normally (without the flag) to ensure a 100% match against the newly recorded baseline.

## Key Files to Reference

- `platform/tests/Platform.ApiTests/TestDataHelpers/TestDataProvider.cs`: Used for loading expected JSON.
- `platform/tests/Platform.ApiTests/Assertion/AssertionExtensions.cs`: Contains `AssertDeepEquals`.
- `platform/tests/Platform.ApiTests/README.md`: Root testing mandates for Platform API.
