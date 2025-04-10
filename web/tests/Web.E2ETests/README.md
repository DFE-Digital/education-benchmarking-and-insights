# Running Automated End-to-End (E2E) Tests locally

This guide provides instructions on how to run the automated E2E tests for the project, specifically targeting the same environment used by the CI/CD pipeline.

## 🚀 Getting Started

To run the tests locally and ensure they mimic the behavior and environment of the pipeline, follow these steps:

### 1. Update Your `appsettings.local.json`

Create or update the `appsettings.local.json` file in the test project directory to point to the correct  [automated test environment](https://s198d02-education-benchmarking-fqhxhwdsdyh3cded.a02.azurefd.net)

### 2. Populate Required Variables

Refer to the `appsettings.json` file for all required configuration keys. You must replicate these keys in `appsettings.local.json` with appropriate values.

### 3. Authentication

The E2E tests use the following test account:

- **Username:** `dsi.ebfi.acc1@outlook.com`
- **Password:** _Obtain this from a member of the Tech Team._

> ⚠️ **Important:** Do not commit credentials to version control. Treat this account as sensitive and follow your team's security practices.

### 4. Headless Mode (Optional)

To run tests in headless mode (without opening a browser window), you can add the following variable in `appsettings.local.json`:

```json
"Headless": true 
```

### Running the tests
Once you've configured appsettings.local.json, you need to build the project and then can run as per your preferred command line tool. 