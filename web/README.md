# Web

This is the main front-end website project. This project will consume the output of the front end components project via NPM.

This is an MVC web app written in C#. It's main purpose is to provide proxy authentication/authorization services, along with any static pages that need serving

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FWeb%20CICD?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2600&branchName=main)

## Getting started

### Run the Web App locally

#### Secret storage
In a console window:
1. Navigate to `EducationBenchmarking.Web` project root
2. Run `dotnet user-secrets init` to initialise secrets in the directory

#### Platform APIs
If running the Platform APIs locally then no further configuration required; ensure the API port configuration matches that in `appsettings.Development.json`.

However, if you are using deployed instances of the Platform APIs then having initialised secret storage in a console window:
1. Navigate to `EducationBenchmarking.Web` project root
2. Set Insight API url user secret: `dotnet user-secrets set "Apis:Insight:Url" "[INSERT URL VALUE]"`
3. Set Insight API key user secret: `dotnet user-secrets set "Apis:Insight:Key" "[INSERT KEY VALUE]"`
4. Set Benchmark API url user secret: `dotnet user-secrets set "Apis:Benchmark:Url" "[INSERT URL VALUE]"`
5. Set Benchmark API key user secret: `dotnet user-secrets set "Apis:Benchmark:Key" "[INSERT KEY VALUE]"`
6. Set Establishment API url user secret: `dotnet user-secrets set "Apis:Establishment:Url" "[INSERT URL VALUE]"`
7. Set Establishment API key user secret: `dotnet user-secrets set "Apis:Establishment:Key" "[INSERT KEY VALUE]"`

#### DfE Sign-in (DSI) authentication
Having initialised secret storage, in a console window:
1. Navigate to `EducationBenchmarking.Web` project root
2. Set XXXX user secret: `dotnet user-secrets set "XXXX" "xxxxx"`

#### Session cache
Having initialised secret storage, in a console window:
1. Navigate to `EducationBenchmarking.Web` project root
2. Set session cache connection string user secret: `dotnet user-secrets set "CosmosCacheSettings:ConnectionString" "[INSERT CONNECTION STRING VALUE]"`
3. Optional, direct mode is preferred however if you have issues run the follow to set the mode to gateway: `dotnet user-secrets set "CosmosCacheSettings:IsDirect" false`

### Running tests

Tests will run when creating new Pull Requests and when code is merged into the main branch.
#### Unit Tests
Run:
```
dotnet test tests\EducationBenchmarking.Web.Tests
```
#### Integration Tests
Run:
```
dotnet test tests\EducationBenchmarking.Web.Integration.Tests
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
dotnet test tests\EducationBenchmarking.Web.E2ETests
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
dotnet test tests\EducationBenchmarking.Web.A11yTests
```

_Playwright is used for end-to-end and accessibility testing which opens a browser and navigates like a user._

