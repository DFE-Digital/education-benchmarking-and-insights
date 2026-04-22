# Platform API Test Planning Workflow

This procedural guide details how to research and plan 100% functional test coverage for Platform API features.

## Procedural Steps

### 1. Research & Dependency Mapping

- **Mandatory Discovery Rules**: Read and follow the discovery and validation rules in `.gemini/instructions/shared-api-discovery.md` to resolve the target **API** and **Feature** from the provided input to its directory path (e.g., `platform/src/apis/Platform.Api.[API]/Features/[FeatureName]`).
- If either the API name or the Feature name is missing, or if the target cannot be found, you **MUST STOP** and follow the **Fallback & Clarification** rules in the discovery document.
- **Endpoints & Verbs:** Read `Routes.cs` to identify all public route strings. Then, scan the `Functions/` directory for `[HttpTrigger]` attributes to correctly map the HTTP Verbs (e.g., `GET`, `POST`) to those routes.
- **Validation Rules:** Read all `Validator` classes in the feature's `Validators/` folder. Document every validation rule found. *Crucially, look out for `Include(new SomeOtherValidator())` statements to ensure you capture inherited FluentValidation rules from parent validators.*
- **API Versions:** Check the `Handlers/` implementing `IVersionedHandler` to identify all supported API versions (e.g., '1.0', '1.1') for the feature.
- **Not Found Handling:** Inspect the `Handlers/` to see if a 404 Not Found response is explicitly returned (e.g., via `CreateNotFoundResponse()`).

### 2. Evaluate Test Coverage

Define the scenarios required for 100% functional coverage:

- **Success Scenarios (200 OK):** Identify valid data combinations for each explicitly supported version.
- **Bad Request Scenarios (400):** Map EVERY validation rule identified in step 1 (including inherited ones) to a test case.
- **Unsupported API Version (400):** Plan a test case for an invalid `x-api-version` header.
- **Not Found Scenarios (404):** Only add a 404 test scenario if the code actually handles and returns a 404 response.

### 3. Create/Update Test Plan

Create a Markdown file in `documentation\quality-assurance\api-test-plans\[API]-[Feature].md`.
*Note: Derive the `[API]` name by stripping `Platform.Api.` from the project namespace (e.g., `Platform.Api.School` becomes `School`).*

**Strict Format:** You MUST use the template defined in `.gemini/templates/test-plan-template.md` for the document structure.

### 4. Verification

- Ensure the plan is saved and follows the required structure.
- Notify the user that the planning phase is complete and provide a link/path to the plan.

## Key Files to Reference

- `platform/tests/Platform.ApiTests/README.md`: Root testing mandates for Platform API.
