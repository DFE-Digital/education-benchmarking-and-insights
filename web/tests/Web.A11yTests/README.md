# Running Accessibility (A11y) Tests Locally

This guide provides an overview for setting up and running accessibility (A11y) tests for the project, consistent with the CI/CD pipeline configuration.  
It complements the detailed setup instructions already available in [`web/README.md`](../../README.md#accessibility-tests).

## 🚀 Getting Started

To run the tests locally and ensure they match pipeline behavior, follow these steps:

### 1. Configure `appsettings.local.json`

Follow the configuration structure outlined in [`web/README.md`](../../README.md#accessibility-tests) and create or update your `appsettings.local.json` file in the `Web.A11yTests` project directory.

- Set the correct `"ServiceUrl"` for the [automated test environment](https://s198d02-education-benchmarking-fqhxhwdsdyh3cded.a02.azurefd.net)


### 2. Manage Secrets Securely

- **Credentials** (`dfe-signin-testa11y-username` and `dfe-signin-testa11y-password`) are stored securely in Azure DevOps:

  > `Library` → `Automated Tests App Settings`

Fetch any other required variable values from the same library.

- **Benchmark Host and Key** must be retrieved from **Azure Key Vault**.  

### 3. Running the Tests

Once your local settings are configured, run the tests from the `web` directory root with:

```bash
dotnet test tests/Web.A11yTests
