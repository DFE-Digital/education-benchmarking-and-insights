# Platform API Metadata Enrichment Workflow

This procedural guide details how to enhance the OpenAPI documentation of a specific Platform API Feature without altering its core business logic.

## Procedural Steps

### 1. Scope & Discovery

- Identify the target API and Feature from the provided input.
- **Mandatory Discovery Rules**: Read and follow the discovery and validation rules in `.gemini/instructions/shared-api-discovery.md` to resolve the `<api_name>` and `<feature_name>` to a specific directory path (e.g., `platform/src/apis/Platform.Api.School/Features/Search`).
- If the target cannot be found or if either name is missing, follow the **Fallback & Clarification** rules in the discovery document before proceeding.
- Once validated, scan ONLY the specified `Features/[Feature_Name]` directory (and its subdirectories) to locate:
  - Azure Function endpoints (files containing `[HttpTrigger]`).
  - Request/Response Models (typically in `Models/` or `Responses/` directories).

### 2. OpenAPI Enrichment (Operations & Parameters)

- Open each Azure Function file identified in Step 1.
- **Operations:** Locate the `[OpenApiOperation]` attribute. Ensure it has both `Summary` and `Description` properties. Provide a clear, business-readable explanation of what the endpoint does.
- **Parameters:**
  - **Custom Attributes:** Identify parameters using custom specialized attributes (e.g., `[OpenApiUrnParameter]`, `[OpenApiDimensionParameter]`, `[OpenApiApiVersionParameter]`).
  - **Check Implementation:** Verify the default `Description` for these attributes in `platform/src/abstractions/Platform.Functions/OpenApi/`.
  - **Avoid Redundancy:** Do NOT add a `Description` property override to a custom attribute if the default description is sufficient. If a redundant `Description` property exists that matches the global standard, REMOVE it to keep the code clean.
  - **Standard Parameters:** For generic `[OpenApiParameter]` attributes, ALWAYS ensure a clear `Description` property is present.

### 3. OpenAPI Enrichment (Inputs & Outputs)

- **Request Bodies:** Locate `[OpenApiRequestBody]` attributes (typically on POST/PUT endpoints). Ensure they have a `Description` property that clearly explains the expected payload structure and its purpose.
- **Responses:** Locate `[OpenApiResponseWithBody]` and `[OpenApiResponseWithoutBody]` attributes. Ensure every response type (e.g., `statusCode: HttpStatusCode.OK`, `HttpStatusCode.BadRequest`, `HttpStatusCode.NotFound`) has a `Description` property explaining the conditions under which that response is returned and what the payload represents.

### 4. Model Enrichment (XML Documentation)

- Open each Request/Response Model file identified in Step 1.
- Add XML `<summary>` tags to the class/record itself and to every public property.
- Descriptions should be concise and explain the business meaning of the property (e.g., "The unique slug used for the URL of the news article" or "The academic year the financial data relates to").

### 5. Verification

- Run a build on the specific API project (e.g., `dotnet build platform/src/apis/Platform.Api.[API_NAME]/Platform.Api.[API_NAME].csproj`) to ensure no compilation errors were introduced by syntax typos.
- Inform the user that the OpenAPI and Model documentation enrichment is complete for the specified feature.

## Constraints

- **Strict Focus:** Do NOT modify domain constants (unless specifically asked), validation rules, or core business logic. Only change OpenAPI attribute properties (`Summary`, `Description`, etc.) and XML documentation tags.
- **Preserve Logic:** Do NOT change routing logic, authorization levels, handler mappings, or HTTP verbs.
- **Clean Code:** Prioritize using central attribute descriptions over local overrides to maintain consistency and reduce boilerplate in Function classes.
