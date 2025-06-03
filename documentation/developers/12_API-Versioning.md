#  API Endpoint Versioning

This guide explains how to implement a new API endpoint and/or a new version of an existing endpoint using the header-based versioning infrastructure in Azure Functions.

## Prerequisites

Ensure the versioning framework is already in place, including:
- A base `IVersionedHandler` interface.
- A `VersionedHandlerDispatcher<T>` for version selection.
- Extension method `ReadVersion` for reading header from HTTP request.

## Define a New Handler Interface (for new endpoints)

- Create a new handler interface for your endpoint.
- Inherit from `IVersionedHandler`.
- Define the method that processes the request for this endpoint.

> For example: `IGetSchoolsHandler` for a `/schools` endpoint.

## Implement Versioned Handlers

- Implement one class per supported API version.
- Each class should:
    - Implement the new handler interface.
    - Define a `Version` property with a string like `"1.0"`.
    - Include the request-handling logic specific to that version.

> Handler should be implement even if there is just a single (latest) version

## Register Handlers and Dispatcher

In your DI container configuration:

- Register all versioned handler implementations for the new endpoint.
- Register a `VersionedHandlerDispatcher<T>` for the new handler interface.

## Create the Azure Function for the Endpoint

- Create a new Azure Function for the endpoint (e.g., `GetSchoolsFunction`).
- Inject:
    - `IVersionedHandlerDispatcher<T>` for the handler interface.
- In the Function method:
    - Read the version from the `x-api-version` header.
    - Use the dispatcher to get the matching handler.
    - If not found, return a 400 Bad Request with a relevant error.
    - Otherwise, delegate the request to the selected handler.

## Test Your Implementation

- **Unit tests** for each versioned handler.
- **Functional tests** for the new Azure Function to ensure:
    - Version routing works.
    - The latest version is selected if no version is specified.
    - Bad requests are returned for unsupported versions.

## Update Documentation and API Spec

- **Update documentation** to reflect the new endpoint and versioned handler setup.
- **Define the API spec only for the latest version** of the new endpoint:
    - This includes Swagger/OpenAPI or API reference docs.
- **Do not document legacy versions**:
    - Older versions are maintained for backwards compatibility only.
    - They are **undocumented** and intended for internal use or legacy clients only.

> ⚠️ Developers should always target the latest documented version unless otherwise required.

<!-- Leave the rest of this page blank -->
\newpage
