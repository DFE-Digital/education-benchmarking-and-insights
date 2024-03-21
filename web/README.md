# Web

This is the main front-end website project. This project will consume the output of the front end components project via NPM.

This is an MVC web app written in C#. It's main purpose is to provide proxy authentication/authorization services, along with any static pages that need serving

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FWeb%20CICD?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2600&branchName=main)

## Getting started

### Run the Web App locally

#### Secret storage
In a console window:
1. Navigate to `Web.App` project root
2. Run `dotnet user-secrets init` to initialise secrets in the directory

> Note: If there is already a `<UserSecretsId>` setting in the `Web.App` project file then `dotnet user-secrets init` will fail. This is because the dotnet tool thinks the user secrets has already been initialised. To avoid this run `dotnet user-secrets set "PLACEHOLDER" "PLACEHOLDER". This will create a `secrets.json` file in the folder location described [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux#how-the-secret-manager-tool-works). At this point you can update the `secrets.json` by hand with all of the required settings.

#### Platform APIs
If running the Platform APIs locally then no further configuration required; ensure the API port configuration matches that in `appsettings.Development.json`.

However, if you are using deployed instances of the Platform APIs then having initialised secret storage in a console window:
1. Navigate to `Web.App` project root
2. Set Insight API url user secret: `dotnet user-secrets set "Apis:Insight:Url" "[INSERT URL VALUE]"`
3. Set Insight API key user secret: `dotnet user-secrets set "Apis:Insight:Key" "[INSERT KEY VALUE]"`
4. Set Benchmark API url user secret: `dotnet user-secrets set "Apis:Benchmark:Url" "[INSERT URL VALUE]"`
5. Set Benchmark API key user secret: `dotnet user-secrets set "Apis:Benchmark:Key" "[INSERT KEY VALUE]"`
6. Set Establishment API url user secret: `dotnet user-secrets set "Apis:Establishment:Url" "[INSERT URL VALUE]"`
7. Set Establishment API key user secret: `dotnet user-secrets set "Apis:Establishment:Key" "[INSERT KEY VALUE]"`

#### DfE Sign-in (DSI) authentication
Having initialised secret storage, in a console window:
1. Navigate to `Web.App` project root
2. Set DSI Sign-out URI user secret: `dotnet user-secrets set "DFESignInSettings:SignOutUri" "[INSERT URL VALUE]"`
3. Set DSI Signed out callback path user secret: `dotnet user-secrets set "DFESignInSettings:SignedOutCallbackPath" "[INSERT PATH VALUE]"`
4. Set DSI Metadata address user secret: `dotnet user-secrets set "DFESignInSettings:MetadataAddress" "[INSERT URL VALUE]"`
5. Set DSI Issuer user secret: `dotnet user-secrets set "DFESignInSettings:Issuer" "[INSERT SECRET VALUE]"`
6. Set DSI Client Secret user secret: `dotnet user-secrets set "DFESignInSettings:ClientSecret" "[INSERT CLIENT SECRET VALUE]"`
7. Set DSI Client ID user secret: `dotnet user-secrets set "DFESignInSettings:ClientID" "[INSERT ID VALUE]"`
8. Set DSI Callback path user secret: `dotnet user-secrets set "DFESignInSettings:CallbackPath" "[INSERT PATH VALUE]"`
9. Set DSI Audience user secret: `dotnet user-secrets set "DFESignInSettings:Audience" "[INSERT AUDIENCE VALUE]"`
10. Set DSI API Url user secret: `dotnet user-secrets set "DFESignInSettings:APIUri" "[INSERT AUDIENCE VALUE]"`
11. Set DSI API Secret user secret: `dotnet user-secrets set "DFESignInSettings:APISecret" "[INSERT API SECRET VALUE]"`


### Running tests

Tests will run when creating new Pull Requests and when code is merged into the main branch.
#### Unit Tests
Run:
```
dotnet test tests\Web.Tests
```
#### Integration Tests
Run:
```
dotnet test tests\Web.Integration.Tests
```

#### End-to-end Tests
Add configuration in `appsetings.local.json`
```
{
  "ServiceUrl": "[INSERT URL OF SERVICE UNDER TEST]",
  "Headless" : true
}
```
Run:
```
dotnet test tests\Web.E2ETests
```
#### Accessibility Tests
Add configuration in `appsetings.local.json`
```
{
  "ServiceUrl": "[INSERT URL OF SERVICE UNDER TEST]",
  "Headless" : true,
  "SchoolUrn" : "[INSERT SCHOOL URN]",
  "Impacts" : ["critical",  "serious", "moderate", "minor"]
  "PlanYear" : [INSERT PLAN YEAR],
  "Benchmark": {
    "Host": "[INSERT URL OF BENCHMARK API]",
    "Key": "[INSERT BENCHMARK API KEY]"
  },
}
```
Run:
```
dotnet test tests\Web.A11yTests
```

_Playwright is used for end-to-end and accessibility testing which opens a browser and navigates like a user._

