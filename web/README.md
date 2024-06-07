# Web

This is the main front-end website project. This project will consume the output of the front-end components project via NPM.

This is an MVC web app written in C#. It's main purpose is to provide proxy authentication/authorisation services, along with any static pages that need serving

## Prerequisites

1. Install .NET 8 SDK
2. Install Visual Studio 2022 Professional (with C#, Node and Azure Workflows) or Rider 2024
3. ADO private artefact access to install published Front-end components package
4. Install Node 20.11.1 (if necessary use nvm to switch to this version nvm use 20.11.1)
5. Clone the project `git clone https://github.com/DFE-Digital/education-benchmarking-and-insights.git`

> **Note:** Ensure that, if cloning to a DfE user area, the root folder is outside any of the 'OneDrive' folders to prevent 'too long path name' errors at build time.

## Getting started

### Run the Web App locally

#### Secret storage

In a console window:

1. Navigate to the `Web.App` project root
2. Run `dotnet user-secrets init` to initialise secrets in the directory

> Note: If there is already a `<UserSecretsId>` setting in the `Web.App` project file then `dotnet user-secrets init` will fail. This is because the dotnet tool thinks the user secrets has already been initialised. To avoid this run `dotnet user-secrets set "PLACEHOLDER" "PLACEHOLDER".` This will create a `secrets.json` file in the folder location described [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux#how-the-secret-manager-tool-works). At this point `secrets.json` can be updated manually with the settings described below.

#### Platform APIs

If you are running the Platform APIs locally then no further configuration required; ensure the API port configuration matches that in `appsettings.Development.json` in the root of `Web.App`.

However, if you are using deployed instances of the Platform APIs then having initialised the secret storage add the following section to `secrets.json`

```json
  "Apis": {
    "Insight": {
      "Url": "[INSERT URL VALUE]",
      "Key": "[INSERT KEY VALUE]"
    },
    "Benchmark": {
      "Url": "[INSERT URL VALUE]",
      "Key": "[INSERT KEY VALUE]"
    },
    "Establishment": {
      "Url": "[INSERT URL VALUE]",
      "Key": "[INSERT KEY VALUE]"
    }
  }
```

##### Features

Feature flags may also be defined in the `FeatureManagement` section:

| Name                            | Purpose                                                              |
|---------------------------------|----------------------------------------------------------------------|
| `CurriculumFinancialPlanning`   | Toggles the Curriculum and Financial Planning feature                |
| `CustomData`                    | Toggles the Custom Data feature                                      |
| `DisableOrganisationClaimCheck` | Skips the Organisation Claim check in `SchoolAuthorizationAttribute` |
| `LocalAuthorities`              | Toggles the Local Authorities feature                                |
| `Trusts`                        | Toggles the Trust feature                                            |
| `UserDefinedComparators`        | Toggles the User Defined comparators feature                         |

#### DfE Sign-in (DSI) authentication

Having initialised the secret storage, add the following section to `secrets.json`

```json
  "DFESignInSettings": {
    "APISecret": "[INSERT API SECRET VALUE]",
    "APIUri": "[INSERT URL VALUE]",
    "Audience": "[INSERT AUDIENCE VALUE]",
    "CallbackPath": "[INSERT PATH VALUE]",
    "ClientID": "[INSERT ID VALUE]",
    "ClientSecret": "[INSERT SECRET VALUE]",
    "Issuer": "[INSERT SECRET VALUE]",
    "MetadataAddress": "[INSERT URL VALUE]",
    "SignedOutCallbackPath": "[INSERT PATH VALUE]",
    "SignOutUri": "[INSERT URL VALUE]"
  }
```

### Build the front-end library

To use the GOV.UK Design System and front-end components locally:

- Navigate to the root of the Web APP `.\web\src\Web.App`
- Install the required packages `npm i`
- Run the gulp script to build ssas and copy assets `npm run-script build`

### Running tests

Tests will run when creating new Pull Requests and when code is merged into the main branch.

#### Unit Tests

From the root of the `web` run

```bat
dotnet test tests\Web.Tests
```

#### Integration Tests

From the root of the `web` run

```bat
dotnet test tests\Web.Integration.Tests
```

#### End-to-end Tests

Add the following configuration in `appsetings.local.json` in the root of `Web.E2ETests`

```json
{
  "ServiceUrl": "[INSERT URL OF SERVICE UNDER TEST]",
  "Headless" : true
}
```

From the root of the `web` run

```bat
dotnet test tests\Web.E2ETests
```

#### Accessibility Tests

Add the following configuration in `appsettings.local.json` in the root of `Web.A11yTests`

```json
{
  "ServiceUrl": "[INSERT URL OF SERVICE UNDER TEST]",
  "Headless" : true,
  "SchoolUrn" : "[INSERT SCHOOL URN]",
  "TrustCompanyNo" : "[INSERT TRUST COMPANY NO]",
  "Impacts" : ["critical",  "serious", "moderate", "minor"],
  "PlanYear" : [INSERT PLAN YEAR],
  "Benchmark": {
    "Host": "[INSERT URL OF BENCHMARK API]",
    "Key": "[INSERT BENCHMARK API KEY]"
  },
  "Authentication": {
    "Username": "[INSERT DSI USERNAME]",
    "Password": "[INSERT DSI PASSWORD]"
  }
}
```

From the root of the `web` run

```bat
dotnet test tests\Web.A11yTests
```

_Playwright is used for end-to-end and accessibility testing which opens a browser and navigates like a user._

> **NOTE:** Running _all_ accessibility tests locally using DSI credentials that are not configured to be able to access the
> school defined in config will result in test failures for those in the `CustomData` and `FinancialPlanning` xUnit categories
> unless the feature flag `DisableOrganisationClaimCheck` has been set to `true`. To skip these tests use the following filter:

```bat
dotnet test tests\Web.A11yTests --filter "Category!=CustomData&Category!=FinancialPlanning"
```
