# Running Accessibility (A11y) Automated Tests locally

This guide outlines how to configure and run the automated accessibility (A11y) tests for the project. These tests are designed to run against the same environment used by the CI/CD pipeline to ensure accessibility compliance and coverage.

## 🚀 Getting Started

Follow these steps to set up and execute A11y tests locally:

### 1. Update Your `appsettings.local.json`

Create or update the `appsettings.local.json` file in the test project directory to point to the correct  [automated test environment](https://s198d02-education-benchmarking-fqhxhwdsdyh3cded.a02.azurefd.net)

### 2. Populate Required Variables

Refer to the `appsettings.json` file for all required configuration keys. You must replicate these keys in `appsettings.local.json` with appropriate values.

### 3. Authentication

The A11y tests use the following test account:

- **Username:** `dsi.ebfi.a11yacc1@outlook.com`
- **Password:** _Obtain this from a member of the Tech Team._

> ⚠️ **Important:** Do not commit credentials to version control. Treat this account as sensitive and follow your team's security practices.
### Running the tests
Once you've configured appsettings.local.json, you need to build the project and then can run as per your preferred command line tool. 