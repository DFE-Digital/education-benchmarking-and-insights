# Running Automated End-to-End (E2E) Tests Locally

This guide provides an overview for setting up and running automated E2E tests for the project, aligned with the CI/CD pipeline setup.  
It complements the more detailed instructions already available in [`web/README.md`](../../README.md#end-to-end-tests).

## 🚀 Getting Started

To run the tests locally and ensure they mirror the CI/CD pipeline environment, follow these high-level steps:

### 1. Configure `appsettings.local.json`

Follow the detailed instructions in [`web/README.md`](../../README.md#end-to-end-tests) to create or update your `appsettings.local.json` file in the `Web.E2ETests` project.  

Ensure you:

- Set the correct `"ServiceUrl"` for the [automated test environment](https://s198d02-education-benchmarking-fqhxhwdsdyh3cded.a02.azurefd.net)

### 2. Manage Credentials Securely

- **Credentials** (`dfe-signin-test-username` and `dfe-signin-test-password`) are stored securely in Azure DevOps:

  > `Library` → `Automated Tests App Settings`
  
Fetch any other required variable values from the same library.  

### 3. Running the Tests

Once your `appsettings.local.json` is configured, build and run the E2E test project using the following command from the `web` directory root:

```bash
dotnet test tests/Web.E2ETests
